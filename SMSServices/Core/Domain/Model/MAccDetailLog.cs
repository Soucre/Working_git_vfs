using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Domain.Model
{
    public class MAccDetailLog
    {
        public virtual int LogId { get; set; }

        public virtual string CustomerId { get; set; }

        public virtual string AccountId { get; set; }

        public virtual string ContractId { get; set; }

        public virtual string Status { get; set; }

        public virtual decimal Amount { get; set; }

        public virtual DateTime EffectiveOnDate { get; set; }

        public virtual decimal AmountCalInterest { get; set; }

        public virtual decimal InterestPayNow { get; set; }

        public virtual string LogType { get; set; }

        public virtual DateTime LogDate { get; set; }

    }
}
