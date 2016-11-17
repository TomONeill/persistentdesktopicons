using PersistentDesktopIcons.Common;
using System;
using System.ServiceProcess;

namespace PersistentDesktopIcons.Service
{
    public partial class PersistentDesktopIconsService : ServiceBase
    {
        public PersistentDesktopIconsService()
        {
            InitializeComponent();
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        [STAThread]
        protected override void OnStart(string[] args)
        {
            new DesktopIconsController().Start();
        }

        protected override void OnStop()
        {
        }
    }
}