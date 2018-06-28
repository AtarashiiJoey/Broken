using System.Web;
using System.Web.Optimization;

namespace Colmart
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

            // ********** CMS SCRIPTS AND STYLESHEETS ********** //

            bundles.Add(new ScriptBundle("~/bundles/cms/jquery").Include(
                       // -- Vendor -- //
                       "~/Areas/CMS/Content/assets/vendor/jquery/jquery.js",
                       "~/Areas/CMS/Content/assets/vendor/jquery-browser-mobile/jquery.browser.mobile.js",
                       "~/Areas/CMS/Content/assets/vendor/jquery-placeholder/jquery-placeholder.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.

            bundles.Add(new ScriptBundle("~/bundles/cms/modernizr").Include(
                        // -- Head Libs -- //
                        "~/Areas/CMS/Content/assets/vendor/modernizr/modernizr.js"));

            bundles.Add(new ScriptBundle("~/bundles/cms/bootstrap").Include(
                      // -- Vendor -- //
                      "~/Areas/CMS/Content/assets/vendor/bootstrap/js/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/cms/layout").Include(
                      // -- Vendor -- //
                      "~/Areas/CMS/Content/assets/vendor/nanoscroller/nanoscroller.js",
                      "~/Areas/CMS/Content/assets/vendor/bootstrap-datepicker/js/bootstrap-datepicker.js",
                      "~/Areas/CMS/Content/assets/vendor/magnific-popup/jquery.magnific-popup.js",
                      "~/Areas/CMS/Content/assets/vendor/bootstrap-fileupload/bootstrap-fileupload.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/cms/theme").Include(
                      // -- Tables -- //
                      "~/Areas/CMS/Content/assets/vendor/select2/js/select2.js",
                      "~/Areas/CMS/Content/assets/vendor/jquery-datatables/media/js/jquery.dataTables.js",
                      "~/Areas/CMS/Content/assets/vendor/jquery-datatables/extras/TableTools/js/dataTables.tableTools.min.js",
                      "~/Areas/CMS/Content/assets/vendor/jquery-datatables-bs3/assets/js/datatables.js",
                      "~/Areas/CMS/Content/assets/javascripts/tables/examples.datatables.editable.js",
                      "~/Areas/CMS/Content/assets/javascripts/tables/examples.datatables.default.js",
                      "~/Areas/CMS/Content/assets/javascripts/tables/examples.datatables.row.with.details.js",
                      "~/Areas/CMS/Content/assets/javascripts/tables/examples.datatables.tabletools.js",

                      // -- Notifications -- //
                      "~/Areas/CMS/Content/assets/javascripts/ui-elements/examples.notifications.js",

                      // -- UI-Elements modals -- //
                      "~/Areas/CMS/Content/assets/vendor/pnotify/pnotify.custom.js",
                      "~/Areas/CMS/Content/assets/javascripts/ui-elements/examples.modals.js",

                      // -- Theme -- //
                      // -- Theme Base, Components and Settings -- //
                      "~/Areas/CMS/Content/assets/javascripts/theme.js",

                      // -- Theme Custom -- //
                      "~/Areas/CMS/Content/assets/javascripts/theme.custom.js",

                      // -- Theme Initialization Files -- //
                      "~/Areas/CMS/Content/assets/javascripts/theme.init.js"));

            bundles.Add(new ScriptBundle("~/bundles/cms/example").Include(
                      // -- Theme (Example) -- //
                      "~/Areas/CMS/Content/assets/javascripts/layouts/examples.header.menu.js",
                      "~/Areas/CMS/Content/assets/javascripts/dashboard/examples.dashboard.js"));

            bundles.Add(new StyleBundle("~/Content/cms/css").Include(

                      // -- Vendor CSS -- //
                      "~/Areas/CMS/Content/assets/vendor/bootstrap/css/bootstrap.css",
                      "~/Areas/CMS/Content/assets/vendor/font-awesome/css/font-awesome.css",
                      "~/Areas/CMS/Content/assets/vendor/magnific-popup/magnific-popup.css",
                      "~/Areas/CMS/Content/assets/vendor/bootstrap-datepicker/css/bootstrap-datepicker3.css",

                      // -- Specific Page Vendor CSS -- //
                      "~/Areas/CMS/Content/assets/vendor/jquery-ui/jquery-ui.css",
                      "~/Areas/CMS/Content/assets/vendor/jquery-ui/jquery-ui.theme.css",
                      "~/Areas/CMS/Content/assets/vendor/bootstrap-multiselect/bootstrap-multiselect.css",
                      "~/Areas/CMS/Content/assets/vendor/morris.js/morris.css",
                      "~/Areas/CMS/Content/assets/vendor/bootstrap-fileupload/bootstrap-fileupload.min.css",

                      // -- UI-Elements modals -- //
                      "~/Areas/CMS/Content/assets/vendor/pnotify/pnotify.custom.css",

                      // -- CMS Theme CSS -- //
                      "~/Areas/CMS/Content/assets/stylesheets/themeCMS.css",

                      // -- Skin CSS -- //
                      "~/Areas/CMS/Content/assets/stylesheets/skins/default.css",

                      // -- Theme Custom CSS -- //
                      "~/Areas/CMS/Content/assets/stylesheets/theme-custom.css"));

            // -- Slim - Crop -- //
            bundles.Add(new StyleBundle("~/Content/cms/slimCropStylesheets").Include(
                       "~/Areas/CMS/Content/assets/vendor/slim_crop/slim.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/cms/slimCropScripts").Include(
                      "~/Areas/CMS/Content/assets/vendor/slim_crop/slim.kickstart.min.js"));

            // -- Tables -- //
            bundles.Add(new StyleBundle("~/Content/cms/tableStylesheets").Include(
                       "~/Areas/CMS/Content/assets/vendor/select2/css/select2.css",
                       "~/Areas/CMS/Content/assets/vendor/select2-bootstrap-theme/select2-bootstrap.min.css",
                       "~/Areas/CMS/Content/assets/vendor/jquery-datatables-bs3/assets/css/datatables.css"));

            // -- Login CSS -- //
            bundles.Add(new StyleBundle("~/Content/loginStylesheets").Include(
                       "~/Areas/CMS/Content/assets/stylesheets/LoginStylesheet.css"));
        }
    }
}
