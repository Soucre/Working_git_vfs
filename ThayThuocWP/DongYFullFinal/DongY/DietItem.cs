using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DongY
{
    [XmlRootAttribute(ElementName = "CheDoAn", IsNullable = false)]
    public class DietItem
    {
        [XmlElement]
        public string id { get; set; }

        [XmlElement]
        public string Name { get; set; }

        [XmlElement]
        public string URL { get; set; }

    }
}
