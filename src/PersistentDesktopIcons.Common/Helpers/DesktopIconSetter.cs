using PersistentDesktopIcons.Common.ManagedWebapi;
using PersistentDesktopIcons.Common.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace PersistentDesktopIcons.Common.Helpers
{
    internal class DesktopIconSetter
    {
        public static void SetDesktopIcons(List<DesktopIcon> cachedDesktopIcons)
        {
            var mainSystemWindow = MainSystemWindowGetter.GetMainSystemWindow();
            var systemListView = SystemListViewGetter.GetSystemListView(mainSystemWindow);

            Debug.WriteLine($"Cache: '{0}'", cachedDesktopIcons);

            for (int i = 0; i < cachedDesktopIcons.Count; i++)
            {
                Debug.WriteLine($"currently setting: '{0}' with position '{1}' and cache title '{2}' and positions '{3}'",
                    systemListView[i].Title,
                    systemListView[i].Position,
                    cachedDesktopIcons[i].Title,
                    cachedDesktopIcons[i].Position
                );
                if (systemListView[i].Title == cachedDesktopIcons[i].Title)
                {
                    systemListView[i].Position = cachedDesktopIcons[i].Position;
                }
            }
        }
    }
}