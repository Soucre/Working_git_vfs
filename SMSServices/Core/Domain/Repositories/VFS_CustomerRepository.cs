using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain.Model;
using NHibernate;
using NHibernate.Criterion;

namespace Core.Domain.Repositories
{
    public class VFS_CustomerRepository : IRepository<VFS_Customer>
    {
        void IRepository<VFS_Customer>.Save(VFS_Customer entity)
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

        void IRepository<VFS_Customer>.Update(VFS_Customer entity)
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

        void IRepository<VFS_Customer>.Delete(VFS_Customer entity)
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

        VFS_Customer IRepository<VFS_Customer>.GetById(Guid id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<VFS_Customer>().Add(Restrictions.Eq("Id", id)).UniqueResult<VFS_Customer>();
        }
        VFS_Customer IRepository<VFS_Customer>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<VFS_Customer>().Add(Restrictions.Eq("Id", id)).UniqueResult<VFS_Customer>();
        }
        IList<VFS_Customer> IRepository<VFS_Customer>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(VFS_Customer));
                return criteria.List<VFS_Customer>();
            }
        }

    }
}
