using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using Core;
using Core.Domain.Model;
using Core.Domain.Repositories;

namespace SMS
{
    public partial class Service1 : ServiceBase
    {
        private IMainThread infoThread;
        public Service1()
        {
            InitializeComponent();
            infoThread = new Information();
        }

        protected override void OnStart(string[] args)
        {
            this.Start();
        }

        protected override void OnStop()
        {
        }
        public void Start()
        {          
            infoThread.Start();
        }
    }
}
