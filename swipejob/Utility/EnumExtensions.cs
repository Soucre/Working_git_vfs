using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace SwipeJob.Utility
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            Type enumType = value.GetType();
            var enumValue = Enum.GetName(enumType, value);
            MemberInfo member = enumType.GetMember(enumValue)[0];

            var attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            var outString = ((DisplayAttribute)attrs[0]).Name;

            if (((DisplayAttribute)attrs[0]).ResourceType != null)
            {
                outString = ((DisplayAttribute)attrs[0]).GetName();
            }

            return outString;
        }

        // this returns a simple generic list of display names
        public static List<string> GetDisplayList<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
            return Enum.GetValues(typeof(T)).Cast<Enum>().Select(v => v.GetDisplayName()).ToList();
        }

        // for Angular, a key-value pairing is necessary, but this will return a hash that cannot be ordered:
        public static Dictionary<string, int> GetDisplayDictionary<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
            var dict = (from e in Enum.GetValues(typeof(T)).Cast<Enum>()
                        let i = Convert.ToInt32(e)
                        orderby i
                        select new
                        {
                            key = e.GetDisplayName(),
                            value = i
                        });
            return dict.ToDictionary(d => d.key, d => d.value);
        }
        
        // this returns a simple object that is easily [de]serialized and ordered
        public static List<SimpleJsObj> GetObjectList<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
            return Enum.GetValues(typeof(T)).Cast<Enum>().Select(v => new SimpleJsObj()
            {
                Name = v.ToString(),
                DisplayName = v.GetDisplayName(),
                Value = Convert.ToInt32(v)
            }).ToList();
        }

        public class SimpleJsObj
        {
            public string DisplayName { get; set; }
            public string Name { get; set; }
            public int Value { get; set; }
        }
    }
}