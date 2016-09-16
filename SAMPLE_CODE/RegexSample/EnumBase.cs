using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexSample
{
    public class EnumBase<TEnum> where TEnum : EnumBase<TEnum>, new()
    {
        public string Value { get; protected set; }

        private static ConcurrentDictionary<Type, object> s_mapping = new ConcurrentDictionary<Type, object>();

        public static TEnum Define(object value) {
            Type enumType = typeof(TEnum);

            var enumObj = new TEnum();
            enumObj.Value = value.ToString();

            var enumSet = new Dictionary<string, object>();
            lock (enumSet) {
                enumSet[enumObj.Value] = enumObj;
            }

            return enumObj;
        }
    }
}
