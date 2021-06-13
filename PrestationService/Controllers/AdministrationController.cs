using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestationService.Controllers
{
    public class AdministrationController : Controller
    {
        // GET: Administration
        public ActionResult Accueil()
        {
            return View();
        }

        public ActionResult Service()
        {
            return View();
        }
    }
}