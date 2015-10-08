using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain.Model;

namespace Core
{
    public interface ISecuritiesHistRepository
    {
        IList<SecuritiesHist> getSecuritiesHistByStockCodeAndTransactionDate(string stockCode, DateTime transactionDate);
    }
}
