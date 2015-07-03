using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DAOvEntitiesFramwork
{    

    public static class Customer_Goline
    {
        static DolPhin_Entities dbGoline;
        public async static Task<List<Customer>> GetListCustomer_Goline()
        {
            using (dbGoline = new DolPhin_Entities())
            {
                return await dbGoline.Customers.ToListAsync();
            }
        }

    }
}
