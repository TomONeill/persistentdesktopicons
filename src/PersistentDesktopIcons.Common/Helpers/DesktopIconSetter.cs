using PersistentDesktopIcons.Common.Logger;
using PersistentDesktopIcons.Common.ManagedWebapi;
using PersistentDesktopIcons.Common.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ManagedWinapi.Windows;

namespace PersistentDesktopIcons.Common.Helpers
{
    internal class DesktopIconSetter
    {
        private static SystemListView _systemListView;

        public DesktopIconSetter()
        {
            var mainSystemWindow = MainSystemWindowGetter.GetMainSystemWindow();
            _systemListView = SystemListViewGetter.GetSystemListView(mainSystemWindow);
        }

        public void SetDesktopIcons(List<DesktopIcon> cachedDesktopIcons)
        {
            Log.WriteLine("Cached icons: '{0}'.", cachedDesktopIcons.Count);

            for (int i = 0; i < _systemListView.Count; i++)
            {
                var desktopIcon = _systemListView[i];
                var cachedIcon = GetItemFromCache(cachedDesktopIcons, desktopIcon.Title);

                if (cachedIcon == null)
                {
                    Log.WriteLine("Icon '{0}' has not been cached. Skipping...", desktopIcon.Title);

                    continue;
                }

                Log.WriteLine("Comparing desktop icon '{0}' with positions '{1}' with cached icon '{2}' with positions '{3}'.",
                    desktopIcon.Title,
                    desktopIcon.Position,
                    cachedIcon.Title,
                    cachedIcon.Position
                );

                if (!AreActualAndCachedPositionsEqual(desktopIcon.Position, cachedIcon.Position))
                {
                    Log.WriteLine("Moving desktop icon '{0}' back to it's original location.", desktopIcon.Title);

                    // Use the local variable instead of a reference.
                    _systemListView[i].Position = cachedIcon.Position;
                }
                else
                {
                    Log.WriteLine("Desktop icon '{0}' has not changed location.", desktopIcon.Title);
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