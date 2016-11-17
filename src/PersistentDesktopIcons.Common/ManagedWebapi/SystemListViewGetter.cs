using ManagedWinapi.Windows;
using System.Diagnostics;

namespace PersistentDesktopIcons.Common.ManagedWebapi
{
    internal static class SystemListViewGetter
    {
        public static SystemListView GetSystemListView(SystemWindow systemWindow)
        {
            var systemListView = SystemListView.FromSystemWindow(systemWindow);

            if (systemListView == null)
            {
                Debug.WriteLine("There were no desktop icons.");
            }

            return systemListView;
        }
    }
}