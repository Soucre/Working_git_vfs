using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumDefine
{
    public enum Enumtest
    {
        VN,
        KL,
        HL
    }

    public class HelperEnumn
    {
        public static T Parse<T>(string input) {
            return (T)Enum.Parse(typeof(T), input);
        }
    }
}
