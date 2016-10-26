using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SwipeJob.Model;
using SwipeJob.Model.EF;
using System.Security;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using SwipeJob.Model.Extra;
using SwipeJob.Utility;
using SwipeJob.Utility.Exceptions;

namespace SwipeJob.Core
{
    public class CommonManager : BaseManager
    {
        public CommonManager()
        {

        }

        public CommonManager(Guid userId) : base(userId)
        {

        }

        public async Task UpdateJobSeekerProfilePhoto(byte[] imageBytes)
        {
            using (AppDbContext context = new AppDbContext())
            {
                User user = await GetCurrentUser(context);
                if (user.UserType == UserType.JobSeeker)
                {
                    JobSeeker jobSeeker = await context.JobSeekers.FirstOrDefaultAsync(x => x.UserId == user.Id);
                    jobSeeker.Avartar = imageBytes;
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task UpdateJobSeekerMoreDoc(byte[] imageBytes,string extension)
        {
            using (AppDbContext context = new AppDbContext())
            {
                User user = await GetCurrentUser(context);
                if (user.UserType == UserType.JobSeeker)
                {
                    JobSeeker jobSeeker = await context.JobSeekers.FirstOrDefaultAsync(x => x.UserId == user.Id);
                    jobSeeker.MoreDocument = imageBytes;
                    jobSeeker.Extension = extension;
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<JobSeeker> CreateTempFile(Guid id)
        {
            using (AppDbContext context = new AppDbContext())
            {
                JobSeeker jobSeeker = await context.JobSeekers.FirstOrDefaultAsync(x => x.UserId == id);
                if (jobSeeker == null)
                {
                    throw new UserException(ErrorCode.FILE_NOT_FOUND.ToString());
                }
                return jobSeeker;
            }
        }

        public async Task UpdateEmployerLogoPhoto(byte[] imageBytes)
        {
            using (AppDbContext context = new AppDbContext())
            {
                User user = await GetCurrentUser(context);
                if (user.UserType == UserType.Employer)
                {
                    Employer employer = await context.Employers.FirstOrDefaultAsync(x => x.UserId == user.Id);
                    employer.Logo = imageBytes;
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<List<Industry>> GetAllIndustry()
        {

            using (AppDbContext context = new AppDbContext())
            {
                List<Industry> industries = await context.Industries.Where(x => x.Name != null).OrderBy(x => x.Name).ToListAsync();
                return industries;
            }
        }

        public async Task<List<Location>> GetAllLocation()
        {
            using (AppDbContext context = new AppDbContext())
            {
                List<Location> locations = await context.Locations.OrderBy(x => x.Name).ToListAsync();
                return locations;
            }
        }

        public async Task<List<Language>> GetAllLanguages()
        {
            using (AppDbContext context = new AppDbContext())
            {
                List<Language> languages = await context.Languages.OrderBy(x => x.Name).ToListAsync();
                return languages;
            }
        }

        public async Task<Language> GetLaguageById(Guid id)
        {
            using (AppDbContext context = new AppDbContext())
            {
                Language language = await context.Languages.Where(x => x.Id == id).FirstOrDefaultAsync();
                return language;
            }
        }
    }
}