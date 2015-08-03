using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DongY
{
    [XmlRootAttribute(ElementName = "BMI", IsNullable = false)]
    public class BMI
    {
        [XmlElement]
        public string IdBMI { get; set; }

        [XmlElement]
        public string GenderId { get; set; }

        [XmlElement]
        public string Old { get; set; }

        [XmlElement]
        public string sd3a { get; set; }

        [XmlElement]
        public string sd2a { get; set; }

        [XmlElement]
        public string sd1 { get; set; }

        [XmlElement]
        public string sd2 { get; set; }

        [XmlElement]
        public string sd3 { get; set; }

    }
}
