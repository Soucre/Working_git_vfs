using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Domain.Model;

namespace NHibernate101.Models
{
    public class PostViewModel
    {
        public PostViewModel()
            : this(new Post(), new List<Category>())   
        {
        }



        public PostViewModel(Post post, IList<Category> allCategories)
        {
            Post = post;

            AllCategories = new List<PostCategory>();
            foreach (Category c in allCategories)
            {
                AllCategories.Add(new PostCategory(c, OnPost(c.Id)));
            }
        }

        private bool OnPost(Guid categoryId)
        {
            foreach (Category c in Post.Categories)
            {
                if (c.Id.ToString() == categoryId.ToString())
                    return true;
            }
            return false;
        }

        public PostViewModel(Post post)
            : this(post, new List<Category>())
        {
        }

        public PostViewModel(IList<Category> allCategories)
            : this(new Post(), allCategories)
        {
           
        }

        public Post Post { get; set; }
        public IList<PostCategory> AllCategories { get; set; }
    }
}
