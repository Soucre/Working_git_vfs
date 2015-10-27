using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DAOvEntitiesFramwork_CusServices
{
    public static class CustomerDAO
    {
        private static VfsCustomerServiceEntities db;
        public static async  Task<List<CustomerCustom>> getListCustomerVIPType()
        {
            using (db = new VfsCustomerServiceEntities())
            {
                var result = await (from c in db.Customers
                              where c.VType == true
                              select new CustomerCustom
                              {
                                  CustomerId = c.CustomerId,
                                  Mobile = c.Mobile
                              }).ToListAsync();

                return result;
            }
        }
    }

    public class CustomerCustom
    {
        public string CustomerId { get; set; }
        public string Mobile { get; set; }
    }
}
