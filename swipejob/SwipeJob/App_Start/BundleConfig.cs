using System.Web.Optimization;

namespace SwipeJob.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Content/js/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Content/js/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Content/js/bootstrap.js",
                "~/Content/js/respond.min.js",
                "~/Content/js/jquery.blockUI.js",
                "~/Content/js/moment.min.js",
                "~/Content/js/locales.min.js",
                "~/Content/js/bootstrap-datetimepicker.min.js",
                "~/Content/js/jstz.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Content/js/angular.js",
                "~/Content/js/ngDialog.min.js",
                "~/Content/js/ui-bootstrap-tpls-0.14.3.min.js",
                "~/Content/js/angular-google-plus.min.js",
                "~/Content/js/angular-facebook.min.js",
                "~/Content/js/ngprogress.min.js"
             ));

            bundles.Add(new ScriptBundle("~/bundles/swipeJobBase").Include(
                "~/Content/js/SwipeJob/app.js",
                "~/Content/js/SwipeJob/directives.js",
                "~/Content/js/SwipeJob/headerController.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/validate").Include(
                "~/Content/js/jquery.validate.js"
                ));

            bundles.Add(new StyleBundle("~/css").Include("~/Content/css/*.css", new CssRewriteUrlTransform()));
        }
    }
}
