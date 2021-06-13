using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestationService.Controllers
{
    public class HomeServiceController : Controller
    {
        // GET: HomeService
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BricoMobilier()
        {
            return View();
        }
    }
}