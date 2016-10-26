using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using SwipeJob.Model;
using SwipeJob.Model.ApiRequset;
using SwipeJob.Model.EF;
using SwipeJob.Utility;

namespace SwipeJob.Core
{
   public class JobSeekerTempManager : BaseManager
    {
        public async Task<bool> CheckEmail(ArgJobSeekerTemp arg)
        {
            Utils.CheckNullOrEmpty(new List<string> { "Email" }, arg.Email);

            using (AppDbContext context = new AppDbContext())
            {
                JobSeekerTempProfile jobSeekerTempProfile = await context.JobSeekerTempProfiles.FirstOrDefaultAsync(x => x.Email == arg.Email);
                return jobSeekerTempProfile == null;
            }
        }

        public async Task<bool> Register(ArgJobSeekerTemp arg)
        {
            Utils.CheckNullOrEmpty(new List<string> { "Email", "FullName", "PhoneNumber" }, arg.Email, arg.FullName, arg.PhoneNumber);

            using (AppDbContext context = new AppDbContext())
            {
                if (arg.Industry==null)
                {
                    arg.Industry = await context.Industries.FirstOrDefaultAsync(x => x.Name == null);
                }
                context.JobSeekerTempProfiles.Add(new JobSeekerTempProfile
                {
                    FullName = arg.FullName,
                    Email = arg.Email,
                    PhoneNumber = arg.PhoneNumber,
                    ExperienceLevel = arg.ExperienceLevel,
                    IndustryId = arg.Industry.Id,
                    DayOfBirthUtc = arg.DayOfBirthUtc,
                    RegisteredDateUtc = DateTime.UtcNow
                });
                await EmailDelivery.SendJobSeekerRegisterCompleted(arg.Email);
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
