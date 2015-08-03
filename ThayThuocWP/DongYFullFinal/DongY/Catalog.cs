using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DongY
{
    [XmlRootAttribute(ElementName = "Catalog", IsNullable = false)]
    public class Catalog
    {
        [XmlElement]
        public string Idc { get; set; }

        [XmlElement]
        public string Name { get; set; }
        
        [XmlElement]
        public string Note { get; set; }

    }
}
