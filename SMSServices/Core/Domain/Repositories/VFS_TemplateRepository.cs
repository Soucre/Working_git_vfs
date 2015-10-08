using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain.Model;
using NHibernate;
using NHibernate.Criterion;

namespace Core.Domain.Repositories
{
    public class VFS_TemplateRepository : IRepository<VFS_Template>
    {
        void IRepository<VFS_Template>.Save(VFS_Template entity)
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

        void IRepository<VFS_Template>.Update(VFS_Template entity)
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

        void IRepository<VFS_Template>.Delete(VFS_Template entity)
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

        VFS_Template IRepository<VFS_Template>.GetById(Guid id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<VFS_Template>().Add(Restrictions.Eq("Id", id)).UniqueResult<VFS_Template>();
        }
        VFS_Template IRepository<VFS_Template>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<VFS_Template>().Add(Restrictions.Eq("Id", id)).UniqueResult<VFS_Template>();
        }
        IList<VFS_Template> IRepository<VFS_Template>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(VFS_Template));
                return criteria.List<VFS_Template>();
            }
        }

    }
}
