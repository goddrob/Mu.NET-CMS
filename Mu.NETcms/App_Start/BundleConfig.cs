using BundleTransformer.Core.Builders;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Resolvers;
using BundleTransformer.Core.Transformers;
using System.Web;
using System.Web.Optimization;

namespace Mu.NETcms
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        static NullBuilder nullBuilder;
        static StyleTransformer styleTransformer;
        static ScriptTransformer scriptTransformer;
        static NullOrderer nullOrderer;
        private static Bundle createBundle(string name, string[] paths, string type)
        {
            var bundle = new Bundle(name);
            foreach (string s in paths)
            {
                bundle.Include(s);
            }
            bundle.Builder = nullBuilder;
            if (type.Equals("style")) bundle.Transforms.Add(styleTransformer);
            else if (type.Equals("script")) bundle.Transforms.Add(scriptTransformer);
            bundle.Orderer = nullOrderer;
            return bundle;
        }
        public static void RegisterBundles(BundleCollection bundles)
        {

            // Bundle Transformer
            nullBuilder = new NullBuilder();
            styleTransformer = new StyleTransformer();
            scriptTransformer = new ScriptTransformer();
            nullOrderer = new NullOrderer();

            // Replace a default bundle resolver in order to the debugging HTTP-handler
            // can use transformations of the corresponding bundle
            BundleResolver.Current = new CustomBundleResolver();
            //
            bundles.Add(createBundle("~/bundles/jquery", new string[] { "~/Scripts/jquery-{version}.js" }, "script"));
            bundles.Add(createBundle("~/bundles/jqueryval", new string[] { "~/Scripts/jquery.validate*" }, "script"));
            bundles.Add(createBundle("~/bundles/modernizr", new string[] { "~/Scripts/modernizr-*" }, "script"));
            bundles.Add(createBundle("~/bundles/bootstrap", new string[] {"~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js" }, "script"));
            bundles.Add(createBundle("~/Content/css", new string[] {"~/Content/bootstrap.css",
                      "~/Content/site.css" }, "style"));
            bundles.Add(createBundle("~/themes/main", new string[] {"~/Content/css/main.css",
                "~/Content/css/devices.css",
                "~/Content/css/post.css",
                "~/Content/css/validationEngine.jquery.css",
                "~/Content/css/jquery.fancybox.css" }, "style"));
            bundles.Add(createBundle("~/css/slider", new string[] { "~/Content/css/paralax_slider.css" }, "style"));
            bundles.Add(createBundle("~/bundles/slider", new string[] { "~/Content/javascript/jquery.cslider.js" }, "script"));
            bundles.Add(createBundle("~/css/data", new string[] { "~/Content/css/Data.css" }, "style"));
            bundles.Add(createBundle("~/css/reset", new string[] { "~/Content/css/reset.css" }, "style"));
            bundles.Add(createBundle("~/css/login", new string[] {"~/Content/css/loginforms.css",
                "~/Content/css/buttons-si.css" }, "style"));
            bundles.Add(createBundle("~/css/sidebar", new string[] { "~/Content/css/sidebar.css" }, "style"));
            bundles.Add(createBundle("~/bundles/fancybox", new string[] { "~/Content/javascript/jquery.fancybox.js?v=2.1.3" }, "script"));
            //bundles.Add(createBundle("~/css/data", new string[] { }, "script"));



            
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));
            //bundles.Add(new StyleBundle("~/themes/main").Include(
            //    "~/Content/css/main.css",
            //    "~/Content/css/devices.css",
            //    "~/Content/css/post.css",
            //    "~/Content/css/validationEngine.jquery.css",
            //    "~/Content/css/jquery.fancybox.css"));

            //bundles.Add(new StyleBundle("~/css/slider").Include(
            //    "~/Content/css/paralax_slider.css"));

            //bundles.Add(new ScriptBundle("~/bundles/slider").Include(
            //    "~/Content/javascript/jquery.cslider.js"));

            //bundles.Add(new ScriptBundle("~/bundles/fancybox").Include(
            //    "~/Content/javascript/jquery.fancybox.js?v=2.1.3"));

            //bundles.Add(new StyleBundle("~/css/data").Include(
            //    "~/Content/css/Data.css"));

            //bundles.Add(new StyleBundle("~/css/reset").Include(
            //    "~/Content/css/reset.css"));

            //bundles.Add(new StyleBundle("~/css/login").Include(
            //    "~/Content/css/loginforms.css",
            //    "~/Content/css/buttons-si.css"));

            //bundles.Add(new StyleBundle("~/css/sidebar").Include(
            //    "~/Content/css/sidebar.css"));
        }
    }
}
