using System.Web.Optimization;

namespace tfs_dashboard
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new StyleBundle("~/bundles/bootstrap-styles").Include(
                        "~/stylesheets/styles.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-scripts").Include(
                        "~/javascripts/bootstrap*"));

            bundles.Add(new ScriptBundle("~/bundles/tfsApp").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-local-storage.js",
                "~/Scripts/angular-route.js",
                "~/Scripts/angular-modal-service.js",
                "~/App/app.js",
                "~/App/filters.js",
                "~/Scripts/common.js",
                "~/App/controllers/connectioncontroller.js",
                "~/App/controllers/configurationcontroller.js",
                "~/App/directives/ticket.js",
                "~/App/directives/navigation.js",
                "~/App/controllers/homecontroller.js",
                "~/App/services/dashboardservice.js",
                "~/App/services/tfsservice.js",
                "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                "~/App/controllers/settingscontroller.js"));
        }
    }
}