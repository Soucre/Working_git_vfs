using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Domain.Model
{
    public class Category
    {
        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }
    }
}
