using System.Data.Entity;
using System.Linq;
using System.Net;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using PrestationService.Models;

namespace PrestationService.Controllers
{
    public class ServicesController : Controller
    {
        private bdServiceContext db = new bdServiceContext();

        // GET: Services
        public ActionResult Index()
        {
            var services = db.services.Include(s => s.SousCategorie);
            return View(services.ToList());
        }

        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            // Utilisateur util = db.Utilisateurs.Where(a => a.role == "Professionnel").FirstOrDefault();
            //List<Utilisateur> listuser = db.Utilisateurs.Where(a => a.role == "Professionnel").ToList();
             ViewBag.Utilisateur = db.Utilisateurs.ToList();
            // ViewBag.Utilisateur = util.id;

            ViewBag.idSouCat = new SelectList(db.SousCategories, "idSouCat", "libSouCat");
            return View();
        }

        // POST: Services/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idService,libelle,description,prix,nbHeure,id,idSouCat")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Utilisateur = db.Utilisateurs.ToList();
            ViewBag.idSouCat = new SelectList(db.SousCategories, "idSouCat", "libSouCat", service.idSouCat);
            return View(service);
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            ViewBag.Utilisateur = db.Utilisateurs.ToList();
            ViewBag.idSouCat = new SelectList(db.SousCategories, "idSouCat", "libSouCat", service.idSouCat);
            return View(service);
        }

        // POST: Services/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idService,libelle,description,prix,nbHeure,id,idSouCat")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Utilisateur = db.Utilisateurs.ToList();
            ViewBag.idSouCat = new SelectList(db.SousCategories, "idSouCat", "libSouCat", service.idSouCat);
            return View(service);
        }

        // GET: Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = db.services.Find(id);
            db.services.Remove(service);
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
        public ActionResult AllService()
        {
            var services = db.services/*.Include(s => s.Utilisateur)*/.Include(s => s.SousCategorie);
            return View(services.ToList());

        }
        // GET: Services/Create
        public ActionResult NewService()
        {
            bdServiceContext bd = new bdServiceContext();
            
                Service serv = new Service();
                string loginName = HttpContext.User.Identity.GetUserId();
                Utilisateur util = bd.Utilisateurs.Where(a => a.iduser == loginName).FirstOrDefault();
                serv.id = util.id;
            
            //ViewBag.Utilisateur = db.Utilisateurs.ToList();
            // ViewBag.IdProfessionnel = new SelectList(db.professionnels, "IdProfessionnel", "nomComplet");
            ViewBag.idSouCat = new SelectList(db.SousCategories, "idSouCat", "libSouCat");
            return View(serv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewService([Bind(Include = "idService,libelle,description,prix,nbHeure,id,idSouCat")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.services.Add(service);
                db.SaveChanges();
                return RedirectToAction("AllService");
            }

            ViewBag.Utilisateur = db.Utilisateurs.ToList();
            ViewBag.idSouCat = new SelectList(db.SousCategories, "idSouCat", "libSouCat", service.idSouCat);
            return View(service);
        }

        public JsonResult GetbyID(int ID)
        {
            var service = db.services.ToList().Find(x => x.idService.Equals(ID));
            return Json(service, JsonRequestBehavior.AllowGet); }
    }
}
