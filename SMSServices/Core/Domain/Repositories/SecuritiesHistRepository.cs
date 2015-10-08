using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain.Model;
using NHibernate;
using NHibernate.Criterion;

namespace Core.Domain.Repositories
{
    public class SecuritiesHistRepository : IRepository<SecuritiesHist>, ISecuritiesHistRepository
    {
        #region IRepository<SecuritiesHist> Members

        void IRepository<SecuritiesHist>.Save(SecuritiesHist entity)
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

        void IRepository<SecuritiesHist>.Update(SecuritiesHist entity)
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

        void IRepository<SecuritiesHist>.Delete(SecuritiesHist entity)
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

        SecuritiesHist IRepository<SecuritiesHist>.GetById(Guid id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<SecuritiesHist>().Add(Restrictions.Eq("Id", id)).UniqueResult<SecuritiesHist>();
        }
        SecuritiesHist IRepository<SecuritiesHist>.GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.CreateCriteria<SecuritiesHist>().Add(Restrictions.Eq("Id", id)).UniqueResult<SecuritiesHist>();
        }
        IList<SecuritiesHist> IRepository<SecuritiesHist>.GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(SecuritiesHist));
                return criteria.List<SecuritiesHist>();
            }
        }

        public IList<SecuritiesHist> getSecuritiesHistByStockCodeAndTransactionDate(string stockCode, DateTime transactionDate)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                IList<SecuritiesHist> resual = null;
                IQuery query = session.CreateQuery("SELECT obj   FROM Core.Domain.Model.SecuritiesHist obj WHERE obj.StockCode = :stockCode And obj.TransactionDate = :transactionDate");

                query.SetString("stockCode", stockCode);
                query.SetDateTime("transactionDate", transactionDate);
                resual = query.List<SecuritiesHist>();

                return resual;
            }
        }

        #endregion
    }
}
