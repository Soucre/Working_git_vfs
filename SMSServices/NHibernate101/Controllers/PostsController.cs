using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using NHibernate101.Models;
using Core;
using Core.Domain.Model;
using Core.Domain.Repositories;

namespace NHibernate101.Controllers
{
    public class PostsController : Controller
    {
        //
        // GET: /Posts/

        public ActionResult Index()
        {
            IRepository<Post> repo = new PostRepository();
            IRepository<SecuritiesHist> repoSecuritieshist = new SecuritiesHistRepository();
            IRepository<RightExec> repoExec = new RightExecRepository();

            //IList<SecuritiesHist> listSecuritiesHist = repoSecuritieshist.GetAll();
            IList<RightExec> listRightExec = repoExec.GetAll();

            return View(repo.GetAll());
        }

        public ActionResult Create()
        {
            
            IRepository<Category> repo = new CategoryRepository();
            PostViewModel model = new PostViewModel(repo.GetAll());
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection formCollection)
        {
            string title = formCollection.Get("Title");
            string body = formCollection.Get("Body");
            bool isPublic = formCollection.Get("IsPublic").Contains("true");

            IRepository<Category> categoriesRepo = new CategoryRepository();
            IRepository<Post> postsRepo = new PostRepository();
            List<Category> allCategories = (List<Category>)categoriesRepo.GetAll();

            Post post = new Post();
            post.Body = body;
            post.Title = title;
            post.CreationDate = DateTime.Now;
            post.IsPublic = isPublic;

            foreach (Category category in allCategories)
            {
                if (formCollection.Get(category.Id.ToString()).Contains("true"))
                    post.Categories.Add(category);

            }

            postsRepo.Save(post);


            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid id)
        {
            IRepository<Post> repo = new PostRepository();
            repo.Delete(repo.GetById(id));
            return RedirectToAction("Index");
        }

        public ActionResult Details(Guid id)
        {
            IRepository<Post> repo = new PostRepository();
            Post post = repo.GetById(id);
            PostViewModel model = new PostViewModel(post);
            return View(model);
        }

        public ActionResult Edit(Guid id)
        {
            IRepository<Post> postsRepo = new PostRepository();
            IRepository<Category> categoriesRepo = new CategoryRepository();

            PostViewModel model = new PostViewModel(postsRepo.GetById(id), categoriesRepo.GetAll());

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Guid id, FormCollection formCollection)
        {
            string title = formCollection.Get("Title");
            string body = formCollection.Get("Body");
            bool isPublic = formCollection.Get("IsPublic").Contains("true");

            IRepository<Category> categoriesRepo = new CategoryRepository();
            IRepository<Post> postsRepo = new PostRepository();
            List<Category> allCategories = (List<Category>)categoriesRepo.GetAll();

            Post post = new Post();
            post.Body = body;
            post.Title = title;
            post.IsPublic = isPublic;
            post.Id = id;

            foreach (Category category in allCategories)
            {
                if (formCollection.Get(category.Id.ToString()).Contains("true"))
                    post.Categories.Add(category);
            }

            postsRepo.Update(post);

            return RedirectToAction("Index");
        }


    }
}
