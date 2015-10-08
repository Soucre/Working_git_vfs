using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public interface IRightExecRepository<T>
    {
        IList<T> getRightExecListFromStock(string stock);
    }
}
