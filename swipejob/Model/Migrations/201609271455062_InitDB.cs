namespace SwipeJob.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applicant",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        JobSeekerId = c.Guid(nullable: false),
                        JobId = c.Guid(nullable: false),
                        ApplicantStatus = c.Int(nullable: false),
                        CreatedDateUtc = c.DateTime(nullable: false),
                        UpdatedDateUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Job", t => t.JobId)
                .ForeignKey("dbo.JobSeeker", t => t.JobSeekerId)
                .Index(t => t.JobSeekerId)
                .Index(t => t.JobId);
            
            CreateTable(
                "dbo.Job",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        EmployerId = c.Guid(nullable: false),
                        LanguageId = c.Guid(nullable: false),
                        LocationId = c.Guid(nullable: false),
                        JobType = c.Int(nullable: false),
                        JobName = c.String(nullable: false),
                        MinSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaxSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        JobStartDate = c.DateTime(nullable: false),
                        JobDescription = c.String(),
                        IsStartWorkImmediately = c.Boolean(nullable: false),
                        StartWorkingAt = c.String(),
                        EndWorkingAt = c.String(),
                        AvailableDate = c.DateTime(nullable: false),
                        HoursPerDay = c.Int(nullable: false),
                        DayPerWeek = c.Int(nullable: false),
                        DayPerMonth = c.Int(nullable: false),
                        GenderRequired = c.Int(nullable: false),
                        ExtraSalary = c.String(),
                        IsSalaryIncludeMealAndBreakTime = c.Boolean(nullable: false),
                        ViewCount = c.Long(nullable: false),
                        CreatedDateUtc = c.DateTime(nullable: false),
                        UpdatedDateUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employer", t => t.EmployerId)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .ForeignKey("dbo.Location", t => t.LocationId)
                .Index(t => t.EmployerId)
                .Index(t => t.LanguageId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Education",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        EducationLevel = c.Int(nullable: false),
                        FieldOfStudyId = c.Guid(nullable: false),
                        Certification = c.String(),
                        MinimumGrade = c.String(),
                        ExperienceLevel = c.Int(nullable: false),
                        JobId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Industry", t => t.FieldOfStudyId)
                .ForeignKey("dbo.Job", t => t.JobId)
                .Index(t => t.FieldOfStudyId)
                .Index(t => t.JobId);
            
            CreateTable(
                "dbo.Industry",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employer",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        CompanyName = c.String(nullable: false),
                        CompanyRegistrationNumber = c.String(),
                        Logo = c.Binary(),
                        Address = c.String(),
                        WebLink = c.String(),
                        ContactName = c.String(nullable: false),
                        PhoneNumber = c.String(),
                        NatureOfBusiness = c.String(),
                        OverView = c.String(storeType: "ntext"),
                        CreatedDateUtc = c.DateTime(nullable: false),
                        UpdatedDateUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 225),
                        Password = c.String(),
                        AccountType = c.Int(nullable: false),
                        UserType = c.Int(nullable: false),
                        RegisteredDateUtc = c.DateTime(nullable: false),
                        IsActivated = c.Boolean(nullable: false),
                        IsLocked = c.Boolean(nullable: false),
                        LockedDateUtc = c.DateTime(),
                        ConfirmationCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JobSeeker",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        Avartar = c.Binary(),
                        FullName = c.String(nullable: false),
                        Gender = c.Int(nullable: false),
                        NationalServiceStatus = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(),
                        NRICNumber = c.String(),
                        NRICType = c.Int(nullable: false),
                        Race = c.String(),
                        Religions = c.String(),
                        PhoneNumber = c.String(),
                        MobileNumber = c.String(),
                        PostalCode = c.String(),
                        Address = c.String(),
                        MoreDocument = c.Binary(),
                        Extension = c.String(),
                        ExperienceYear = c.Int(nullable: false),
                        HighestEducation = c.String(),
                        LanguageId = c.Guid(nullable: false),
                        ExpectedPosition = c.String(),
                        ExpectedLocation = c.String(),
                        ExpectedJobCategory = c.String(),
                        ExpectedSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CanNegotiation = c.Boolean(nullable: false),
                        InterestIn = c.String(storeType: "ntext"),
                        AdditionalInfo = c.String(storeType: "ntext"),
                        CreatedDateUtc = c.DateTime(nullable: false),
                        UpdatedDateUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.LanguageId);
            
            CreateTable(
                "dbo.CompanyHisotry",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        JobSeekerId = c.Guid(nullable: false),
                        CompanyName = c.String(),
                        Position = c.String(),
                        From = c.DateTime(),
                        To = c.DateTime(),
                        CreatedDateUtc = c.DateTime(nullable: false),
                        UpdatedDateUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobSeeker", t => t.JobSeekerId)
                .Index(t => t.JobSeekerId);
            
            CreateTable(
                "dbo.Inbox",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        RecipientId = c.Guid(nullable: false),
                        Title = c.String(),
                        Message = c.String(),
                        CreatedDateUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.RecipientId)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.JobSeeker", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RecipientId);
            
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        CultureName = c.String(),
                        CreatedDateUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Feedback",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        FullName = c.String(),
                        Address = c.String(),
                        Email = c.String(nullable: false, maxLength: 225),
                        PhoneNumber = c.String(),
                        Message = c.String(),
                        CreatedDateUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JobSeekerTempProfile",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        IndustryId = c.Guid(nullable: false),
                        FullName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        DayOfBirthUtc = c.DateTime(),
                        ExperienceLevel = c.Int(),
                        RegisteredDateUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Industry", t => t.IndustryId)
                .Index(t => t.IndustryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobSeekerTempProfile", "IndustryId", "dbo.Industry");
            DropForeignKey("dbo.Applicant", "JobSeekerId", "dbo.JobSeeker");
            DropForeignKey("dbo.Job", "LocationId", "dbo.Location");
            DropForeignKey("dbo.Job", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.Job", "EmployerId", "dbo.Employer");
            DropForeignKey("dbo.Employer", "UserId", "dbo.User");
            DropForeignKey("dbo.JobSeeker", "UserId", "dbo.User");
            DropForeignKey("dbo.JobSeeker", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.Inbox", "UserId", "dbo.JobSeeker");
            DropForeignKey("dbo.Inbox", "UserId", "dbo.User");
            DropForeignKey("dbo.Inbox", "RecipientId", "dbo.User");
            DropForeignKey("dbo.CompanyHisotry", "JobSeekerId", "dbo.JobSeeker");
            DropForeignKey("dbo.Education", "JobId", "dbo.Job");
            DropForeignKey("dbo.Education", "FieldOfStudyId", "dbo.Industry");
            DropForeignKey("dbo.Applicant", "JobId", "dbo.Job");
            DropIndex("dbo.JobSeekerTempProfile", new[] { "IndustryId" });
            DropIndex("dbo.Inbox", new[] { "RecipientId" });
            DropIndex("dbo.Inbox", new[] { "UserId" });
            DropIndex("dbo.CompanyHisotry", new[] { "JobSeekerId" });
            DropIndex("dbo.JobSeeker", new[] { "LanguageId" });
            DropIndex("dbo.JobSeeker", new[] { "UserId" });
            DropIndex("dbo.Employer", new[] { "UserId" });
            DropIndex("dbo.Education", new[] { "JobId" });
            DropIndex("dbo.Education", new[] { "FieldOfStudyId" });
            DropIndex("dbo.Job", new[] { "LocationId" });
            DropIndex("dbo.Job", new[] { "LanguageId" });
            DropIndex("dbo.Job", new[] { "EmployerId" });
            DropIndex("dbo.Applicant", new[] { "JobId" });
            DropIndex("dbo.Applicant", new[] { "JobSeekerId" });
            DropTable("dbo.JobSeekerTempProfile");
            DropTable("dbo.Feedback");
            DropTable("dbo.Location");
            DropTable("dbo.Language");
            DropTable("dbo.Inbox");
            DropTable("dbo.CompanyHisotry");
            DropTable("dbo.JobSeeker");
            DropTable("dbo.User");
            DropTable("dbo.Employer");
            DropTable("dbo.Industry");
            DropTable("dbo.Education");
            DropTable("dbo.Job");
            DropTable("dbo.Applicant");
        }
    }
}
