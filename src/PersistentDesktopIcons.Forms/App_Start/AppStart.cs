using PersistentDesktopIcons.Common;
using System;
using System.Windows.Forms;

namespace PersistentDesktopIcons.Forms
{
    internal static class AppStart
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            new DesktopIconsController().Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PersistentDesktopIconsForm());
        }
    }
}