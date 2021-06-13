using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PrestationService.Models;

namespace PrestationService.Controllers
{
    public class DocumentsController : Controller
    {
        private bdServiceContext db = new bdServiceContext();

        // GET: Documents
        public ActionResult Index()
        {
            return View(db.documents.ToList());
        }

        // GET: Documents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // GET: Documents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Documents/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "documentId,documentName,obligatoire")] Document document)
        {
            if (ModelState.IsValid)
            {
                db.documents.Add(document);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(document);
        }

        // GET: Documents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "documentId,documentName,obligatoire")] Document document)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(document);
        }

        // GET: Documents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Document document = db.documents.Find(id);
            db.documents.Remove(document);
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

        [HttpGet]
        public ActionResult Upload()
        {
            Document objdocument = new Document();

            objdocument.DocumentList = new List<Document>
            {
            new Document { documentId = 1,documentName ="Diplome", obligatoire=1 },
            new Document { documentId = 2,documentName ="Ninea", obligatoire=1},
            new Document { documentId = 3,documentName ="Registre de Commerce", obligatoire=1 },
            new Document { documentId = 4,documentName ="Photo", obligatoire=0 }
            };

            return View(objdocument);
        }

        [HttpPost]
        public ActionResult Upload(Document document)
        {
            var DocumentUpload = document.DocumentList;
            foreach (var Doc in DocumentUpload)
            {
                string strFileUpload = "file_" + Convert.ToString(Doc.documentId);
                HttpPostedFileBase file = Request.Files[strFileUpload];

                if (file != null && file.ContentLength > 0)
                {
                    // if you want to save in folder use this  
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Documents"), fileName);
                    file.SaveAs(path);

                    // if you want to store in Bytes in Database use this  
                    byte[] image = new byte[file.ContentLength];
                    file.InputStream.Read(image, 0, image.Length);

                }
            }
            return View(document);
        }
    }
}
