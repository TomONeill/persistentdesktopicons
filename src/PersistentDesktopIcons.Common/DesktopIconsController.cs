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
        private const long MaxLogSizeInKiloByte = 1024;

        public void Start()
        {
#if !DEBUG
            Log.ClearIfLargerThan(MaxLogSizeInKiloByte);
#endif
            Log.WriteLine("Current run: {0}", DateTime.Now);

            var thread = new Thread(InternalRun);
            thread.IsBackground = true;
            thread.Name = "DesktopIconsRestoreController.InternalRun()";
            thread.Start();

            //SystemEvents.DisplaySettingsChanging += BeforeDisplaySettingsChanging;
            SystemEvents.DisplaySettingsChanged += AfterDisplaySettingsChanging;
        }

        private void InternalRun()
        {
            CacheDesktopIcons();

            while (true)
            {
                Thread.Sleep(1000);
            }
        }

        //private void BeforeDisplaySettingsChanging(object sender, EventArgs e)
        //{
        //    Log.WriteLine("Display settings are going to change.");

        //    CacheDesktopIcons();
        //}

        private void AfterDisplaySettingsChanging(object sender, EventArgs e)
        {
            Log.WriteLine("Display settings have changed");

            RestoreDesktopIcons();
        }

        private void CacheDesktopIcons()
        {
            Log.WriteLine("Caching desktop icon positions...");

            _cachedIcons = DesktopIconGetter.GetDesktopIcons();
        }

        private void RestoreDesktopIcons()
        {
            Log.WriteLine("Restoring desktop icon positions from cache...");

            if (!_cachedIcons.Any())
            {
                Log.WriteLine("No desktop icons were cached. Therefore, no icons have been restored.");

                return;
            }

            DesktopIconSetter.SetDesktopIcons(_cachedIcons);
        }

        public void Dispose()
        {
            Log.WriteLine("Disposing application...");

            ClearCache();

            //SystemEvents.DisplaySettingsChanging -= BeforeDisplaySettingsChanging;
            SystemEvents.DisplaySettingsChanged -= AfterDisplaySettingsChanging;

            Log.WriteLine("");
        }

        private void ClearCache()
        {
            Log.WriteLine("Clearing cache...");

            _cachedIcons = null;
        }
    }
}