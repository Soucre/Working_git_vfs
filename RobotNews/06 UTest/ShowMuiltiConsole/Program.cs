using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowMuiltiConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 20; i++) {
                using (var process1 = new Process()) {
                    process1.StartInfo.FileName = @"E:\Working Vfs\Working_git_vfs\RobotNews\ConsoleApplication1\bin\Debug\ConsoleApplication1.exe";
                    process1.Start();
                }
            }

        }
    }
}
