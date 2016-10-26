using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SwipeJob.Model;
using SwipeJob.Model.EF;
using SwipeJob.Model.Extra;
using SwipeJob.Utility.Exceptions;

namespace SwipeJob.Core
{
    public class JobSeekerManager: BaseManager
    {
        public JobSeekerManager()
        {

        }

        public JobSeekerManager(Guid userId) : base(userId)
        {

        }

        public async Task<User> GetJobSeekerById(Guid id)
        {
            using (AppDbContext context = new AppDbContext())
            {
                User user = await context.Users.Include(x => x.JobSeeker).Include(x=>x.JobSeeker.Language).Where(x => x.Id == id).FirstOrDefaultAsync();
                return user;
            }
        }

        public async Task<List<CompanyHisotry>> GetCompanyHistoryForJobSeeker(Guid id)
        {
            using (AppDbContext context = new AppDbContext())
            {
                List<CompanyHisotry> companyHisotries = await context.CompanyHisotries.Where(x => x.JobSeekerId == id).OrderBy(x=>x.CreatedDateUtc).ToListAsync();
                return companyHisotries;
            }
        }

        public async Task DeleteCompanyHistory(CompanyHisotry deleteCompanyHisotry)
        {
            using (AppDbContext context = new AppDbContext())
            {
                CompanyHisotry companyHisotry =await context.CompanyHisotries.Where(x => x.Id == deleteCompanyHisotry.Id).FirstOrDefaultAsync();
                if (companyHisotry == null)
                {
                    throw new UserException(ErrorCode.INVALID.ToString());
                }
                context.CompanyHisotries.Remove(companyHisotry);
                await context.SaveChangesAsync();
            }
        }
    }
}