using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOvEntitiesFramwork_CusServices
{
    public class ReportDAO
    {
        private VfsCustomerServiceEntities db;
        public void InsertReport(Report rp)
        {
            using (db = new VfsCustomerServiceEntities())
            {
                try
                {
                    db.Reports.Add(rp);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    
                    
                }
            }
        }
    }
}
