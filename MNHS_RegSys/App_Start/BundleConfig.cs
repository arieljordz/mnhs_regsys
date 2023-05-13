using System.Web;
using System.Web.Optimization;

namespace MNHS_RegSys
{
    public class BundleConfig
    {
        //For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/AdminLTE/css").Include(
                      "~/Resources/AdminLTE-3.0.0-alpha.2/dist/css/adminlte.min.css",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/dist/css/adminlte.css",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/dist/css/ionicons.css",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/plugins/font-awesome/css/font-awesome.min.css",
                       "~/Resources/AdminLTE-3.0.0-alpha.2/plugins/select2/select2.css",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/plugins/datatables/jquery.dataTables.min.css",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/plugins/iCheck/flat/blue.css",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/plugins/morris/morris.css",
                      //"~/Resources/AdminLTE-3.0.0-alpha.2/plugins/jvectormap/jquery-jvectormap-1.2.2.css",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/plugins/datepicker/datepicker3.css",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/dist/css/fonts.css",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/plugins/daterangepicker/daterangepicker-bs3.css",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css",
                      "~/Content/Content/Site.css",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/scrollbar/scrollbar.css"
                      // "~/Resources/parsley/parsley.css"
                      ));

            bundles.Add(new StyleBundle("~/AdminLTE/js").Include(
                      "~/Resources/AdminLTE-3.0.0-alpha.2/plugins/jquery/jquery.js",
                      //"~/Resources/AdminLTE-3.0.0-alpha.2/plugins/jquery/jquery.min.js",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/plugins/bootstrap/js/bootstrap.bundle.min.js",
                       "~/Resources/AdminLTE-3.0.0-alpha.2/plugins/bootstrap/js/bootstrap.min.js",
                       "~/Resources/AdminLTE-3.0.0-alpha.2/plugins/select2/select2.full.js",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/plugins/morris/morris.min.js",
                      // "~/Resources/SmartWizard-master/dist/js/jquery.smartWizard.min.js",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/plugins/datatables/jquery.dataTables.min.js",
                      //"~/Resources/AdminLTE-3.0.0-alpha.2/plugins/datatables/dataTables.bootstrap4.js",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/dist/js/codejquery.js",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/plugins/sparkline/jquery.sparkline.min.js",
                      //"~/Resources/AdminLTE-3.0.0-alpha.2/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                      //"~/Resources/AdminLTE-3.0.0-alpha.2/plugins/jvectormap/jquery-jvectormap-world-mill-en.js",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/plugins/knob/jquery.knob.js",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/dist/js/moment.js",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/dist/js/raphael.js",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/plugins/daterangepicker/daterangepicker.js",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/plugins/datepicker/bootstrap-datepicker.js",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/plugins/slimScroll/jquery.slimscroll.min.js",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/plugins/fastclick/fastclick.js",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/dist/js/adminlte.js",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/dist/js/demo.js"
                      //"~/Resources/parsley/parsley.js",
                      //"~/Content/plugins/font-awesome/css/font-awesome.css",
                      //"~/Content/plugins/font-awesome/css/font-awesome.min.css"
                  ));

            bundles.Add(new StyleBundle("~/dataTable/css").Include(
                      "~/Resources/AdminLTE-3.0.0-alpha.2/plugins/datatables/jquery.dataTables_themeroller.css",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/plugins/datatables/dataTables.bootstrap4.css",
                      "~/Resources/AdminLTE-3.0.0-alpha.2/plugins/datatables/jquery.dataTables.css"
                      ));
            bundles.Add(new StyleBundle("~/dataTable/js").Include(

                      ));


            //DATATABLE JS
            bundles.Add(new ScriptBundle("~/bundles/datatable-js").Include
            (
            "~/Content/Plugins/dataTables/js/jquery.dataTables.js",
            "~/Content/Plugins/dataTables/js/dataTables.bootstrap.js",
            "~/Content/plugins/dataTables/js/dataTables.responsive.js"
            ));

            //DATATABLE CSS
            bundles.Add(new StyleBundle("~/bundles/datatable-css").Include
            (
            "~/Content/Plugins/dataTables/css/dataTables.bootstrap.css",
            "~/Content/plugins/dataTables/css/dataTables.responsive.css"
            ));

            //DATEPICKER JS
            bundles.Add(new ScriptBundle("~/bundles/datepicker-js").Include
            (
            "~/Content/Plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"
            ));

            //DATEPICKER CSS
            bundles.Add(new StyleBundle("~/bundles/datepicker-css").Include
            (
            "~/Content/Plugins/bootstrap-datepicker/css/datepicker3.css"
            ));


            //CHART JS
            bundles.Add(new ScriptBundle("~/bundles/chart-js").Include
            (
            "~/Content/Plugins/chart/Chart.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/global-js").Include
             (
                      "~/Scripts/JSGlobal.js"));

            bundles.Add(new StyleBundle("~/bundles/site-css").Include
             (
                      "~/Content/Site.css"));
        }
    }
}