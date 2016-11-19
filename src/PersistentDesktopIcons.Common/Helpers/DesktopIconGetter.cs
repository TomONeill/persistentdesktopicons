using PersistentDesktopIcons.Common.Logger;
using PersistentDesktopIcons.Common.ManagedWebapi;
using PersistentDesktopIcons.Common.Models;
using System.Collections.Generic;
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
                    Log.WriteLine("Duplicate icon name found: '{0}'", iconTitle);

                    continue;
                }

                var desktopIcon = new DesktopIcon(iconTitle, iconPosition);

                desktopIcons.Add(desktopIcon);

                Log.WriteLine("Desktop icon count: '{0}'. Getting desktop icon '{1}' with positions '{2}'",
                    desktopIcons.Count,
                    desktopIcon.Title,
                    desktopIcon.Position
                );
            }

            return desktopIcons;
        }
    }
}