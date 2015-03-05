using System.Web;
using System.Web.Optimization;

namespace Mu.NETcms
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
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/themes/red").Include(
                "~/Content/Themes/red/stylesheets/main.css",
                "~/Content/Themes/red/stylesheets/devices.css",
                "~/Content/Themes/red/stylesheets/paralax_slider.css",
                "~/Content/Themes/red/stylesheets/post.css",
                "~/Content/Themes/red/stylesheets/jquery.fancybox.css?v=2.1.2"));

            bundles.Add(new ScriptBundle("~/bundles/slider").Include(
                "~/Content/Themes/red/javascript/jquery.cslider.js"));

            bundles.Add(new ScriptBundle("~/bundles/fancybox").Include(
                "~/Content/Themes/red/javascript/jquery.fancybox.js?v=2.1.3"));
        }
    }
}
