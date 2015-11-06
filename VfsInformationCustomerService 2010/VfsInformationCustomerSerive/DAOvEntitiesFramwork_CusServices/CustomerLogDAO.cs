using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DAOvEntitiesFramwork_CusServices
{
    public class CustomerLogDAO
    {
        private VfsCustomerServiceEntities db;
        public  List<CustomerLog> getListCustomerVIPType()
        {
            using (db = new VfsCustomerServiceEntities())
            {
                var result = (from cl in db.CustomerLogs
                                    orderby cl.Total_Download descending, cl.Total_Login descending, cl.CustomerId ascending
                                    select cl).ToListAsync();

                return result.Result;
            }
        }

    }
}
