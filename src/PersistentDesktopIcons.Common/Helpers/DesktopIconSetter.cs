using PersistentDesktopIcons.Common.ManagedWebapi;
using PersistentDesktopIcons.Common.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace PersistentDesktopIcons.Common.Helpers
{
    internal class DesktopIconSetter
    {
        public static void SetDesktopIcons(List<DesktopIcon> cachedDesktopIcons)
        {
            var mainSystemWindow = MainSystemWindowGetter.GetMainSystemWindow();
            var systemListView = SystemListViewGetter.GetSystemListView(mainSystemWindow);

            Debug.WriteLine("Cached icons: '{0}'", cachedDesktopIcons.Count);

            for (int i = 0; i < cachedDesktopIcons.Count; i++)
            {
                Debug.WriteLine("Current icon: '{0}' with position '{1}'. Cached icon: '{2}' with position '{3}'",
                    systemListView[i].Title,
                    systemListView[i].Position,
                    cachedDesktopIcons[i].Title,
                    cachedDesktopIcons[i].Position
                );

                if (AreCachedAndActualTitleEqual(systemListView[i].Title, cachedDesktopIcons[i].Title)
                 && !AreCachedAndActualPositionsEqual(systemListView[i].Position, cachedDesktopIcons[i].Position))
                {
                    systemListView[i].Position = cachedDesktopIcons[i].Position;
                }
            }
        }

        private static bool AreCachedAndActualTitleEqual(string actual, string cached)
        {
            return actual == cached;
        }

        private static bool AreCachedAndActualPositionsEqual(Point actual, Point cached)
        {
            return actual.X == cached.X
                && actual.Y == cached.Y;
        }
    }
}