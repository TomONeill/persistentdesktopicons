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

            var desktopIconTitles = mainSystemWindow.Content.PropertyList;
            var desktopIcons = new List<DesktopIcon>();
            const int firstDesktopIcon = 3;

            for (var i = firstDesktopIcon; i < desktopIconTitles.Count; i++)
            {
                var iconTitle = desktopIconTitles.ElementAt(i).Value;

                if (desktopIcons.Any(d => d.Title == iconTitle))
                {
                    Debug.WriteLine($"Duplicate icon name found: '{0}'", iconTitle);
                    continue;
                }

                var desktopIcon = new DesktopIcon
                {
                    Title = iconTitle,
                    Position = systemListView[i - 3].Position
                };

                desktopIcons.Add(desktopIcon);
            }

            return desktopIcons;
        }

        public static List<string> GetDesktopIconTitles()
        {
            var mainSystemWindow = MainSystemWindowGetter.GetMainSystemWindow();
            var desktopIconTitles = mainSystemWindow.Content.PropertyList;
            const int firstFewNonDesktopIcons = 3;

            for (var i = 0; i < firstFewNonDesktopIcons; i++)
            {
                desktopIconTitles.Remove(desktopIconTitles.ElementAt(i).Key);
            }

            return desktopIconTitles.Select(t => t.Value).ToList();
        }
    }
}