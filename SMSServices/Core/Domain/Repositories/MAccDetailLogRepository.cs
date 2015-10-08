using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain.Model;
using NHibernate;
using NHibernate.Criterion;

namespace Core.Domain.Repositories
{
    public class MAccDetailLogRepository : IRepository<MAccDetailLog>, IMAccDetailLogRepository<MAccDetailLog>
    {
        #region IRepository<MAccDetailLog> Members

        void IRepository<MAccDetailLog>.Save(MAccDetailLog entity)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(entity);
                    transaction.Commit();
                }
            }
        }

        void IRepository<MAccDetailLog>.Update(MAccDetailLog entity)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(entity);
                    transaction.Commit();
                }
            }
        }

        void IRepository<MAccDetailLog>.Delete(MAccDetailLog entity)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(entity);
                    transaction.Commit();
                }
            }
        }

        MAccDetailLog IRepository<MAccDetailLog>.GetById(Guid id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<MAccDetailLog>().Add(Restrictions.Eq("Id", id)).UniqueResult<MAccDetailLog>();
        }
        MAccDetailLog IRepository<MAccDetailLog>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<MAccDetailLog>().Add(Restrictions.Eq("Id", id)).UniqueResult<MAccDetailLog>();
        }

        IList<MAccDetailLog> IRepository<MAccDetailLog>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(MAccDetailLog));
                return criteria.List<MAccDetailLog>();
            }
        }


        public IList<MAccDetailLog> getListFromCustomer(string accountId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                IList<MAccDetailLog> resual = new List<MAccDetailLog>();
                IQuery query = session.CreateQuery("SELECT obj FROM MAccDetailLog obj WHERE  obj.AccountId =:accountId And obj.Status IN ('C','B') Order By obj.LogDate, obj.LogId");

                query.SetString("accountId", accountId);


                resual = query.List<MAccDetailLog>();


                return resual;
            }
        }
        public IList<String> getListAllCutomer()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                IList<String> resual = new List<String>();
                IQuery query = session.CreateQuery("SELECT DISTINCT obj.AccountId FROM MAccDetailLog obj order by obj.AccountId ");




                resual = query.List<String>();


                return resual;
            }
        }
        public void truncateTable()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                IList<String> resual = new List<String>();
                IQuery query = session.CreateSQLQuery("Truncate table [VFS_MAccDetailLogBlance]");         
                
                query.ExecuteUpdate();

            }
        }
    
        #endregion
    }
}
