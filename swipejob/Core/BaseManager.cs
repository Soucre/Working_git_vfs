using System;
using System.Data.Entity;
using System.Threading.Tasks;
using SwipeJob.Model;
using SwipeJob.Model.EF;
using SwipeJob.Model.Extra;
using SwipeJob.Utility.Exceptions;

namespace SwipeJob.Core
{
    public class BaseManager
    {
        public BaseManager()
        {            
        }

        public BaseManager(Guid userId)
        {
            _userId = userId;
        }

        protected async Task<User> GetCurrentUser(AppDbContext context)
        {
            User user = await context.Users.FirstOrDefaultAsync(p => p.Id == _userId);
            if (user == null)
            {
                throw new UserException(ErrorCode.INVALID_SESSION.ToString());
            }

            return user;
        }

        protected Guid _userId;
    }
}
