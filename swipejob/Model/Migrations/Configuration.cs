using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using CsvHelper;
using SwipeJob.Model.EF;
using SwipeJob.Model.Extra;
using SwipeJob.Utility;

namespace SwipeJob.Model.Migrations
{
    public class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppDbContext context)
        {
            if (!Convert.ToBoolean(ConfigurationManager.AppSettings["BootstrapSeedData"]))
            {
                return;
            }

            //add data for table language
            List<Language> languageList = new List<Language>();
            using (TextReader reader = new StreamReader(GetType().Assembly.GetManifestResourceStream("SwipeJob.Model.Migrations.Seed.Language.csv")))
            {
                CsvReader csvReader = new CsvReader(reader);

                while (csvReader.Read())
                {
                    Language language = new Language
                    {
                        Name = csvReader.GetField<string>(0),
                        CultureName = csvReader.GetField<string>(1),
                        CreatedDateUtc = DateTime.UtcNow
                    };
                    languageList.Add(language);
                }
                context.Languages.AddOrUpdate(p => p.Name, languageList.ToArray());
            }

            context.Industries.AddOrUpdate(p => p.Id, new Industry());

            using (TextReader reader = new StreamReader(GetType().Assembly.GetManifestResourceStream("SwipeJob.Model.Migrations.Seed.Industries.csv")))
            {
                CsvReader csvReader = new CsvReader(reader);
                List<Industry> industries = new List<Industry>();

                while (csvReader.Read())
                {
                    Industry industry = new Industry
                    {
                        Name = csvReader.GetField<string>(0)
                    };
                    industries.Add(industry);
                }
                context.Industries.AddOrUpdate(p => p.Name, industries.ToArray());
            }

            List<Location> locations = new List<Location>();
            using (TextReader reader = new StreamReader(GetType().Assembly.GetManifestResourceStream("SwipeJob.Model.Migrations.Seed.Location.csv")))
            {
                CsvReader csvReader = new CsvReader(reader);
                locations = new List<Location>();

                while (csvReader.Read())
                {
                    Location location = new Location
                    {
                        Name = csvReader.GetField<string>(0)
                    };
                    locations.Add(location);
                }
                context.Locations.AddOrUpdate(p => p.Name, locations.ToArray());
            }

            for (int i=0; i < 10; i++)
            {
                User user = new User
                {
                    Email = "jobseeker" + i.ToString() + "@swipejob.com",
                    Password = UtilsCryptography.GenerateBCryptHash("123456Aa"),
                    UserType = UserType.JobSeeker,
                    AccountType = AccountType.Email,
                    IsActivated = true,
                    RegisteredDateUtc = DateTime.UtcNow

                };

                context.Users.AddOrUpdate(x => x.Email, user);

                User user1 = new User
                {
                    Email = "employer" + i.ToString()  + "@swipejob.com",
                    Password = UtilsCryptography.GenerateBCryptHash("123456Aa"),
                    UserType = UserType.Employer,
                    AccountType = AccountType.Email,
                    IsActivated = true,
                    RegisteredDateUtc = DateTime.UtcNow
                };
                context.Users.AddOrUpdate(x => x.Email, user1);

                context.SaveChanges();

                context.JobSeekers.AddOrUpdate(x => x.FullName,
                    new JobSeeker
                    {
                        UserId = user.Id,
                        FullName = "Admin JobSeeker" + i.ToString(),
                        Gender = Gender.Male,
                        NRICType = NRICType.Citizen,
                        NationalServiceStatus = NationnalServiceStatus.AwaitingEnlistment,
                        Address = "TEST",
                        PhoneNumber = "12345678",
                        MobileNumber = "12345",
                        PostalCode = "12345",
                        NRICNumber = "1234",
                        Race = "TEST",
                        Religions = "TEST",
                        CreatedDateUtc = DateTime.UtcNow,
                        UpdatedDateUtc = DateTime.UtcNow,
                        LanguageId = languageList.First().Id
                    });

                if (i == 0)
                {
                    for (int j=0; j<20; j++)
                    {
                        Job job = new Job
                        {
                            Id = Guid.NewGuid(),
                            EmployerId = user1.Id,
                            LanguageId = languageList[0].Id,
                            JobName = "Job " + i.ToString(),
                            JobType = JobType.FullTime,
                            MinSalary = 5,
                            MaxSalary = 10,
                            ExtraSalary = "1",
                            StartDate = DateTime.UtcNow,
                            EndDate = DateTime.UtcNow,
                            JobStartDate = DateTime.UtcNow,
                            JobDescription = "Job Description " + i.ToString(),
                            IsStartWorkImmediately = true,
                            StartWorkingAt = "5",
                            EndWorkingAt = "10",
                            AvailableDate = DateTime.UtcNow,
                            HoursPerDay = 8,
                            DayPerWeek = 7,
                            DayPerMonth = 28,
                            GenderRequired = GenderRequired.Both,
                            IsSalaryIncludeMealAndBreakTime = false,
                            CreatedDateUtc = DateTime.UtcNow,
                            UpdatedDateUtc = DateTime.UtcNow,
                            LocationId = locations[0].Id
                        };
                        context.Jobs.AddOrUpdate(job);
                    }
                } else
                {
                    Job job = new Job
                    {
                        Id = Guid.NewGuid(),
                        EmployerId = user1.Id,
                        LanguageId = languageList[0].Id,
                        JobName = "Job " + i.ToString(),
                        JobType = JobType.FullTime,
                        MinSalary = 5,
                        MaxSalary = 10,
                        ExtraSalary = "1",
                        StartDate = DateTime.UtcNow,
                        EndDate = DateTime.UtcNow,
                        JobStartDate = DateTime.UtcNow,
                        JobDescription = "Job Description " + i.ToString(),
                        IsStartWorkImmediately = true,
                        StartWorkingAt = "5",
                        EndWorkingAt = "10",
                        AvailableDate = DateTime.UtcNow,
                        HoursPerDay = 8,
                        DayPerWeek = 7,
                        DayPerMonth = 28,
                        GenderRequired = GenderRequired.Both,
                        IsSalaryIncludeMealAndBreakTime = false,
                        CreatedDateUtc = DateTime.UtcNow,
                        UpdatedDateUtc = DateTime.UtcNow,
                        LocationId = locations[0].Id
                    };
                    context.Jobs.AddOrUpdate(job);
                }
                

                context.Employers.AddOrUpdate(x => x.CompanyName, new Employer
                {
                    UserId = user1.Id,
                    CompanyRegistrationNumber = "123456789",
                    CompanyName = "SwipeJob" + i.ToString(),
                    Address = "test",
                    ContactName = "Cang Le",
                    PhoneNumber = "0977998747",
                    NatureOfBusiness = "IT",
                    WebLink = "http://beta.swipejob.com.sg/",
                    OverView = "test",
                    CreatedDateUtc = DateTime.UtcNow,
                    UpdatedDateUtc = DateTime.UtcNow,
                });

                context.SaveChanges();
            }

            
        }
    }
}
