using Microsoft.Win32;
using PersistentDesktopIcons.Common.Helpers;
using PersistentDesktopIcons.Common.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace PersistentDesktopIcons.Common
{
    public class DesktopIconsController : IDisposable
    {
        private List<DesktopIcon> _cachedIcons;

        public void Start()
        {
            var thread = new Thread(InternalRun);
            thread.IsBackground = true;
            thread.Name = "DesktopIconsRestoreController.InternalRun()";
            thread.Start();

            SystemEvents.DisplaySettingsChanging += BeforeDisplaySettingsChanging;
            SystemEvents.DisplaySettingsChanged += AfterDisplaySettingsChanging;
        }

        private void InternalRun()
        {
            while (true)
            {
                Thread.Sleep(1000);
            }
        }

        private void BeforeDisplaySettingsChanging(object sender, EventArgs e)
        {
            Debug.WriteLine("Display settings are going to change.");

            UpdateCache();
        }

        private void AfterDisplaySettingsChanging(object sender, EventArgs e)
        {
            Debug.WriteLine("Display settings have changed");

            RestoreDesktopIcons();
        }

        private void UpdateCache()
        {
            Debug.WriteLine("Caching desktop icon positions...");

            _cachedIcons = DesktopIconGetter.GetDesktopIcons();
        }

        private void RestoreDesktopIcons()
        {
            Debug.WriteLine("Restoring desktop icon positions from cache...");

            if (!_cachedIcons.Any())
            {
                Debug.WriteLine("No desktop icons were cached. Therefore, no icons have been restored.");

                return;
            }

            DesktopIconSetter.SetDesktopIcons(_cachedIcons);
        }

        public void Dispose()
        {
            Debug.WriteLine("Disposing application...");

            ClearCache();

            SystemEvents.DisplaySettingsChanging -= BeforeDisplaySettingsChanging;
            SystemEvents.DisplaySettingsChanged -= AfterDisplaySettingsChanging;
        }

        private void ClearCache()
        {
            Debug.WriteLine("Clearing cache...");

            _cachedIcons = null;
        }
    }
}