using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain.Model;

namespace Core
{
    public interface IMAccDetailLogRepository<T>
    {
        IList<MAccDetailLog> getListFromCustomer(string accountId);
        IList<String> getListAllCutomer();
        void truncateTable();
    }
}
