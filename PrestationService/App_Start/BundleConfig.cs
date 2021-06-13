using System.Web;
using System.Web.Optimization;

namespace PrestationService
{
    public class BundleConfig
    {
        // Pour plus d'informations sur le regroupement, visitez https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilisez la version de développement de Modernizr pour le développement et l'apprentissage. Puis, une fois
            // prêt pour la production, utilisez l'outil de génération à l'adresse https://modernizr.com pour sélectionner uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/validateDoc.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/PagedList.css",
                      "~/Content/modalProfile",
                      "~/Content/pay.css",
                      "~/Content/site.css",
                      "~/Content/Fact.css"));

            bundles.Add(new StyleBundle("~/Asset/vendor").Include(
                      "~/Asset/vendor/fontawesome-free/css/all.css",
                      "~/Asset/vendor/datatables/dataTables.bootstrap4.css"

                      ));
            bundles.Add(new StyleBundle("~/Asset/css").Include(
                      "~/Asset/css/sb-admin-2.css"
                      
                      ));

            bundles.Add(new ScriptBundle("~/Asset/js").Include(
                     "~/Asset/vendor/jquery/jquery.min.js",
                     "~/Asset/vendor/bootstrap/js/bootstrap.bundle.min.js",
                     "~/Asset/vendor/jquery-easing/jquery.easing.min.js",
                     "~/Asset/js/sb-admin-2.min.js",
                     "~/Asset/vendor/datatables/jquery.dataTables.min.js",
                     "~/Asset/vendor/datatables/dataTables.bootstrap4.min.js",
                     "~/Asset/js/demo/datatables-demo.js",
                     "~/Asset/js/demo/chart-pie-demo.js",
                     "~/Asset/js/demo/chart-bar-demo.js",
                     "~/Asset/js/demo/chart-area-demo.js"
                     ));

        }
    }
}
 