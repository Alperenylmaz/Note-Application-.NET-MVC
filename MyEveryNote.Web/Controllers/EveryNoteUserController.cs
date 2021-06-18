using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyEveryNote.BusinessLayer;
using MyEveryNote.BusinessLayer.Results;
using MyEveryNote.Entity;
using MyEveryNote.Web.Models;

namespace MyEveryNote.Web.Controllers
{
    public class EveryNoteUserController : Controller
    {
        EveryNoteUserManager everyNoteUserManager = new EveryNoteUserManager();

        // GET: EveryNoteUser
        public ActionResult Index()
        {
            return View(everyNoteUserManager.List());
        }

        // GET: EveryNoteUser/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EveryNoteUser everyNoteUser = everyNoteUserManager.Find(x=> x.Id == id.Value);
            if (everyNoteUser == null)
            {
                return HttpNotFound();
            }
            return View(everyNoteUser);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EveryNoteUser everyNoteUser)
        {
            ModelState.Remove("CreationOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                BusinessLayerResult<EveryNoteUser> res = everyNoteUserManager.Insert(everyNoteUser);
                if(res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                }
                return RedirectToAction("Index");
            }

            return View(everyNoteUser);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EveryNoteUser everyNoteUser = everyNoteUserManager.Find(x => x.Id == id.Value);
            if (everyNoteUser == null)
            {
                return HttpNotFound();
            }
            return View(everyNoteUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EveryNoteUser everyNoteUser)
        {
            ModelState.Remove("CreationOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {

                everyNoteUserManager.Update(everyNoteUser);
                return RedirectToAction("Index");
            }
            return View(everyNoteUser);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            EveryNoteUser everyNoteUser = everyNoteUserManager.Find(x => x.Id == id.Value);
            if (everyNoteUser == null)
            {
                return HttpNotFound();
            }
            return View(everyNoteUser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EveryNoteUser everyNoteUser = everyNoteUserManager.Find(x => x.Id == id);
            everyNoteUserManager.Delete(everyNoteUser);
            return RedirectToAction("Index");
        }
    }
}
