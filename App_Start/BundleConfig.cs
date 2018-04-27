using System.Web;
using System.Web.Optimization;

namespace SavingsManager
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/vendors/jquery-{version}.js",
                "~/Scripts/vendors/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/vendors/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/vendors/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/vendors/bootstrap.js",
                      "~/Scripts/vendors/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/kendo.common.css",
                      "~/Content/kendo.blueopal.css",
                      "~/Content/jquery-ui.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                    "~/Scripts/Kendoui/kendo.web.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/savings").Include(
            
            "~/Scripts/grupo.js",
            "~/Scripts/socio.js", 
            "~/Scripts/plan.js",
            "~/Scripts/ahorro.js",
            "~/Scripts/savings.js"
            
            ));
        }
    }
}
