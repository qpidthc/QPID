using System.Web;
using System.Web.Optimization;

namespace WebHTCBackEnd
{
    public class BundleConfig
    {
        // 如需「搭配」的詳細資訊，請瀏覽 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {            
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好實際執行時，請使用 http://modernizr.com 上的建置工具，只選擇您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/jquery.nice-select.min.js",
                      "~/Scripts/bootstrap-select.min.js",
                      "~/Scripts/bootstrap-datepicker.min.js",
                      "~/Scripts/bootstrap-datepicker.zh-TW.min.js",
                      "~/Scripts/jquery.dataTables.min.js",
                      "~/Scripts/dataTables.jqueryui.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(                     
                      "~/Content/site.css", 
                      "~/Content/bootstrap-select.min.css",
                      "~/Content/nice-select.css",
                      "~/Content/bootstrap-datepicker.min.css",
                      "~/Content/dataTables.jqueryui.min.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/sidebar.css",
                      "~/Content/datatable.css"
                      ));

            //bundles.Add(new ScriptBundle("~/Scripts/js").Include(
            //          "~/Scripts/jquery.nice-select.min.js",
            //          "~/Scripts/bootstrap-select.min.js",
            //          "~/Scripts/bootstrap-datepicker.min.js",
            //          "~/Scripts/bootstrap-datepicker.zh-TW.min.js",
            //          "~/Scripts/jquery.dataTables.min.js",
            //          "~/Scripts/dataTables.jqueryui.min.js"));

    //        <script src="./Scripts/jquery.nice-select.min.js"></script>
    //<script src="./Scripts/bootstrap-select.min.js"></script>   
    //<script src="./Scripts/bootstrap-datepicker.min.js"></script>
    //<script src="./Scripts/bootstrap-datepicker.zh-TW.min.js" charset="UTF-8"></script>   
    //<script src="./Scripts/jquery.dataTables.min.js"></script>
    //<script src="./Scripts/dataTables.jqueryui.min.js"></script>
        }
    }
}
