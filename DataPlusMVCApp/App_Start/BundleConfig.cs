using System.Web;
using System.Web.Optimization;

namespace DataPlusMVCApp
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
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/inputmask").Include(
            "~/Scripts/inputmask/jquery.inputmask.js"));

            bundles.Add(new ScriptBundle("~/bundles/blockui").Include(
            "~/Scripts/jquery.blockUI.js"));

            bundles.Add(new ScriptBundle("~/bundles/noty")
                .Include("~/Scripts/noty/jquery.noty.js")
                .Include("~/Scripts/noty/themes/default.js")
                .Include("~/Scripts/noty/themes/relax.js")
                .Include("~/Scripts/noty/themes/bootstrap.js")
                .Include("~/Scripts/noty/layouts/bottom.js")
                .Include("~/Scripts/noty/layouts/bottomCenter.js")
                .Include("~/Scripts/noty/layouts/bottomLeft.js")
                .Include("~/Scripts/noty/layouts/bottomRight.js")
                .Include("~/Scripts/noty/layouts/center.js")
                .Include("~/Scripts/noty/layouts/centerLeft.js")
                .Include("~/Scripts/noty/layouts/centerRight.js")
                .Include("~/Scripts/noty/layouts/inline.js")
                .Include("~/Scripts/noty/layouts/top.js")
                .Include("~/Scripts/noty/layouts/topCenter.js")
                .Include("~/Scripts/noty/layouts/topLeft.js")
                .Include("~/Scripts/noty/layouts/topRight.js"));
        }
    }
}
