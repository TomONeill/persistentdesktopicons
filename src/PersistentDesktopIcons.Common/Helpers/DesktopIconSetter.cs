using PersistentDesktopIcons.Common.Logger;
using PersistentDesktopIcons.Common.ManagedWebapi;
using PersistentDesktopIcons.Common.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace PersistentDesktopIcons.Common.Helpers
{
    internal class DesktopIconSetter
    {
        public static void SetDesktopIcons(List<DesktopIcon> cachedDesktopIcons)
        {
            var mainSystemWindow = MainSystemWindowGetter.GetMainSystemWindow();
            var systemListView = SystemListViewGetter.GetSystemListView(mainSystemWindow);

            Log.WriteLine("Cached icons count: '{0}'", cachedDesktopIcons.Count);

            for (int i = 0; i < systemListView.Count; i++)
            {
                var desktopIcon = systemListView[i];
                var cachedIcon = GetItemFromCache(cachedDesktopIcons, systemListView[i].Title);

                Log.WriteLine("Current icon: '{0}' with position '{1}'. Cached icon: '{2}' with position '{3}'",
                    desktopIcon.Title,
                    desktopIcon.Position,
                    cachedIcon.Title,
                    cachedIcon.Position
                );

                if (!AreActualAndCachedPositionsEqual(desktopIcon.Position, cachedIcon.Position))
                {
                    // Use the local variable instead of a reference.
                    systemListView[i].Position = cachedIcon.Position;
                }
                else
                {
                    Log.WriteLine("Positions of the desktop icon '{0}' matched with cached icon '{1}'.",
                        desktopIcon.Title,
                        cachedIcon.Title
                    );
                }
            }
        }

        private static DesktopIcon GetItemFromCache(List<DesktopIcon> cachedDesktopIcons, string iconTitle)
        {
            return cachedDesktopIcons.Single(c => c.Title == iconTitle);
        }

        private static bool AreActualAndCachedPositionsEqual(Point actual, Point cached)
        {
            return actual.X == cached.X
                && actual.Y == cached.Y;
        }
    }
}