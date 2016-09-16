using MappingV2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubmitToApi
{
    public class EntityOrders
    {
        public class Head
        {
            public string RequestId { get; set; }
            public string RequestAction { get; set; }
            public string ResponseType { get; set; }
            public string Timestamp { get; set; }
            public string TotalCount { get; set; }
        }

        public class AddressBilling
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }
            public string Phone2 { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Address3 { get; set; }
            public string Address4 { get; set; }
            public string Address5 { get; set; }
            public string CustomerEmail { get; set; }
            public string City { get; set; }
            public string Ward { get; set; }
            public string Region { get; set; }
            public string PostCode { get; set; }
            public string Country { get; set; }
        }

        public class AddressShipping
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }
            public string Phone2 { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Address3 { get; set; }
            public string Address4 { get; set; }
            public string Address5 { get; set; }
            public string CustomerEmail { get; set; }
            public string City { get; set; }
            public string Ward { get; set; }
            public string Region { get; set; }
            public string PostCode { get; set; }
            public string Country { get; set; }
        }


        public class Order
        {
            public string OrderId { get; set; }
            public string CustomerFirstName { get; set; }
            public string CustomerLastName { get; set; }
            public string OrderNumber { get; set; }
            public string PaymentMethod { get; set; }
            public string Remarks { get; set; }
            public string DeliveryInfo { get; set; }
            public string Price { get; set; }
            public string GiftOption { get; set; }
            public string GiftMessage { get; set; }
            public string VoucherCode { get; set; }
            public string CreatedAt { get; set; }
            public string UpdatedAt { get; set; }
            public string AddressUpdatedAt { get; set; }
            public AddressBilling AddressBilling { get; set; }
            public AddressShipping AddressShipping { get; set; }
            public string NationalRegistrationNumber { get; set; }
            public string ItemsCount { get; set; }
            public string PromisedShippingTime { get; set; }
            public string ExtraAttributes { get; set; }
            public Statuses Statuses { get; set; }
        }


        public class Statuses
        {
            [JsonConverter(typeof(SingleValueArrayConverter<string>))]
            public IList<string> Status { get; set; }
        }

        public class Orders
        {
            public List<Order> Order { get; set; }
        }

        public class Body
        {
            public Orders Orders { get; set; }
        }

        public class SuccessResponse
        {
            public Head Head { get; set; }
            public Body Body { get; set; }
        }

        public class RootObject
        {
            public SuccessResponse SuccessResponse { get; set; }
        }
    }
}
