using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DongY
{
    [XmlRootAttribute(ElementName="Item", IsNullable= false)]
    public class Item
    {
        [XmlElement]
        public string IdItem { get; set; }

        [XmlElement]
        public string Idc { get; set; }

        [XmlElement]
        public string Name { get; set; }

        [XmlElement]
        public string Link { get; set; }

    }
}
