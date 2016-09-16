//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace EnumDefine
//{
//    public class EnumBase<TEnum>
//    {

//        public static TEnum Define(object value, bool bDefault = false)
//        {
//            string output = null;
//            Type type = value.GetType();

//            //Check first in our cached results...
//            if (_stringValues.ContainsKey(value))
//                output = (_stringValues[value] as StringValueAttribute).Value;
//            else {
//                //Look for our 'StringValueAttribute' 
//                //in the field's custom attributes
//                FieldInfo fi = type.GetField(value.ToString());
//                StringValueAttribute[] attrs =
//                   fi.GetCustomAttributes(typeof(StringValueAttribute),
//                                           false) as StringValueAttribute[];
//                if (attrs.Length > 0) {
//                    _stringValues.Add(value, attrs[0]);
//                    output = attrs[0].Value;
//                }
//            }

//            return output;
//        }

      
//    }
//    public class EnumBase<TEnum>

//    {

//    }

//    public class StringValueAttribute : System.Attribute
//    {

//        private string _value;

//        public StringValueAttribute(string value) {
//            _value = value;
//        }

//        public string Value {
//            get { return _value; }
//        }

//    }
//}
