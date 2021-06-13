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
    public class CartesController : Controller
    {
        private bdServiceContext db = new bdServiceContext();

        // GET: Cartes
        public ActionResult Index()
        {
            return View(db.paiements.ToList());
        }

        // GET: Cartes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carte carte = db.cartes.Find(id);
            if (carte == null)
            {
                return HttpNotFound();
            }
            return View(carte);
        }

        // GET: Cartes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cartes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPay,typPay,acompte,typeCarte,numeroCarte,numeroCsv,expiration")] Carte carte)
        {
            if (ModelState.IsValid)
            {
                db.paiements.Add(carte);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carte);
        }

        // GET: Cartes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carte carte = db.cartes.Find(id);
            if (carte == null)
            {
                return HttpNotFound();
            }
            return View(carte);
        }

        // POST: Cartes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPay,typPay,acompte,typeCarte,numeroCarte,numeroCsv,expiration")] Carte carte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carte).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carte);
        }

        // GET: Cartes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carte carte = db.cartes.Find(id);
            if (carte == null)
            {
                return HttpNotFound();
            }
            return View(carte);
        }

        // POST: Cartes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Carte carte = db.cartes.Find(id);
            db.paiements.Remove(carte);
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
