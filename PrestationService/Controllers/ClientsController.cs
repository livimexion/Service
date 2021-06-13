using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PrestationService.Models;

namespace PrestationService.Controllers
{
    public class ClientsController : Controller
    {
        private bdServiceContext db = new bdServiceContext();

        // GET: Clients
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //ViewBag.searchString = searchString != null ? searchString : "";
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.LastSortParm = String.IsNullOrEmpty(sortOrder) ? "last_name_desc" : "";

            page = page.HasValue ? page : 1;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var clients = from s in db.clients
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                //clients = clients.Where(s => s.nom.Contains(searchString)
                //                       || s.prenom.Contains(searchString));
            }
            //switch (sortOrder)
            //{
            //    case "name_desc":
            //        clients = clients.OrderByDescending(s => s.nom);
            //        break;
            //    case "last_name_desc":
            //        clients = clients.OrderByDescending(s => s.prenom);
            //        break;
            //    default:  // Name ascending 
            //        clients = clients.OrderBy(s => s.nom);
            //        break;
            //}

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(/*db.clients.OrderBy(i=> i.nom).ToPagedList(pageNumber, pageSize)*/);

      
        }


        //recherche et pagination
        //public ActionResult Recherche(string searchString, int? page)
        //{

        //    ViewBag.searchString = searchString != null ? searchString : "";
        //    page = page.HasValue ? page : 1;
        //    var clients = db.clients.ToList();

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        clients = clients.Where(c => c.nom.Contains(searchString)).ToList();
        //    }
        //    int pageSize = 3;
        //    int pageNumber = (page ?? 1);
        //    return View(clients.ToPagedList(pageNumber, pageSize));
        //}

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdClient,naissance,adresse,tel,identification")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdClient,naissance,adresse,tel,identification")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.clients.Find(id);
            db.clients.Remove(client);
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
