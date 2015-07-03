using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace VfsSMSEmailServiceV2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //if (System.Diagnostics.Debugger.IsAttached)
            //{
            //    // We are running with a debugger attached, so start
            //    // the service directly.
            //    MainThread service = new MainThread();
            //    string[] args = new string[] { "arg1", "arg2" };
            //    service.Start();
            //}
            //else
            //{
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
			{ 
				new InformationService()
			};
                ServiceBase.Run(ServicesToRun);
            //}

        }
    }
}
