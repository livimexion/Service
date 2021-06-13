using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using PrestationService.Models;

[assembly: OwinStartupAttribute(typeof(PrestationService.Startup))]
namespace PrestationService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            bdServiceContext db = new bdServiceContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first ADMINISTRATEUR SYSTÈME Role and creating a default Admin User   
            if (!roleManager.RoleExists("Admin"))
            {
                // first we create Admin rool  
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
                Role p = new Role();
                p.libRole = role.Name;
                db.roles.Add(p);
                db.SaveChanges();
                //Here we create a Admin super user who will maintain the website                 

                var user = new ApplicationUser();
                user.UserName = "Admin";
                user.Email = "coucoumpc@gmail.com";

                string userPWD = "P@ssword123";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin  
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }

            // creating Promoteur role   
            if (!roleManager.RoleExists("Client"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Client";
                roleManager.Create(role);
                Role p = new Role();
                p.libRole = role.Name;
                db.roles.Add(p);
                db.SaveChanges();
            }

            // creating DirecteurDEPS role   
            if (!roleManager.RoleExists("Agent"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Agent";
                roleManager.Create(role);
                Role p = new Role();
                p.libRole = role.Name;
                db.roles.Add(p);
                db.SaveChanges();
            }

            // creating ChefServiceGuichet role   
            if (!roleManager.RoleExists("Professionnel"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Professionnel";
                roleManager.Create(role);
                Role p = new Role();
                p.libRole = role.Name;
                db.roles.Add(p);
                db.SaveChanges();
            }
        }
    }
}
