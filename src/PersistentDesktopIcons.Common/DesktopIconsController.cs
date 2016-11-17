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
            thread.Name = "DesktopIconRestoreProcessor.InternalRun()";
            thread.Start();

            SystemEvents.DisplaySettingsChanging += (s, e) =>
            {
                Debug.WriteLine("Display settings is going to change. Caching desktop icon positions...");
                UpdateCache();
            };

            SystemEvents.DisplaySettingsChanged += (s, e) =>
            {
                Debug.WriteLine("Display settings changed");
                RestoreDesktopIcons();
            };
        }

        private void InternalRun()
        {
            while (true)
            {
                Thread.Sleep(1000);
            }
        }

        public void UpdateCache()
        {
            _cachedIcons = DesktopIconGetter.GetDesktopIcons();
        }

        public void RestoreDesktopIcons()
        {
            var desktopIconTitles = DesktopIconGetter.GetDesktopIconTitles();

            if (desktopIconTitles == null)
            {
                return;
            }

            if (!_cachedIcons.Any())
            {
                Debug.WriteLine("No desktop icons were cached. Therefore, no icons have been restored.");
                return;
            }

            DesktopIconSetter.SetDesktopIcons(desktopIconTitles, _cachedIcons);
        }

        public void Dispose()
        {
            _cachedIcons = null;
        }
    }
}