using System.Data.Entity;
using SwipeJob.Model.Migrations;

namespace SwipeJob.Model.EF
{
    public class DbInitializer : MigrateDatabaseToLatestVersion<AppDbContext, Configuration>
    {
       
    }
}
