using PersistentDesktopIcons.Common.Logger;
using PersistentDesktopIcons.Common.ManagedWebapi;
using PersistentDesktopIcons.Common.Models;
using System.Collections.Generic;
using System.Linq;
using ManagedWinapi.Windows;

namespace PersistentDesktopIcons.Common.Helpers
{
    internal class DesktopIconGetter
    {
        private static SystemListView _systemListView;

        public DesktopIconGetter()
        {
            var mainSystemWindow = MainSystemWindowGetter.GetMainSystemWindow();
            _systemListView = SystemListViewGetter.GetSystemListView(mainSystemWindow);
        }

        public List<DesktopIcon> GetDesktopIcons()
        {
            var desktopIcons = new List<DesktopIcon>();

            for (var i = 0; i < _systemListView.Count; i++)
            {
                var iconTitle = _systemListView[i].Title;
                var iconPosition = _systemListView[i].Position;

                if (desktopIcons.Any(d => d.Title == iconTitle))
                {
                    Log.WriteLine("Duplicate icon name found: '{0}'.", iconTitle);

                    continue;
                }

                var desktopIcon = new DesktopIcon(iconTitle, iconPosition);

                desktopIcons.Add(desktopIcon);

                Log.WriteLine("Desktop icon count: '{0}'. Getting desktop icon '{1}' with positions '{2}'.",
                    desktopIcons.Count,
                    desktopIcon.Title,
                    desktopIcon.Position
                );
            }

            return desktopIcons;
        }
    }
}