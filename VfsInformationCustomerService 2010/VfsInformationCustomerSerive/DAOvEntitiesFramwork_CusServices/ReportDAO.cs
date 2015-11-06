using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOvEntitiesFramwork_CusServices
{
    public class ReportDAO
    {
        private VfsCustomerServiceEntities DataContext;
        public void InsertReport(Report rp)
        {
            using (DataContext = new VfsCustomerServiceEntities())
            {
                try
                {
                    DataContext.Reports.Add(rp);
                    DataContext.SaveChanges();
                }
                catch (Exception)
                {
                    
                    
                }
            }
        }
        public List<Report> GetListReport(ReportEnums orderBy, string orderDirection, int page, int pageSize, out int totalRecords)
        {
            
                using (DataContext = new VfsCustomerServiceEntities())
                {
                    DataContext.Database.Connection.Open();
                    DbCommand cmd = DataContext.Database.Connection.CreateCommand();
                    cmd.CommandText = "spReportGetList";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrderBy", orderBy.ToString()));
                    cmd.Parameters.Add(new SqlParameter("OrderDirection", orderDirection));
                    cmd.Parameters.Add(new SqlParameter("Page", page));
                    cmd.Parameters.Add(new SqlParameter("PageSize", pageSize));

                    var totalCountParam = new SqlParameter("TotalRecords", 0) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(totalCountParam);

                    List<Report> tasks;
                    using (var reader = cmd.ExecuteReader())
                    {
                        tasks = reader.MapToList<Report>();
                    }
                    //Access output variable after reader is closed
                    totalRecords = (totalCountParam.Value == null) ? 0 : Convert.ToInt32(totalCountParam.Value);
                    return tasks;
                }
            
          
                            
        }
    }
    public enum ReportEnums
    {
        Id,
        Title,
        UploadDir,
        DateViewCustomer,
        TotalDownload,
        FileSize,
        IdReportType,
        Ticker,
        CreateDate,
        FileType
        
    }
}
