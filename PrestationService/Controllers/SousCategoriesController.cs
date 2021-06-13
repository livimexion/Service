using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PrestationService.Models;

namespace PrestationService.Controllers
{
    public class SousCategoriesController : Controller
    {
        private bdServiceContext db = new bdServiceContext();

        // GET: SousCategories
        public ActionResult Index()
        {
            var sousCategories = db.SousCategories.Include(s => s.Categorie);
            return View(sousCategories.ToList());
        }

        // GET: SousCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SousCategorie sousCategorie = db.SousCategories.Find(id);
            if (sousCategorie == null)
            {
                return HttpNotFound();
            }
            return View(sousCategorie);
        }

        // GET: SousCategories/Create
        public ActionResult Create()
        {
            //ViewBag.Categorie = db.categories.ToList();
           ViewBag.IdCat = new SelectList(db.categories, "idCat", "libCat");
            return View();
        }

        // POST: SousCategories/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idSouCat,libSouCat,description,photo,idCat")] SousCategorie sousCategorie)
        {
            if (ModelState.IsValid)
            {
                db.SousCategories.Add(sousCategorie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

          ViewBag.IdCat = new SelectList(db.categories, "idCat", "libCat", sousCategorie.idCat);
            return View(sousCategorie);
        }

        // GET: SousCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SousCategorie sousCategorie = db.SousCategories.Find(id);
            if (sousCategorie == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCat = new SelectList(db.categories, "idCat", "libCat", sousCategorie.idCat);
            return View(sousCategorie);
        }

        // POST: SousCategories/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idSouCat,libSouCat,description,photo,idCat")] SousCategorie sousCategorie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sousCategorie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCat = new SelectList(db.categories, "idCat", "libCat", sousCategorie.idCat);
            return View(sousCategorie);
        }

        // GET: SousCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SousCategorie sousCategorie = db.SousCategories.Find(id);
            if (sousCategorie == null)
            {
                return HttpNotFound();
            }
            return View(sousCategorie);
        }

        // POST: SousCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SousCategorie sousCategorie = db.SousCategories.Find(id);
            db.SousCategories.Remove(sousCategorie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
