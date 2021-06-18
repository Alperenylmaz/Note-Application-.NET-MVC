using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyEveryNote.BusinessLayer;
using MyEveryNote.Entity;
using MyEveryNote.Web.Models;

namespace MyEveryNote.Web.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryManager categoryManager = new CategoryManager();

        public ActionResult Index()
        {
            return View(categoryManager.List());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryManager.Find(x=> x.Id == id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            ModelState.Remove("CreationOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                categoryManager.Insert(category);
                CacheHelper.RemoveCategoriesFromCache();
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryManager.Find(x => x.Id == id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            ModelState.Remove("CreationOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                Category ctg = categoryManager.Find(x => x.Id == category.Id);
                ctg.Title = category.Title;
                ctg.Description = category.Description;

                int hede = categoryManager.Update(ctg);
                CacheHelper.RemoveCategoriesFromCache();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryManager.Find(x => x.Id == id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = categoryManager.Find(x => x.Id == id);
            categoryManager.Delete(category);
            CacheHelper.RemoveCategoriesFromCache();
            return RedirectToAction("Index");
        }

    }
}
