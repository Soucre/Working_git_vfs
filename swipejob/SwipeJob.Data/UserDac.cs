using Dapper;
using SwipeJob.Data.Base;
using SwipeJob.Data.DB_Manager;
using SwipeJob.Model;
using System;

namespace SwipeJob.Data
{
    public class UserDac: Disposable
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        #endregion

        public User GetUserById()
        {            
            return DbManager.QuerySingle<User>("SJ_UserSelect", new DbQueryOption {
                ConnectionID = DB_Swipe_Job.GetConnectionId(),
                ParameterModel = new DynamicParameters(this)
            });
        }
    }
}
