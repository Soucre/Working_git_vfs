using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Domain.Model
{
    public class RightExec
    {
        #region Primitive Properties

        public virtual int Id
        {
            get;
            set;
        }

        public virtual string StockCode
        {
            get;
            set;
        }

        public virtual string StockType
        {
            get;
            set;
        }

        public virtual string BoardType
        {
            get;
            set;
        }

        public virtual System.DateTime DateNoRight
        {
            get;
            set;
        }

        public virtual System.DateTime DateOwnerConfirm
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> DatePay
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> BeginRegisterDate
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> EndRegisterDate
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> BeginTransferDate
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> EndTransferDate
        {
            get;
            set;
        }

        public virtual string Description
        {
            get;
            set;
        }

        public virtual int RateA
        {
            get;
            set;
        }

        public virtual int RateB
        {
            get;
            set;
        }

        public virtual string RightType
        {
            get;
            set;
        }

        public virtual Nullable<decimal> Difference
        {
            get;
            set;
        }

        public virtual Nullable<bool> Posted
        {
            get;
            set;
        }

        public virtual Nullable<decimal> RightExecPrice
        {
            get;
            set;
        }

        public virtual Nullable<int> RoundType
        {
            get;
            set;
        }

        public virtual Nullable<decimal> RoundPrice
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> DeadLine
        {
            get;
            set;
        }

        public virtual Nullable<int> RightRateA
        {
            get;
            set;
        }

        public virtual Nullable<int> RightRateB
        {
            get;
            set;
        }

        public virtual Nullable<int> DecimalPlaces
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> BeginTransDate
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> ActiveDate
        {
            get;
            set;
        }

        public virtual string ReceiptNumber
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> ReceiptDate
        {
            get;
            set;
        }

        //public virtual System.Guid msrepl_tran_version
        //{
        //    get;
        //    set;
        //}

        #endregion
    }
}
