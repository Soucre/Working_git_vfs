using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLTBalancer
{
    public sealed class LoadBalancer
    {
        // Static members are 'eagerly initialized', that is, 
        // immediately when class is loaded for the first time.
        // .NET guarantees thread safety for static initialization
        private static readonly LoadBalancer _instance = new LoadBalancer() 
        {
        
        };


        // Type-safe generic list of servers
        private List<Server> _server;
        private Random _random = new Random();

        // Contructor is private

        private LoadBalancer()
        {
            // Load list server IP
            _server = new List<Server> 
            {
                 new Server{ Name = "ServerI", IP = "120.14.220.18" },
                 new Server{ Name = "ServerII", IP = "120.14.220.19" },
                 new Server{ Name = "ServerIII", IP = "120.14.220.20" },
                 new Server{ Name = "ServerIV", IP = "120.14.220.21" },
                 new Server{ Name = "ServerV", IP = "120.14.220.22" }
            };

        }

        public static LoadBalancer GetLoadBalancer()
        {
            return _instance;
        }

        public Server NextServer
        {
            get
            {
                int r = _random.Next(_server.Count);
                return _server[r];
            }
        }

    }

    public class Server
    {
        /// <summary>
        /// Name of server / ten server
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// IP of server / IP server
        /// </summary>
        public string IP { get; set; }
    }
}
