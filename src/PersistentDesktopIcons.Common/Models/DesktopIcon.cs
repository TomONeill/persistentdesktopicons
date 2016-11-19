using System.Drawing;

namespace PersistentDesktopIcons.Common.Models
{
    public class DesktopIcon
    {
        public DesktopIcon(string title, Point position)
        {
            Title = title;
            Position = position;
        }

        public string Title { get; }
        public Point Position { get; }
    }
}