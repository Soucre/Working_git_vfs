using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Domain.Model;

namespace NHibernate101.Models
{
    public class PostCategory
    {
        public PostCategory()
        {
        }

        public PostCategory(Category category, bool selected)
        {
            Category = category;
            IsSelected = selected;
        }

        public Category Category { get; set; }
        public bool IsSelected { get; set; }
    }
}
