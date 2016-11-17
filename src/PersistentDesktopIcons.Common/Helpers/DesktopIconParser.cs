using PersistentDesktopIcons.Common.Models;
using System.Drawing;

namespace PersistentDesktopIcons.Common.Helpers
{
    internal class DesktopIconParser
    {
        public DesktopIcon Parse(string icon)
        {
            var desktopIcon = new DesktopIcon();

            string[] fields = icon.Split(':');
            desktopIcon.Title = fields[0];
            desktopIcon.Position = new Point(int.Parse(fields[1]), int.Parse(fields[2]));

            return desktopIcon;
        }
    }
}