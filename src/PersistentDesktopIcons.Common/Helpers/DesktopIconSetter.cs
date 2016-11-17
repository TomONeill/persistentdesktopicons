using ManagedWinapi.Windows;
using PersistentDesktopIcons.Common.Models;

namespace PersistentDesktopIcons.Common.Helpers
{
    internal class DesktopIconSetter
    {
        public static void SetDesktopIcon(SystemListView desktop, DesktopIcon desktopIcon)
        {
            for (int i = 0; i < desktop.Count; i++)
            {
                if (desktop[i].Title == desktopIcon.Title)
                {
                    desktop[i].Position = desktopIcon.Position;
                    return;
                }
            }
        }
    }
}