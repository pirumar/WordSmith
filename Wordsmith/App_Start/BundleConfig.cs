using System.Web;
using System.Web.Optimization;

namespace Wordsmith
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js" ,
                      "~/Scripts/plugins.js",
                      "~/Scripts/main.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/base.css",
                      "~/Content/vendor.css",
                      "~/Content/main.css"));
            //dashboard component
            bundles.Add(new StyleBundle("~/bundles/dashboard/css").Include(
                "~/Content/assets/css/cs-skin-elastic.css",
                "~/Content/assets/css/style.css"));

            bundles.Add(new ScriptBundle("~/bundles/dashboard/mainjs").Include(
            "~/Content/assets/js/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/dashboard/js").Include(

            "~/Content/assets/js/init/fullcalendar-init.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/dashboard/jsweat").Include(

            "~/Content/assets/js/init/weather-init.js" 
            ));

        }
    }
}
