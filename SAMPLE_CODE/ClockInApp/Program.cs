using AnhCop.AutoLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockInApp
{
    class Program
    {
        static void Main(string[] args)
        {         
            if (AppHelper.ClockStatus == "I")
                Clock_In_System(); // Login System
            else
                Clock_Out_System(); // Logout System
        }

        private static void Clock_In_System()
        {
            using (var summit = new RobotAuto()) {
                summit.DomainName = AppHelper.DomainName;
                summit.SubDomain = AppHelper.SubDomain;
                summit.Com_code = AppHelper.Com_code;
                summit.IdLogin = AppHelper.IdLogin;
                summit.Password = AppHelper.Password;
                summit.InSelHour = AppHelper.InSelHour;
                summit.InSelMin = AppHelper.SelMin;
                summit.InSelAm = AppHelper.InSelAm;
                summit.HidSelDate = DateTime.Now.ToString("yyyy-MM-dd");

                var reusltString = summit.SumitClockIn();

                Console.WriteLine(reusltString);
                Console.ReadLine();
            }
        }
        private static void Clock_Out_System()
        {
            using (var summit = new RobotAuto()) {             

                var reusltString = summit.SumitClockOut();

                Console.WriteLine(reusltString);
                Console.ReadLine();
            }
        }
    }
}
