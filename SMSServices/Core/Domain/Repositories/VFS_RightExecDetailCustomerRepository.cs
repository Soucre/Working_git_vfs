using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain.Model;
using NHibernate;
using NHibernate.Criterion;

namespace Core.Domain.Repositories
{
    public class VFS_RightExecDetailCustomerRepository : IRepository<VFS_RightExecDetailCustomer>, IVFS_RightExecDetailCustomerRepository<VFS_RightExecDetailCustomer>
    {
        #region IRepository<DetailCustomerRightExec> Members

        void IRepository<VFS_RightExecDetailCustomer>.Save(VFS_RightExecDetailCustomer entity)
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

        void IRepository<VFS_RightExecDetailCustomer>.Update(VFS_RightExecDetailCustomer entity)
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

        void IRepository<VFS_RightExecDetailCustomer>.Delete(VFS_RightExecDetailCustomer entity)
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

        VFS_RightExecDetailCustomer IRepository<VFS_RightExecDetailCustomer>.GetById(Guid id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<VFS_RightExecDetailCustomer>().Add(Restrictions.Eq("Id", id)).UniqueResult<VFS_RightExecDetailCustomer>();
        }
        VFS_RightExecDetailCustomer IRepository<VFS_RightExecDetailCustomer>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<VFS_RightExecDetailCustomer>().Add(Restrictions.Eq("Id", id)).UniqueResult<VFS_RightExecDetailCustomer>();
        }

        IList<VFS_RightExecDetailCustomer> IRepository<VFS_RightExecDetailCustomer>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(VFS_RightExecDetailCustomer));
                return criteria.List<VFS_RightExecDetailCustomer>();
            }
        }

        public IList<VFS_RightExecDetailCustomer> getListRightExecDetailCustomerFromIdRightExec(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                IList<VFS_RightExecDetailCustomer> resual = new List<VFS_RightExecDetailCustomer>();
                IQuery query = session.CreateQuery("SELECT obj FROM VFS_RightExecDetailCustomer obj WHERE  obj.IdRightExec =:id");

                query.SetInt32("id", id);


                resual = query.List<VFS_RightExecDetailCustomer>();


                return resual;
            }
        }

        #endregion
    }
}
