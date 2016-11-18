using Microsoft.Win32;
using System.Windows.Forms;

namespace PersistentDesktopIcons.Forms
{
    public partial class PersistentDesktopIconsForm : Form
    {
        public PersistentDesktopIconsForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            LaunchInBackground();

#if !DEBUG
                StartWithWindows();
#endif
        }

        private void LaunchInBackground()
        {
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
        }

        private void StartWithWindows()
        {
            var runRegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            var registerRegistration = runRegistryKey.GetValue(Application.ProductName);

            if (registerRegistration == null)
            {
                runRegistryKey.SetValue(Application.ProductName, Application.ExecutablePath.ToString());
            }
        }
    }
}