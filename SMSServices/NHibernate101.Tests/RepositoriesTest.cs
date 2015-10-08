using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Domain.Model;
using Core.Domain.Repositories;
using Core;

namespace NHibernate101.Tests
{
    [TestClass]
    public class RepositoriesTest
    {
        IRepository<Category> categoriesRepository;
        IRepository<Post> postsRepository;
        Post testPost;
        Category testCategory1;
        Category testCategory2;

        public RepositoriesTest()
        {
        }

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }


        [TestInitialize()]
        public void CreateRepositories()
        {
            categoriesRepository = new CategoryRepository();
            postsRepository = new PostRepository();
        }


        [TestMethod]
        [DeploymentItem("hibernate.cfg.xml")]
        public void CanCreateCategory()
        {
            testCategory1 = new Category() { Name = "ASP.NET" };
            categoriesRepository.Save(testCategory1);

        }

        [TestMethod]
        [DeploymentItem("hibernate.cfg.xml")]
        public void CanCreatePost()
        {
            testPost = new Post();
            testPost.Title = "ASP.NET MVC and NHibernate";
            testPost.Body = "In this article I’m going to cover how to install and configure NHibernate and use it in a ASP.NET MVC application.";
            testPost.CreationDate = DateTime.Now;
            testPost.IsPublic = true;

            testCategory2 = new Category() { Name= "ASP.NET MVC"};

            categoriesRepository.Save(testCategory2);
            testPost.Categories.Add(testCategory2);

            postsRepository.Save(testPost);

        }
    }
}