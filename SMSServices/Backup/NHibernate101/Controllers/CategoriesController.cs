using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Core.Domain.Model;
using Core;
using Core.Domain.Repositories;

namespace NHibernate101.Controllers
{
    public class CategoriesController : Controller
    {
        //
        // GET: /Categories/
        public ActionResult Index()
        {
            IRepository<Category> repo = new CategoryRepository();
            return View(repo.GetAll());
        }

        public ActionResult Details(Guid id)
        {
            IRepository<Category> repo = new CategoryRepository();
            return View(repo.GetById(id));
        }

        public ActionResult Edit(Guid id)
        {
            IRepository<Category> repo = new CategoryRepository();
            return View(repo.GetById(id));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Guid id, FormCollection formCollection)
        {
            string name = formCollection.Get("Name");
            Category category = new Category() { Id = id, Name = name };

            IRepository<Category> repo = new CategoryRepository();
            repo.Update(category);

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection formCollection)
        {
            string name = formCollection.Get("Name");
            Category category = new Category() { Name = name };

            IRepository<Category> repo = new CategoryRepository();
            repo.Save(category);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid id)
        {
            IRepository<Category> repo = new CategoryRepository();
            repo.Delete(repo.GetById(id));
            return RedirectToAction("Index");
        }
    }
}
