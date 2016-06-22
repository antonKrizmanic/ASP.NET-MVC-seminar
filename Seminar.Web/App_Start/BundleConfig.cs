using System.Web;
using System.Web.Optimization;

namespace Seminar.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/moment.js",
                        "~/Scripts/moment-with-locales.js",
                        "~/Scripts/bootstrap-datetimepicker.js"
            ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/site.css"));
            bundles.Add(new ScriptBundle("~/bundles/globalize").Include(
                        "~/Scripts/cldr.js",
                        "~/Scripts/cldr/event.js",
                        "~/Scripts/cldr/supplemental.js",
                        "~/Scripts/globalize.js",
                        "~/Scripts/globalize/number.js"
            ));

        }
    }
}
