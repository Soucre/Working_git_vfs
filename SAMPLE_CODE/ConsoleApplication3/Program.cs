using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] filterOption = { "color", "" };
            string prod_opt = "{color:\"red\", isGift:\"true\"}";

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            //return serializer.Deserialize(json, objectType);

            var result = (Dictionary<string, object>)serializer.Deserialize(prod_opt, typeof(Dictionary<string, object>));

            foreach (var item in result) {
                if (filterOption.Contains(item.Key)) {
                    
                }
            }
        }
    }


}
