using ManagedWinapi.Windows;
using PersistentDesktopIcons.Common.Logger;

namespace PersistentDesktopIcons.Common.ManagedWebapi
{
    internal static class SystemListViewGetter
    {
        public static SystemListView GetSystemListView(SystemWindow systemWindow)
        {
            var systemListView = SystemListView.FromSystemWindow(systemWindow);

            if (systemListView == null)
            {
                Log.WriteLine("There were no desktop icons.");
            }

            return systemListView;
        }
    }
}