using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Domain.Model
{
    public class Post
    {
        public Post()
        {
            Categories = new List<Category>();
        }
        
        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Body { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual bool IsPublic { get; set; }
        public virtual IList<Core.Domain.Model.Category> Categories { get; set; }
    }
}
