namespace SwipeJob.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateuserdateofbirth : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobSeeker", "DateOfBirth", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobSeeker", "DateOfBirth", c => c.DateTime(nullable: false));
        }
    }
}
