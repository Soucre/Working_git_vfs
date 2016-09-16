using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class ApiLazadaResponseBaseDto
    {
        public ApiLazadaResponseBody SuccessResponse { get; set; }
    }

    public class ApiLazadaResponseBody
    {
        public Head Head { get; set; }

        public Body Body { get; set; }
    }

    public class Head
    {
        public string RequestId { get; set; }
        public string RequestAction { get; set; }
        public string ResponseType { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class Body
    {
        public Orders Orders { get; set; }
    }

    public class Orders
    {
        public IList<Order> Order { get; set; }
    }

    public class Order
    {
        public string OrderId { get; set; }
        public string OrderNumber { get; set; }
        public OrderItems OrderItems {
            get;
            set;
        }
    }

    public class OrderItems
    {
        /// <summary>
        /// Original OrderItem Object got from Lazada's API / Chuỗi mặc định từ Lazada
        /// </summary>

        public object OrderItem { get; set; }

        public List<OrderItemDetail> OrderItemClass { get; set; }
        /// <summary>
        /// OrderItem Object Deserialized to class from Original oject above / được class từ chuỗi trên
        /// </summary>
        //public List<OrderItemDetail> OrderItemClass { get; set; }        

    }

    public class OrderItem
    {
        public object OrderItemDetail { get; set; }
    }
    public class OrderItemDetail
    {
        public string OrderItemId { get; set; }
        public string ShopId { get; set; }
        public string OrderId { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
        public string ShopSku { get; set; }
        public string ShippingType { get; set; }
        public string ItemPrice { get; set; }
        public string PaidPrice { get; set; }
        public string Currency { get; set; }
        public string WalletCredits { get; set; }
        public string TaxAmount { get; set; }
        public string ShippingAmount { get; set; }
        public string ShippingServiceCost { get; set; }
        public string VoucherAmount { get; set; }
        public string VoucherCode { get; set; }
        public string Status { get; set; }
        public string ShipmentProvider { get; set; }
        public string IsDigital { get; set; }
        public string DigitalDeliveryInfo { get; set; }
        public string TrackingCode { get; set; }
        public string TrackingCodePre { get; set; }
        public string Reason { get; set; }
        public string ReasonDetail { get; set; }
        public string PurchaseOrderId { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string PackageId { get; set; }
        public string PromisedShippingTime { get; set; }
        public string ExtraAttributes { get; set; }
        public string ShippingProviderType { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }

  
}
