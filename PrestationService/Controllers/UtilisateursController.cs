using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using PrestationService.App_Start;
using PrestationService.Models;


namespace PrestationService.Controllers
{
    
    public class UtilisateursController : Controller
    {
        private bdServiceContext db = new bdServiceContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public UtilisateursController()
        {
        }

        public UtilisateursController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        [Authorize(Roles = "Admin")]
        // GET: Utilisateurs
        public ActionResult Index()
        {
            return View(db.Utilisateurs.ToList());
        }

        // GET: Utilisateurs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = db.Utilisateurs.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        [Authorize(Roles = "Admin")]
        // GET: Utilisateurs/Create
        public ActionResult Create()
        {
            ViewBag.Role = db.roles.ToList();
            return View();
        }

        // POST: Utilisateurs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,iduser,identifiant,nom,prenom,email,role,tel")] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                string iduser = CreateUserMvc(utilisateur.identifiant, utilisateur.email, "P@sser123", utilisateur.role);
                if (!string.IsNullOrEmpty(iduser))
                {
                    utilisateur.iduser = iduser;
                    db.Utilisateurs.Add(utilisateur);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Role = db.roles.ToList();
            return View(utilisateur);
        }

        [Authorize(Roles = "Admin")]
        // GET: Utilisateurs/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Role = db.roles.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = db.Utilisateurs.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        // POST: Utilisateurs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,iduser,identifiant,nom,prenom,email,role,tel")] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                var leU = db.Utilisateurs.Where(a => a.identifiant == utilisateur.identifiant).FirstOrDefault();
                bool isUpdate = UpdateUserRoleMvc(utilisateur.identifiant, leU.role, utilisateur.role);
                if (isUpdate)
                {
                    db.Entry(utilisateur).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Role = db.roles.ToList();
            return View(utilisateur);
        }

        [Authorize(Roles ="Admin")]
        // GET: Utilisateurs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = db.Utilisateurs.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        // POST: Utilisateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utilisateur utilisateur = db.Utilisateurs.Find(id);
            db.Utilisateurs.Remove(utilisateur);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #region gestion Utilisateur

        public bool SetRole(string name, string profil)
        {
            bool rep = false;
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser();

            user = UserManager.FindByName(name);
            UserManager.AddToRole(user.Id, profil);
            //to do: Mise à jour base de données

            rep = true;
            return rep;
        }

        public string CreateUserMvc(string name, string email, string password, string profil)
        {
            string rep = "";
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            //var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = new ApplicationUser();

            user.UserName = name;
            user.Email = email;

            string userPWD = password;

            var chkUser = UserManager.Create(user, userPWD);
            if (chkUser.Succeeded)
            {
                rep = user.Id;
                var result1 = UserManager.AddToRole(user.Id, profil);
            }
            else
            {
                return "";
            }

            string code = UserManager.GenerateEmailConfirmationToken(user.Id);
            try
            {
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                GMailer.senMail(user.Email, "Confirmez votre compte", "Confirmez votre compte en cliquant <a href=\"" + callbackUrl + "\">ici</a>");
            }
            catch(Exception ex)
            {

            }

            return rep;
        }

        public bool UpdateUserRoleMvc(string name, string odlProfil, string newProfil)
        {

            try
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser();
                //user.UserName = name;

                user = UserManager.FindByName(name);

                UserManager.RemoveFromRole(user.Id, odlProfil);

                UserManager.AddToRole(user.Id, newProfil);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool RemoveUserRoleMvcOrbusV2(string name, string odlProfil)
        {

            try
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser();

                user = UserManager.FindByName(name);

                UserManager.RemoveFromRole(user.Id, odlProfil);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool setEmailUtilisateur(string email, string name)
        {
            bool rep = false;
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser();

            user = UserManager.FindByName(name);

            UserManager.SetEmail(user.Id, email);

            rep = true;

            return rep;
        }


        public string getUserNameById(string id)
        {
            try
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser();
                //user.UserName = name;

                user = UserManager.FindById(id);

                return user.UserName;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public bool ChangeUserRoleMvc(string name, string newProfil)
        {
            try
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser();

                user = UserManager.FindByName(name);

                var roles = UserManager.GetRoles(user.Id);
                UserManager.RemoveFromRoles(user.Id, roles.ToArray());

                UserManager.AddToRole(user.Id, newProfil);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public string VerifIdentifiantAndEmail(string identifiant, string email)
        {
            string rep = string.Empty;
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser();
            user = UserManager.FindByEmail(email);
            if (user != null)
            {
                return string.Format("Cet Email n'est pas valide ");
            }
            user = UserManager.FindByName(identifiant);
            if (user != null)
            {
                return string.Format("Cet Identifiant n'est pas valide");
            }
            return rep;
        }

        public async Task<bool> SendMailRegister(string email, string url)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser();
            user = await UserManager.FindByEmailAsync(email);

            var provider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("OrbusAdministration");
            UserManager.UserTokenProvider = new Microsoft.AspNet.Identity.Owin.DataProtectorTokenProvider<ApplicationUser>(provider.Create("EmailConfirmation"));

            string Code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            var callbackUrl = Url.Action("ConfirmEmail", "Account",
               new { userId = user.Id, code = Code }, protocol: url);

            GMailer.senMail(email, "Reinitialisation mot de passe", string.Format("Merci de cliquez sur ce lien <a href=\"{0}\">ici</a> ", callbackUrl));
            //await GMailer.senMail(email, "Reinitialisation mot de passe", callbackUrl, user.Id, Code);
            return true;
        }

        #endregion

        
        // GET: Utilisateurs/Register
        public ActionResult RegisterClient()
        {
            Utilisateur utilisateur = new Utilisateur();
            utilisateur.role = "Client";
            return View(utilisateur);
        }

        // POST: Utilisateurs/Register
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterClient([Bind(Include = "id,iduser,identifiant,nom,prenom,email,tel,role")] Utilisateur utilisateur)
        {
           
            if (ModelState.IsValid)
            {
               
                string iduser = CreateUserMvc(utilisateur.identifiant, utilisateur.email, "P@sser123", utilisateur.role);
                if (!string.IsNullOrEmpty(iduser))
                {
                    // bool env = SendMailRegister(utilisateur.email, );
                    utilisateur.iduser = iduser;
                    db.Utilisateurs.Add(utilisateur);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
           // ViewBag.Role = db.roles.ToList();
            return View(utilisateur);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }

                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ProfilClient()
        {
            bdServiceContext bd = new bdServiceContext();

            if (HttpContext.User.Identity.IsAuthenticated)
            {

                Utilisateur user = new Utilisateur();
                string loginName = HttpContext.User.Identity.GetUserName();
                Utilisateur util = bd.Utilisateurs.Where(a => a.identifiant == loginName).FirstOrDefault();
                user.identifiant = loginName;
                //var id = user.id;
                if (loginName == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                //Utilisateur utilisateur = db.Utilisateurs.Find(id);
              //user = db.Utilisateurs.Find();
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(util);

                //Utilisateur utilisateur = db.Utilisateurs.Find(id);
                //var client = db.Utilisateurs.Where(a => a.iduser == ID).FirstOrDefault();


                //ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                // var user = bd.Utilisateur.Where(a => a.iduser.Equals(ID)).FirstOrDefault();
             
            }
            return RedirectToAction("login", "Account");
        }

        public DataTable GetTableUser()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Identifiant", typeof(string));
            table.Columns.Add("Nom", typeof(string));
            table.Columns.Add("Prenom", typeof(string));
            table.Columns.Add("Email", typeof(string));
            table.Columns.Add("Telephone", typeof(string));
            table.Columns.Add("Role", typeof(string));
            List<Utilisateur> listuser = db.Utilisateurs.ToList();
            foreach (var u in listuser) { table.Rows.Add(u.identifiant, u.nom, u.prenom, u.email, u.tel, u.role); }
            return table;
        }

        public ActionResult ReportUser()
        {
            CrystalDecisions.CrystalReports.Engine.ReportDocument RptListeUser
= new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            try
            {
                RptListeUser.Load(Server.MapPath("~/Report/RptUser.rpt"));
                RptListeUser.SetDataSource(GetTableUser());
                Stream stream =
                RptListeUser.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                Response.AppendHeader("Content-Disposition", "inline");
                return File(stream, "application/pdf");
            }
            finally
            {
                RptListeUser.Dispose();
                RptListeUser.Close();
            }
        }
    }
}
