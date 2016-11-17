using ManagedWinapi.Windows;
using PersistentDesktopIcons.Common.ManagedWebapi;
using PersistentDesktopIcons.Common.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace PersistentDesktopIcons.Common.Helpers
{
    internal class DesktopIconSetter
    {
        public static void SetDesktopIcons(List<string> desktopIconTitles, List<DesktopIcon> cachedDesktopIcons)
        {
            var mainSystemWindow = MainSystemWindowGetter.GetMainSystemWindow();
            var systemListView = SystemListViewGetter.GetSystemListView(mainSystemWindow);

            for (int i = 0; i < cachedDesktopIcons.Count; i++)
            {
                if (desktopIconTitles[i] == cachedDesktopIcons[i].Title)
                {
                    systemListView[i].Position = cachedDesktopIcons[i].Position;
                }
            }
        }
    }
}