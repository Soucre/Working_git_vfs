using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class UtilityMaping
    {

        /// Convert The class from Lazada object to ERP class to map
        /// By default, Result of Lazada return a lot of case with different case / 
        /// Mặc định dữ liệu Lazada return 1 đối tượng thì trả List, 2 đối tượng trở lên trả về object;
        /// <typeparam name="TData"></typeparam>string data
        /// <typeparam name="TMap"></typeparam> Entity to map
        /// <typeparam name="TResult"></typeparam> Lists return from Entity
        /// <param name="tData"></param>
        /// <param name="tResult"></param>
        public static void MappingToClass<TData, TMap, TResult>(TData tData, TResult tResult) {
            var type = tData.GetType();
            if (type == typeof(Newtonsoft.Json.Linq.JArray)) {
                var ListItemObject = JsonConvert.DeserializeObject<IList<TMap>>(tData.ToString());
                (tResult as List<TMap>).AddRange(ListItemObject);
            }
            else {
                var ItemObject = JsonConvert.DeserializeObject<TMap>(tData.ToString());
                (tResult as List<TMap>).Add(ItemObject);
            }
        }

        //public static void MappingToClass<TData, TMap>(TData tData) {
        //    var listPropertiesTResult = typeof(TMap).GetProperties();// tResult.GetType().GetProperties(); //  Get list Type of Tresult / Lấy danh sách cac Property output
        //    foreach (var item in listPropertiesTResult) {
        //        var typeItem = item.GetType();// Get Type of tMap / Lấy kiểu của Entity truyền vào


        //        var resultMapp = MapDynamic<TData, dynamic>(tData, typeItem);
        //        if (resultMapp != null) {
        //            // Mapp
        //        }



        //    }
        //}

        //private static TResult MapDynamic<TData, TResult>(TData tData, Type typeItem) {
        //    TResult tResult = (TResult)Activator.CreateInstance(typeof(TResult));
        //    if (typeItem == typeof(Newtonsoft.Json.Linq.JArray)) {
        //        var ListItemObject = JsonConvert.DeserializeObject<IList<TResult>>(tData.ToString());
        //        (tResult as List<TResult>).AddRange(ListItemObject);
        //    }
        //    else {
        //        var ItemObject = JsonConvert.DeserializeObject<TResult>(tData.ToString());
        //        (tResult as List<TResult>).Add(ItemObject);
        //    }

        //    return tResult;
        //}


    }

}
