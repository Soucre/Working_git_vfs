﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace VfsCustomerInformationServices
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if(!DEBUG)
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new InformationService()
			};
            ServiceBase.Run(ServicesToRun);

#else
            Information test = new Information();
                        test.Execute();
#endif

        }
    }
}
