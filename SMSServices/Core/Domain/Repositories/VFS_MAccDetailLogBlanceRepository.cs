using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain.Model;
using NHibernate;
using NHibernate.Criterion;

namespace Core.Domain.Repositories
{
    public class VFS_MAccDetailLogBlanceRepository : IRepository<VFS_MAccDetailLogBlance>
    
    {
        #region IRepository<VFS_MAccDetailLogBlance> Members

        void IRepository<VFS_MAccDetailLogBlance>.Save(VFS_MAccDetailLogBlance entity)
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

        void IRepository<VFS_MAccDetailLogBlance>.Update(VFS_MAccDetailLogBlance entity)
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

        void IRepository<VFS_MAccDetailLogBlance>.Delete(VFS_MAccDetailLogBlance entity)
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

        VFS_MAccDetailLogBlance IRepository<VFS_MAccDetailLogBlance>.GetById(Guid id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<VFS_MAccDetailLogBlance>().Add(Restrictions.Eq("Id", id)).UniqueResult<VFS_MAccDetailLogBlance>();
        }
        VFS_MAccDetailLogBlance IRepository<VFS_MAccDetailLogBlance>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<VFS_MAccDetailLogBlance>().Add(Restrictions.Eq("Id", id)).UniqueResult<VFS_MAccDetailLogBlance>();
        }

        IList<VFS_MAccDetailLogBlance> IRepository<VFS_MAccDetailLogBlance>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(VFS_MAccDetailLogBlance));
                return criteria.List<VFS_MAccDetailLogBlance>();
            }
        }


        #endregion
    }
}
