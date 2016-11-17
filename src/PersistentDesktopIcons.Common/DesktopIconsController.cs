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
        }

        private void InternalRun()
        {
            UpdateCache();

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
            var desktop = DesktopIconGetter.GetDesktopListView();

            if (desktop == null)
            {
                return;
            }

            if (!_cachedIcons.Any())
            {
                Debug.WriteLine("No desktop icons were cached. Therefore, no icons have been restored.");
                return;
            }

            foreach (var cachedIcon in _cachedIcons)
            {
                DesktopIconSetter.SetDesktopIcon(desktop, cachedIcon);
            }
        }

        public void Dispose()
        {
            _cachedIcons = null;
        }
    }
}