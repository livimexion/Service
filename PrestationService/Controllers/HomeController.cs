using PrestationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestationService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            //user.userName = HttpContext.User.Identity.Name;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var user = new UserConnect();
                ViewData["test"] = HttpContext.User.Identity.Name +" " + HttpContext.Session["mail"];

                return View();

            }
            return View();

            //else 
            //return  RedirectToAction(Indexkkk);
            // ViewData["test1"]=HttpContext.Session["MailProf"];
            // ViewData["test2"]=HttpContext.Session["IdProf"];
            //if (Session["UserID"] != null)
            //{
            //    return View();
            //}



        }
        public String Indexkkk()
        {
           
                return "bonjour";
           

        }

        public ActionResult Home()
        {
            return View();
        }
         public ActionResult Bricolage()
        {
            return View();
        }
        public ActionResult Informatique()
        {
            return View();
        }
        public ActionResult Jardinage()
        {
            return View();
        }
        public ActionResult Mecanique()
        {
            return View();
        }
        public ActionResult Medecine()
        {
            return View();
        }
        public ActionResult Menage()
        {
            return View();
        }
        public ActionResult Menuiserie()
        {
            return View();
        }
        public ActionResult Voyage()
        {
            return View();
        }
        public ActionResult Evenementiel()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}