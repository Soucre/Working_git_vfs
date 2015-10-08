using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Domain.Repositories
{
    public interface IVFS_RightExecDetailCustomerRepository<T>
    {
        IList<T> getListRightExecDetailCustomerFromIdRightExec(int id);
    }
}
