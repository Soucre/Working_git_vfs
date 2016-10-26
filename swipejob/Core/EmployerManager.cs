using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SwipeJob.Model;
using SwipeJob.Model.ApiRequset;
using SwipeJob.Model.EF;
using SwipeJob.Model.Extra;

namespace SwipeJob.Core
{
    public class EmployerManager : BaseManager
    {
        public EmployerManager()
        {

        }

        public EmployerManager(Guid userId) : base(userId)
        {

        }

        public async Task<List<User>> GetTopEmployer()
        {
            using (AppDbContext context = new AppDbContext())
            {
                List<User> users = await context.Users.Include(x => x.Employer).Where(x => x.UserType == UserType.Employer).OrderByDescending(x => x.Employer.CreatedDateUtc).Take(5).ToListAsync();
                return users;
            }
        }

        public async Task<Tuple<List<User>, int, int>> Search(SearchEmployerParams employerParams)
        {
            using (AppDbContext context = new AppDbContext())
            {
                var query = context.Users.Include(x => x.Employer).Where(x => x.UserType == UserType.Employer).AsQueryable();

                if (!string.IsNullOrWhiteSpace(employerParams.CompanyName))
                {
                    query = query.Where(x => x.Employer.CompanyName == employerParams.CompanyName);
                }

                int totalItems = await query.CountAsync();
                int totalPages = totalItems / employerParams.PageSize;
                if (totalItems % employerParams.PageSize > 0)
                {
                    totalPages++;
                }

                List<User> users = await query.OrderByDescending(p => p.Employer.CompanyName).Skip(employerParams.PageIndex * employerParams.PageSize).Take(employerParams.PageSize).ToListAsync();
                return new Tuple<List<User>, int, int>(users, totalPages, totalItems);
            }
        }

        public async Task<User> GetEmployerById(Guid id)
        {
            using (AppDbContext context = new AppDbContext())
            {
                User user = await context.Users.Include(x => x.Employer).Where(x => x.Id == id).FirstOrDefaultAsync();
                return user;
            }
        }
    }
}