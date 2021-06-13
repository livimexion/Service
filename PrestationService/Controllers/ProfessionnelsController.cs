using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PrestationService.Models;

namespace PrestationService.Controllers
{
    public class ProfessionnelsController : Controller
    {
        private bdServiceContext db = new bdServiceContext();

        // GET: Professionnels
        public ActionResult Index(/*string sortOrder, string currentFilter, string searchString, int? page*/)
        {
            //ViewBag.CurrentSort = sortOrder;
            //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewBag.LastSortParm = String.IsNullOrEmpty(sortOrder) ? "last_name_desc" : "";

            //page = page.HasValue ? page : 1;

            //if (searchString != null)
            //{
            //    page = 1;
            //}
            //else
            //{
            //    searchString = currentFilter;
            //}

            //ViewBag.CurrentFilter = searchString;

            //var professionnels = from s in db.professionnels
            //              select s;
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    //professionnels = professionnels.Where(s => s.nom.Contains(searchString)
            //    //                       || s.prenom.Contains(searchString));
            //}
            //var test  = HttpContext.Session["Prof"];
            //if (test.ToString() != String.Empty  )
            //{
            //    //professionnels = professionnels.Where(s => s.prenom.Contains(test.ToString ()));
            //}
            ////switch (sortOrder)
            ////{
            ////    case "name_desc":
            ////        professionnels = professionnels.OrderByDescending(s => s.nom);
            ////        break;
            ////    case "last_name_desc":
            ////        professionnels = professionnels.OrderByDescending(s => s.prenom);
            ////        break;
            ////    default:  // Name ascending 
            ////        professionnels = professionnels.OrderBy(s => s.nom);
            ////        break;
            ////}

            //int pageSize = 3;
            //int pageNumber = (page ?? 1);
            return View(db.professionnels.ToList()/*db.professionnels.OrderBy(s => s.nom).ToPagedList(pageNumber, pageSize)*/);

        }

        // GET: Professionnels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professionnel professionnel = db.professionnels.Find(id);
            if (professionnel == null)
            {
                return HttpNotFound();
            }
            return View(professionnel);
        }

        // GET: Professionnels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Professionnels/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProfessionnel,adresse,diplome,specialite,ninea,registre,experience")] Professionnel professionnel,HttpPostedFileBase upload)
        {



            try
            {
                if (ModelState.IsValid)
                {
                    //if (upload != null && upload.ContentLength > 0)
                    //{
                    //    var avatar = new File
                    //    {
                    //        FileName = System.IO.Path.GetFileName(upload.FileName),
                    //        FileType = FileType.Avatar,
                    //        ContentType = upload.ContentType
                    //    };
                    //    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    //    {
                    //        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    //    }
                    //    professionnel.Files = new List<File> { avatar };
                    //}
                    db.professionnels.Add(professionnel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(professionnel);

            
        }

        // GET: Professionnels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professionnel professionnel = db.professionnels.Find(id);
            if (professionnel == null)
            {
                return HttpNotFound();
            }
            return View(professionnel);
        }

        // POST: Professionnels/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProfessionnel,adresse,diplome,specialite,ninea,registre,experience")] Professionnel professionnel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(professionnel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(professionnel);
        }

        // GET: Professionnels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professionnel professionnel = db.professionnels.Find(id);
            if (professionnel == null)
            {
                return HttpNotFound();
            }
            return View(professionnel);
        }

        // POST: Professionnels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Professionnel professionnel = db.professionnels.Find(id);
            db.professionnels.Remove(professionnel);
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
        public ActionResult Profile(int? id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var user = new UserConnect();
                ViewData["test"] = HttpContext.User.Identity.Name + " " + HttpContext.Session["mail"];
                ViewData["NomComplet"] = HttpContext.Session["nomComplet"];
                ViewData["Specialite"] = HttpContext.Session["specialite"];
                ViewData["Photo"] = HttpContext.Session["photo"];
                return View();

            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professionnel professionnel = db.professionnels.Find(id);
            if (professionnel == null)
            {
                return HttpNotFound();
            }
            return View(professionnel);
            // return View();
        }
        public ActionResult ModalProfile(int? id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var user = new UserConnect();
                ViewData["test"] = HttpContext.User.Identity.Name + " " + HttpContext.Session["mail"];
                ViewData["NomComplet"] = HttpContext.Session["nomComplet"];
                ViewData["mail"] = HttpContext.Session["mail"];
                ViewData["Specialite"] = HttpContext.Session["specialite"];
                ViewData["Photo"] = HttpContext.Session["photo"];
                ViewData["Id"] = HttpContext.Session["Id"];
                return View();

            }
            if (id == null)
            {
                // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("login", "Account");
            }
            Professionnel professionnel = db.professionnels.Find(id);
            if (professionnel == null)
            {
                return HttpNotFound();
            }
            return View(professionnel);
            // return View();
        }
        public ActionResult EditModal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professionnel professionnel = db.professionnels.Find(id);
            if (professionnel == null)
            {
                return HttpNotFound();
            }
            return View(professionnel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditModal([Bind(Include = "IdProfessionnel,nom,prenom,login,naissance,adresse,mail,tel,identification,diplome,specialite,ninea,registre,statut,experience,photo")] Professionnel professionnel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(professionnel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Home", "Home");
            }
            return View(professionnel);
        }
    }
}
