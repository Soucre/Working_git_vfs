using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain.Model;
using NHibernate;
using NHibernate.Criterion;

namespace Core.Domain.Repositories
{
    public class RightExecRepository : IRepository<RightExec>, IRightExecRepository<RightExec>
    {

        #region IRepository<RightExec> Members

        void IRepository<RightExec>.Save(RightExec entity)
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

        void IRepository<RightExec>.Update(RightExec entity)
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

        void IRepository<RightExec>.Delete(RightExec entity)
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

        RightExec IRepository<RightExec>.GetById(Guid id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<RightExec>().Add(Restrictions.Eq("Id", id)).UniqueResult<RightExec>();
        }
        RightExec IRepository<RightExec>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<RightExec>().Add(Restrictions.Eq("Id", id)).UniqueResult<RightExec>();
        }

        IList<RightExec> IRepository<RightExec>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(RightExec));
                return criteria.List<RightExec>();
            }
        }

        public IList<RightExec> getRightExecListFromStock(string stock)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                IList<RightExec> resual = new List<RightExec>();
                IQuery query = session.CreateQuery("SELECT obj FROM RightExec obj WHERE  obj.StockCode =:stock Order By obj.DateOwnerConfirm DESC");

                query.SetString("stock", stock);


                resual = query.List<RightExec>();


                return resual;
            }
        }
        #endregion
    }
}
