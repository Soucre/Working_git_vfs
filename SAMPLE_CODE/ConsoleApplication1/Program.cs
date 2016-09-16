using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace ConsoleApplication1
{
    class Program 
    {
        static void Main(string[] args) {



            #region MyObject
            var myObject = @"{
    'SuccessResponse': {
        'Head': {
            'RequestId': '',
            'RequestAction': 'GetMultipleOrderItems',
            'ResponseType': 'Orders',
            'Timestamp': '2016-07-27T18:56:22+0700'
        },
        'Body': {
            'Orders': {
                'Order': [
                    {
                        'OrderId': '6178295',
                        'OrderNumber': '382566752',
                        'OrderItems': {
                            'OrderItem': {
                                'OrderItemId': '7248403',
                                'ShopId': '10904648',
                                'OrderId': '6178295',
                                'Name': 'Phần mềm Ecount ERP Quản trị doanh nghiệp hiệu quả',
                                'Sku': 'EC7VC',
                                'ShopSku': 'NO007VCAA1IMJWVNAMZ-2459159',
                                'ShippingType': 'Dropshipping',
                                'ItemPrice': '1155000.00',
                                'PaidPrice': '1155000.00',
                                'Currency': 'VND',
                                'WalletCredits': '0.00',
                                'TaxAmount': '105545.45',
                                'ShippingAmount': '6000.00',
                                'ShippingServiceCost': '',
                                'VoucherAmount': '0',
                                'VoucherCode': '',
                                'Status': 'canceled',
                                'ShipmentProvider': 'LEX',
                                'IsDigital': '0',
                                'DigitalDeliveryInfo': '',
                                'TrackingCode': 'LMPDS-382566752-4824',
                                'TrackingCodePre': '',
                                'Reason': 'venture_invalid_-_customer_unreachable',
                                'ReasonDetail': '',
                                'PurchaseOrderId': '5681386',
                                'PurchaseOrderNumber': 'MPDS-M163043276936607435456',
                                'PackageId': '',
                                'PromisedShippingTime': '',
                                'ExtraAttributes': '',
                                'ShippingProviderType': 'standard',
                                'CreatedAt': '2016-07-27 11:31:07',
                                'UpdatedAt': '2016-07-27 16:33:02'
                            }
                        }
                    },
                    {
                        'OrderId': '6178890',
                        'OrderNumber': '386196752',
                        'OrderItems': {
                            'OrderItem': {
                                'OrderItemId': '7249319',
                                'ShopId': '10905996',
                                'OrderId': '6178890',
                                'Name': 'TPHCM - Đào tạo và tư vấn phần mềm Ecount ERP',
                                'Sku': 'EC1TN',
                                'ShopSku': 'NO007VCAA1IMK1VNAMZ-2459166',
                                'ShippingType': 'Dropshipping',
                                'ItemPrice': '650000.00',
                                'PaidPrice': '650000.00',
                                'Currency': 'VND',
                                'WalletCredits': '0.00',
                                'TaxAmount': '59090.91',
                                'ShippingAmount': '0.00',
                                'ShippingServiceCost': '',
                                'VoucherAmount': '0',
                                'VoucherCode': '',
                                'Status': 'canceled',
                                'ShipmentProvider': 'LEX',
                                'IsDigital': '0',
                                'DigitalDeliveryInfo': '',
                                'TrackingCode': 'LMPDS-386196752-2953',
                                'TrackingCodePre': '',
                                'Reason': 'customer_changed_mind',
                                'ReasonDetail': '',
                                'PurchaseOrderId': '5684089',
                                'PurchaseOrderNumber': 'MPDS-M163022966333758086091',
                                'PackageId': '',
                                'PromisedShippingTime': '',
                                'ExtraAttributes': '',
                                'ShippingProviderType': 'standard',
                                'CreatedAt': '2016-07-27 11:55:12',
                                'UpdatedAt': '2016-07-27 16:40:03'
                            }
                        }
                    },
                    {
                        'OrderId': '6184080',
                        'OrderNumber': '325369752',
                        'OrderItems': {
                            'OrderItem': [
                                {
                                    'OrderItemId': '7256432',
                                    'ShopId': '10914625',
                                    'OrderId': '6184080',
                                    'Name': 'TPHCM - Giảm 50% 5 GW users cho tháng sử dụng đầu tiên',
                                    'Sku': 'EC5GW',
                                    'ShopSku': 'NO007VCAA1IMJUVNAMZ-2459157',
                                    'ShippingType': 'Dropshipping',
                                    'ItemPrice': '200000.00',
                                    'PaidPrice': '200000.00',
                                    'Currency': 'VND',
                                    'WalletCredits': '0.00',
                                    'TaxAmount': '18181.82',
                                    'ShippingAmount': '0.00',
                                    'ShippingServiceCost': '',
                                    'VoucherAmount': '0',
                                    'VoucherCode': '',
                                    'Status': 'canceled',
                                    'ShipmentProvider': 'LEX',
                                    'IsDigital': '0',
                                    'DigitalDeliveryInfo': '',
                                    'TrackingCode': '',
                                    'TrackingCodePre': '',
                                    'Reason': 'cs_verification_failed',
                                    'ReasonDetail': '',
                                    'PurchaseOrderId': '',
                                    'PurchaseOrderNumber': '',
                                    'PackageId': '',
                                    'PromisedShippingTime': '',
                                    'ExtraAttributes': '',
                                    'ShippingProviderType': 'standard',
                                    'CreatedAt': '2016-07-27 15:33:26',
                                    'UpdatedAt': '2016-07-27 18:13:10'
                                },
                                {
                                    'OrderItemId': '7256433',
                                    'ShopId': '10914626',
                                    'OrderId': '6184080',
                                    'Name': 'TPHCM - Dịch vụ tải dữ liệu lên hệ thống Ecount ERP',
                                    'Sku': 'EC1US',
                                    'ShopSku': 'NO007VCAA1IMK2VNAMZ-2459167',
                                    'ShippingType': 'Dropshipping',
                                    'ItemPrice': '500000.00',
                                    'PaidPrice': '500000.00',
                                    'Currency': 'VND',
                                    'WalletCredits': '0.00',
                                    'TaxAmount': '45454.55',
                                    'ShippingAmount': '0.00',
                                    'ShippingServiceCost': '',
                                    'VoucherAmount': '0',
                                    'VoucherCode': '',
                                    'Status': 'canceled',
                                    'ShipmentProvider': 'LEX',
                                    'IsDigital': '0',
                                    'DigitalDeliveryInfo': '',
                                    'TrackingCode': '',
                                    'TrackingCodePre': '',
                                    'Reason': 'cs_verification_failed',
                                    'ReasonDetail': '',
                                    'PurchaseOrderId': '',
                                    'PurchaseOrderNumber': '',
                                    'PackageId': '',
                                    'PromisedShippingTime': '',
                                    'ExtraAttributes': '',
                                    'ShippingProviderType': 'standard',
                                    'CreatedAt': '2016-07-27 15:33:27',
                                    'UpdatedAt': '2016-07-27 18:13:10'
                                }
                            ]
                        }
                    }
                ]
            }
        }
    }
}";
            #endregion

            var testJson = JsonConvert.DeserializeObject<ApiLazadaResponseBaseDto>(myObject);


            //var uri = "https://sellercenter-api.lazada.vn/?Action=GetOrders&CreatedAfter=2014-02-25T23%3A46%3A11%2B00%3A00&Format=JSON&Timestamp=2016-07-27T16%3A09%3A57%2B07%3A00&UserID=ecount.vn%40gmail.com&Version=1.0&Signature=63bd2e7d7bb0f0be9824544d9cfb74477c9cfffe2d896fd35623653c0a01b915";
            //using (WebClient webClient = new WebClient()) {
            //    var sssss = JsonConvert.DeserializeObject<ApiLazadaResponseBaseDto>(webClient.DownloadString(uri));
            //}

            //ConvertOjectToClass(testJson, myObject);

            //var easresate = testJson;

        }

        /// <summary>
        /// Convert The class from Lazada object to ERP class to map
        /// By default, Result of Lazada return a lot of case with different case / 
        /// Mặc định dữ liệu Lazada return 1 đối tượng thì trả List, 2 đối tượng trở lên trả về object;
        /// </summary>
        /// <param name="testJson"></param>
        private static void ConvertOjectToClass(ApiLazadaResponseBaseDto testJson, string myObject) {
            for (int i = 0; i < testJson.SuccessResponse.Body.Orders.Order.Count; i++) {
                var objectOrderItems = testJson.SuccessResponse.Body.Orders.Order[i].OrderItems;
                var type = objectOrderItems.OrderItem.GetType();
                objectOrderItems.OrderItemClass = new List<OrderItemDetail>();
                //if (type == typeof(Newtonsoft.Json.Linq.JArray)) {
                //    var ListItemObject = JsonConvert.DeserializeObject<IList<OrderItemDetail>>(objectOrderItems.OrderItem.ToString());
                //    objectOrderItems.OrderItemClass.AddRange(ListItemObject);
                //}
                //else {
                //    var ItemObject = JsonConvert.DeserializeObject<OrderItemDetail>(objectOrderItems.OrderItem.ToString());
                //    objectOrderItems.OrderItemClass.Add(ItemObject);
                //}
                var convertObject = objectOrderItems.OrderItem;

                UtilityMaping.MappingToClass<object, OrderItemDetail, List<OrderItemDetail>>(convertObject, objectOrderItems.OrderItemClass);

                //UtilityMaping.MappingToClass<string, ApiLazadaResponseBaseDto>(myObject);

                objectOrderItems.OrderItem = null;
            }
        }
    }

}

