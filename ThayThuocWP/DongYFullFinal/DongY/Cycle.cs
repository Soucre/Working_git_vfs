using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DongY
{
    [XmlRootAttribute(ElementName = "Cycle", IsNullable = true)]
    class Cycle
    {
        [XmlElement]
        public string id { get; set; }

        [XmlElement]
        public string chuky { get; set; }

        [XmlElement]
        public string songaykinh { get; set; }
        
        [XmlElement]
        public string ngaybatdau { get; set; }

        [XmlElement]
        public string thoigian { get; set; }
    }
}
