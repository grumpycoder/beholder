using System.Web.Optimization;

namespace splc.beholder.web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                                         "~/Scripts/jquery-{version}.js"));

            //JQuery Validate
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                                         "~/Scripts/jquery.unobtrusive*",
                                         "~/Scripts/jquery.validate*"));
            //JQueryUI
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                                         "~/Scripts/jquery-ui-{version}.js"));
            //Foolproof clientside validation
            bundles.Add(new ScriptBundle("~/bundles/foolproof").Include(
                                         "~/Scripts/mvcfoolproof.unobtrusive.min.js"));
            //Bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                                         "~/Scripts/bootstrap.min.js"));

            //Bootstrap-select
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-select").Include(
                                         "~/Scripts/bootstrap-select.min.js"));

            //Chosen
            bundles.Add(new ScriptBundle("~/bundles/chosen").Include(
                                         "~/Scripts/chosen.jquery.min.js"));

            //Bootstrap Datepicker
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-datepicker").Include(
                                         "~/Scripts/bootstrap-datepicker.js"));


            //Toastr
            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                                         "~/Scripts/toastr.js"));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                                         "~/Scripts/modernizr-*"));
            //Movingboxes 
            bundles.Add(new ScriptBundle("~/bundles/movingboxes").Include(
                                         "~/Scripts/jquery.movingboxes*"));
            //Filedrop
            bundles.Add(new ScriptBundle("~/bundles/jqueryfiledrop").Include(
                                         "~/Scripts/jquery.filedrop*"));
            //Application Script
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                                         "~/Scripts/app-{version}.js"));

            //Bootstrap Style
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                                        "~/Content/bootstrap.min.css",
                                        "~/Content/bootstrap-responsive.min.css"));

            //Bootstrap Theme Style
            bundles.Add(new StyleBundle("~/Content/bootstrap-theme").Include(
                                        "~/Content/bootstrap-yeti.min.css"));


            //Bootstrap-select Style
            bundles.Add(new StyleBundle("~/Content/bootstrap-select").Include(
                                        "~/Content/bootstrap-select.min.css"));

            //JQueryUI Style
            bundles.Add(new StyleBundle("~/Content/jqueryui").Include(
                                        "~/Content/themes/base/jquery.ui.core.css",
                                        "~/Content/themes/base/jquery.ui.dialog.css",
                                        "~/Content/themes/base/jquery.ui.theme.css",
                                        "~/Content/themes/base/jquery.ui.base.css"));
            //fontawesome
            bundles.Add(new StyleBundle("~/Content/fontawesome").Include(
                                        "~/Content/font-awesome.css"));
            //Toastr Style
            bundles.Add(new StyleBundle("~/Content/toastr").Include(
                                        "~/Content/toastr.css"));
            //Site Style
            bundles.Add(new StyleBundle("~/Content/css").Include(
                                        "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/movingboxes").Include(
                                        "~/Content/movingboxes*"));

            bundles.Add(new StyleBundle("~/Content/carousel").Include(
                                        "~/Content/carousel.css"));

            //Bootstrap-select Datepicker
            bundles.Add(new StyleBundle("~/Content/bootstrap-datepicker").Include(
                                        "~/Content/bootstrap-datepicker3.css"));
            //Bootstrap-Chosen
            bundles.Add(new StyleBundle("~/Content/bootstrap-chosen").Include(
                                        "~/Content/bootstrap-chosen.css"));
            
        }
    }
}