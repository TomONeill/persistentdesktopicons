using ManagedWinapi.Windows;
using PersistentDesktopIcons.Common.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PersistentDesktopIcons.Common.Helpers
{
    internal class DesktopIconGetter
    {
        public static List<DesktopIcon> GetDesktopIcons()
        {
            var desktop = GetDesktopListView();

            if (desktop == null)
            {
                return null;
            }

            var desktopIcons = new List<DesktopIcon>();

            for (int i = 0; i < desktop.Count; i++)
            {
                if (desktopIcons.Any(d => d.Title == desktop[i].Title))
                {
                    Debug.WriteLine($"Duplicate icon name found: '{0}'", desktop[i].Title);
                }

                var desktopIcon = new DesktopIcon
                {
                    Title = desktop[i].Title,
                    Position = desktop[i].Position
                };

                desktopIcons.Add(desktopIcon);
            }

            return desktopIcons;
        }

        public static SystemListView GetDesktopListView()
        {
            var mainSystemWindow = SystemWindow.FilterToplevelWindows(delegate (SystemWindow systemWindow)
            {
                return systemWindow.ClassName == "Progman"
                    && systemWindow.Title == "Program Manager"
                    && systemWindow.Process.ProcessName == "explorer";
            });

            if (mainSystemWindow.Length != 1)
            {
                Debug.WriteLine("Unable to find the desktop.");
                return null;
            }

            mainSystemWindow = mainSystemWindow[0].FilterDescendantWindows(false, delegate (SystemWindow systemWindow)
            {
                return systemWindow.ClassName == "SysListView32"
                    && systemWindow.Title == "FolderView";
            });

            if (mainSystemWindow.Length != 1)
            {
                Debug.WriteLine("Unable to find the desktop.");
                return null;
            }

            var systemListView = SystemListView.FromSystemWindow(mainSystemWindow[0]);

            if (systemListView == null)
            {
                Debug.WriteLine("There were no desktop icons.");
            }

            return systemListView;
        }
    }
}