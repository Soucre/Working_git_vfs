using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain.Model;
using NHibernate;
using NHibernate.Criterion;

namespace Core.Domain.Repositories
{
    public class CategoryRepository: IRepository<Category>
    {
        #region IRepository<Category> Members

        void IRepository<Category>.Save(Category entity)
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

        void IRepository<Category>.Update(Category entity)
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

        void IRepository<Category>.Delete(Category entity)
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

        Category IRepository<Category>.GetById(Guid id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Category>().Add(Restrictions.Eq("Id", id)).UniqueResult<Category>();
        }
        Category IRepository<Category>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<Category>().Add(Restrictions.Eq("Id", id)).UniqueResult<Category>();
        }

        IList<Category> IRepository<Category>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(Category));
                return criteria.List<Category>();
            }
        }

        #endregion
    }
}
