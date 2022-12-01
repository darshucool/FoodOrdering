﻿using System.Web.Optimization;

namespace MIMS
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/qtip").Include(
                        "~/Scripts/custom/qtip/jquery.qtip.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/custom/homeslider.js",
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryUploader").Include("~/Scripts/jquery.fileupload.js").Include("~/Scripts/jquery.fileupload-ui.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryUploaderIframe").Include("~/Scripts/jquery.iframe-transport.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/Bootstrap").Include(
                        "~/Scripts/custom/bootstrap/bootstrap.js").Include(
                        "~/Scripts/custom/bootstrap/bootstrap-datepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/DatePicker").Include(
                        "~/Scripts/custom/bootstrap/bootstrap-datepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/TableSorter").Include(
                        "~/Scripts/custom/tablesorter/jquery.dataTables.js"));

            bundles.Add(new ScriptBundle("~/bundles/Util").Include(
                "~/Scripts/custom/Util.js").Include(
                        "~/Scripts/jquery.mCustomScrollbar.js").Include(
                    "~/Scripts/custom/numbervalidation.js"));

            bundles.Add(new ScriptBundle("~/bundles/scrollbar").Include(
                        "~/Scripts/jquery.mCustomScrollbar.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/Bootstrap").Include(
                       "~/Content/Bootstrap/bootstrap.css",
                       "~/Content/Bootstrap/bootstrap-responsive.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css").Include("~/Content/style.css").Include("~/Content/demo_table.css").Include("~/Content/datepicker.css").Include("~/Content/jquery.mCustomScrollbar.css"));

            bundles.Add(new StyleBundle("~/Content/qtip").Include("~/Content/qtip/jquery.qtip.css"));

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
        }
    }
}