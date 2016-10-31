using System;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace SwipeJob.Model.EF
{
    public class ExtenedDBConfiguration : DbConfiguration
    {
        public ExtenedDBConfiguration()
        {
            //SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy(3, TimeSpan.FromSeconds(30)));
        }
    }
}
