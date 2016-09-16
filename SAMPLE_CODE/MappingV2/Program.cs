using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingV2
{
    class Program
    {
        private const string WellFormedJson = "{ id : 1, customer : 'Joe Black', items : [ { id : 1, description:'One', unit_price: 1.00, quantity: 1}, { id : 2, description:'Two', unit_price: 2.00, quantity: 2}, { id : 3, description:'Three', unit_price: 3.00, quantity: 3} ] } ";

        private const string PoorlyFormedJson = "{ id : 1, customer : 'Joe Black', items : { id : 1, description:'One', unit_price: 1.00, quantity: 1} } ";

        static void Main(string[] args) {
            DeserializeWellFormedJson();
            DeserializePoorlyFormedJson();
        }

        public static void DeserializeWellFormedJson() {
            var objString = WellFormedJson;
            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var order = JsonConvert.DeserializeObject<Order>(objString, settings);

        }


        //[ExpectedException(typeof(Newtonsoft.Json.JsonSerializationException),"Looks like it's working properly now!")]
        public static void DeserializePoorlyFormedJson() {
            var objString = PoorlyFormedJson;
            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DateFormatHandling = DateFormatHandling.IsoDateFormat };
            var order = JsonConvert.DeserializeObject<Order>(objString, settings);

        }
    }

    public class Order
    {
        public int id;
        public string customer;

        [JsonConverter(typeof(SingleValueArrayConverter<OrderItem>))]
        public IList<OrderItem> items;
    }

    public class OrderItem
    {
        public int id;
        public string description;
        public double unit_price;
        public double quantity;
    }

}
