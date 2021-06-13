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
    public class TransfertsController : Controller
    {
        private bdServiceContext db = new bdServiceContext();

        // GET: Transferts
        public ActionResult Index()
        {
            return View(db.paiements.ToList());
        }

        // GET: Transferts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transfert transfert = db.transferts.Find(id);
            if (transfert == null)
            {
                return HttpNotFound();
            }
            return View(transfert);
        }

        // GET: Transferts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Transferts/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPay,typPay,acompte,typeTransfert,numero")] Transfert transfert)
        {
            if (ModelState.IsValid)
            {
                db.paiements.Add(transfert);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transfert);
        }

        // GET: Transferts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transfert transfert = db.transferts.Find(id);
            if (transfert == null)
            {
                return HttpNotFound();
            }
            return View(transfert);
        }

        // POST: Transferts/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPay,typPay,acompte,typeTransfert,numero")] Transfert transfert)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transfert).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transfert);
        }

        // GET: Transferts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transfert transfert = db.transferts.Find(id);
            if (transfert == null)
            {
                return HttpNotFound();
            }
            return View(transfert);
        }

        // POST: Transferts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transfert transfert = db.transferts.Find(id);
            db.paiements.Remove(transfert);
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
