using PersistentDesktopIcons.Common.ManagedWebapi;
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
            var mainSystemWindow = MainSystemWindowGetter.GetMainSystemWindow();
            var systemListView = SystemListViewGetter.GetSystemListView(mainSystemWindow);

            var desktopIcons = new List<DesktopIcon>();

            for (var i = 0; i < systemListView.Count; i++)
            {
                var iconTitle = systemListView[i].Title;
                var iconPosition = systemListView[i].Position;

                if (desktopIcons.Any(d => d.Title == iconTitle))
                {
                    Debug.WriteLine("Duplicate icon name found: '{0}'", iconTitle);

                    continue;
                }

                var desktopIcon = new DesktopIcon
                {
                    Title = iconTitle,
                    Position = iconPosition
                };

                desktopIcons.Add(desktopIcon);

                Debug.WriteLine("Desktop icon count: '{0}'. Current icon Title: '{1}', pos (x): '{2}', pos (y): '{3}'",
                    desktopIcons.Count,
                    desktopIcon.Title,
                    desktopIcon.Position.X,
                    desktopIcon.Position.Y
                );
            }

            return desktopIcons;
        }
    }
}