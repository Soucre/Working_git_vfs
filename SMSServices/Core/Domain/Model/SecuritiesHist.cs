using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Domain.Model
{
    
    public class SecuritiesHist
    {
        #region Primitive Properties

        public virtual int Id
        {
            get;
            set;
        }

        public virtual string BranchCode
        {
            get;
            set;
        }

        public virtual string BankGl
        {
            get;
            set;
        }

        public virtual string SectionGl
        {
            get;
            set;
        }

        public virtual string AccountId
        {
            get;
            set;
        }

        public virtual string AccountName
        {
            get;
            set;
        }

        public virtual string StockCode
        {
            get;
            set;
        }

        public virtual long Quantity
        {
            get;
            set;
        }

        public virtual long PendingDebitQuantity
        {
            get;
            set;
        }

        public virtual System.DateTime TransactionDate
        {
            get;
            set;
        }

       

        #endregion
    }
}
