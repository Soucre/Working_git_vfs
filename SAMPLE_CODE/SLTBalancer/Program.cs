using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLTBalancer
{
    class Program
    {
        static void Main(string[] args)
        {
            //var lb1 = LoadBalancer.GetLoadBalancer();
            //var lb2 = LoadBalancer.GetLoadBalancer();
            //var lb3 = LoadBalancer.GetLoadBalancer();

            //if (lb1 == lb2 &&  lb2 == lb3 && lb1 == lb3) {
            //    Console.WriteLine("Same instance\n");
            //}

            // Next, load balance 15 requests for a server

            var balancer = LoadBalancer.GetLoadBalancer();
            for (int i = 0; i < 15; i++) {
                string serverName = balancer.NextServer.Name;
                Console.WriteLine("Dispatch request to: " + serverName);
            }
        }
    }
}
