using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace VfsCustomerInformationServices
{
    public partial class InformationService : ServiceBase
    {
        private IMainThread infoThread;

        public InformationService()
        {
            InitializeComponent();
            infoThread = new Information();

        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            this.Start();
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            infoThread.Stop();
        }

        public void Start()
        {
            infoThread.Start();
        }
    }
}
