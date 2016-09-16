using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegexSample
{
    public class EnumUserRight: EnumBase<EnumUserRight>
    {
        public static EnumUserRight Add = Define("400");

        public static EnumUserRight Edit = Define("200");

        public static EnumUserRight Remove = Define("10");

    }
    
}
