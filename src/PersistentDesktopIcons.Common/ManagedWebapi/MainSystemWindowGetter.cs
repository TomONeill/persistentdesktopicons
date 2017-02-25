using ManagedWinapi.Windows;
using PersistentDesktopIcons.Common.Logger;

namespace PersistentDesktopIcons.Common.ManagedWebapi
{
    internal static class MainSystemWindowGetter
    {
        public static SystemWindow GetMainSystemWindow()
        {
            var mainSystemWindow = SystemWindow.FilterToplevelWindows(delegate (SystemWindow systemWindow)
            {
                return systemWindow.ClassName == "Progman"
                    && systemWindow.Title == "Program Manager"
                    && systemWindow.Process.ProcessName == "explorer";
            });

            if (mainSystemWindow.Length != 1)
            {
                Log.WriteLine("Unable to find the desktop.");

                return null;
            }

            mainSystemWindow = mainSystemWindow[0].FilterDescendantWindows(false, delegate (SystemWindow systemWindow)
            {
                return systemWindow.ClassName == "SysListView32" && systemWindow.Title == "FolderView";
            });

            if (mainSystemWindow.Length != 1)
            {
                Log.WriteLine("Unable to find the desktop.");

                return null;
            }

            return mainSystemWindow[0];
        }
    }
}