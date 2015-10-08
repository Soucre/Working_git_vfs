using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Domain.Model
{
    public class VFS_Customer
    {
        public virtual int Id
        {
            get;
            set;
        }
        public virtual string CustomerId
        {
            get;
            set;
        }

        public virtual String CustomerName
        {
            get;
            set;
        }

        public virtual string CustomerNameViet
        {
            get;
            set;
        }
        public virtual string Mobile
        {
            get;
            set;
        }

        public virtual Nullable<DateTime> Dob
        {
            get;
            set;
        }

      

    }
}
