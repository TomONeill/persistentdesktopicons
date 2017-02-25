using Microsoft.Win32;
using PersistentDesktopIcons.Common.Helpers;
using PersistentDesktopIcons.Common.Logger;
using PersistentDesktopIcons.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PersistentDesktopIcons.Common
{
    public class DesktopIconsController : IDisposable
    {
        private List<DesktopIcon> _cachedIcons;
        private const long MaxLogSizeInKiloBytes = 256;
        private const int IntervalInMilliSeconds = 1000;

        public void Start()
        {
#if !DEBUG
            Log.ClearIfLargerThan(MaxLogSizeInKiloBytes);
#endif
            Log.WriteLine("Persistent Desktop Icons started.");

            var thread = new Thread(InternalRun)
            {
                IsBackground = true,
                Name = "DesktopIconsRestoreController.InternalRun()"
            };
            thread.Start();

            SystemEvents.DisplaySettingsChanging += BeforeDisplaySettingsChanging;
            SystemEvents.DisplaySettingsChanged += AfterDisplaySettingsChanging;
        }

        private void InternalRun()
        {
            while (true)
            {
                Thread.Sleep(IntervalInMilliSeconds);
            }
        }

        private void BeforeDisplaySettingsChanging(object sender, EventArgs e)
        {
            Log.WriteLine("Resolution is changing...");
            
            CacheDesktopIcons();
        }

        private void AfterDisplaySettingsChanging(object sender, EventArgs e)
        {
            Log.WriteLine("Resolution has changed.");

            RestoreDesktopIcons();
        }

        private void CacheDesktopIcons()
        {
            Log.WriteLine("Caching desktop icon positions...");

            var desktopIconGetter = new DesktopIconGetter();

            _cachedIcons = desktopIconGetter.GetDesktopIcons();
            
            Log.WriteLine("Cached desktop icon positions.");
        }

        private void RestoreDesktopIcons()
        {
            Log.WriteLine("Restoring desktop icon positions from cache...");

            if (!_cachedIcons.Any())
            {
                Log.WriteLine("No desktop icons were cached. Therefore, no icons have been restored.");

                return;
            }

            var desktopIconSetter = new DesktopIconSetter();
            desktopIconSetter.SetDesktopIcons(_cachedIcons);

            Log.WriteLine("Restored desktop icon positions from cache.");
        }

        public void Dispose()
        {
            Log.WriteLine("Disposing application...");

            ClearCache();

            SystemEvents.DisplaySettingsChanging -= BeforeDisplaySettingsChanging;
            SystemEvents.DisplaySettingsChanged -= AfterDisplaySettingsChanging;

            Log.WriteLine("");
        }

        private void ClearCache()
        {
            Log.WriteLine("Clearing cache...");

            _cachedIcons = null;

            Log.WriteLine("Cleared cache.");
        }
    }
}