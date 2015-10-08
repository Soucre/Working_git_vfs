using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Domain.Model
{
    public class VFS_MAccDetailLogBlance
    {
        public virtual int LogId { get; set; }

        public virtual decimal Balance { get; set; }
    }
}
