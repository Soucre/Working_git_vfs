using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using System.Configuration.Install;

namespace VfsSMSEmailServiceV2
{
    [RunInstaller(true)]
    public partial class ServiceInstaller : Installer
    {
        private System.ServiceProcess.ServiceInstaller serviceInstaller1;

        public ServiceInstaller()
        {
            InitializeComponent();
        }

        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {

        }

        private void serviceProcessInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {

        }
    }
}
