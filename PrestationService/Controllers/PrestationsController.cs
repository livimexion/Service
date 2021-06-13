using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PrestationService.Models;

namespace PrestationService.Controllers
{
    public class PrestationsController : Controller
    {
        private bdServiceContext db = new bdServiceContext();

        // GET: Prestations
        public ActionResult Index()
        {
            var prestations = db.prestations.Include(p => p.Service);
            return View(prestations.ToList());
        }

        // GET: Prestations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestation prestation = db.prestations.Find(id);
            if (prestation == null)
            {
                return HttpNotFound();
            }
            return View(prestation);
        }

        // GET: Prestations/Create
        public ActionResult Create()
        {
            ViewBag.Utilisateur = db.Utilisateurs.ToList();
            ViewBag.idService = new SelectList(db.services, "idService", "libelle");
            return View();
        }

        // POST: Prestations/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPrestation,adresse,date,id,idService")] Prestation prestation)
        {
            if (ModelState.IsValid)
            {
                db.prestations.Add(prestation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Utilisateur = db.Utilisateurs.ToList();
            ViewBag.idService = new SelectList(db.services, "idService", "libelle", prestation.idService);
            return View(prestation);
        }

        // GET: Prestations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestation prestation = db.prestations.Find(id);
            if (prestation == null)
            {
                return HttpNotFound();
            }
            ViewBag.Utilisateur = db.Utilisateurs.ToList();
            ViewBag.idService = new SelectList(db.services, "idService", "libelle", prestation.idService);
            return View(prestation);
        }

        // POST: Prestations/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPrestation,adresse,date,id,idService")] Prestation prestation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prestation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Utilisateur = db.Utilisateurs.ToList();
            ViewBag.idService = new SelectList(db.services, "idService", "libelle", prestation.idService);
            return View(prestation);
        }

        // GET: Prestations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestation prestation = db.prestations.Find(id);
            if (prestation == null)
            {
                return HttpNotFound();
            }
            return View(prestation);
        }

        // POST: Prestations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prestation prestation = db.prestations.Find(id);
            db.prestations.Remove(prestation);
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

        public ActionResult Prestater(/*int? id*/)
        {
        //    bdServiceContext db = new bdServiceContext();
        //    Prestation pres = new Prestation();
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Service service = db.services.Find(id);
        //    if (service == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    pres.idService = service.idService;
            
        //    string loginName = HttpContext.User.Identity.GetUserName();
        //    Utilisateur util = db.Utilisateurs.Where(a => a.identifiant == loginName).FirstOrDefault();
        //    pres.id = util.id;

           
            //ViewBag.idService = new SelectList(db.services, "idService", "libelle");
            return View(/*pres*/);
        }

        // POST: Prestations/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Prestater([Bind(Include = "idPrestation,adresse,date,id,idService")] Prestation prestation)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.prestations.Add(prestation);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Utilisateur = db.Utilisateurs.ToList();
        //    ViewBag.idService = new SelectList(db.services, "idService", "libelle", prestation.idService);
        //    return View(prestation);
        //}
    }
}
