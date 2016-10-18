using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.ProxyList
{
    public class ProxyListDto
    {
        /// <summary>
        /// IP Address
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// IP Port
        /// </summary>
        public int IPPort { get; set; }

        /// <summary>
        /// Country code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Country name
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// proxy Anonymity
        /// </summary>
        public string Anonymity { get; set; }

        /// <summary>
        /// Proxy type: https / http
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Last update time
        /// </summary>
        public string UpdatedTime { get; set; }
    }
}
