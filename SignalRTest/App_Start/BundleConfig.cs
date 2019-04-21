using System.Web;
using System.Web.Optimization;

namespace SignalRTest
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备就绪，请使用 https://modernizr.com 上的生成工具仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/style.css"));

            bundles.Add(new ScriptBundle("~/bundles/SignalR").Include(
                "~/Scripts/jquery.signalR-2.3.0.js"
                ));




            /*
            <script type="text/javascript" src="echarts.js"></script>
            <script type="text/javascript" src="echarts-gl.js"></script>
            <script type="text/javascript" src="ecStat.min.js"></script>
            <script type="text/javascript" src="dataTool.min.js"></script>
            <script type="text/javascript" src="china.js"></script>
            <script type="text/javascript" src="world.js"></script>
            <script type="text/javascript" src="bmap.min.js"></script>
            <script type="text/javascript" src="simplex.js"></script>*/
            bundles.Add(new ScriptBundle("~/signalr/hubs").Include(
                "~/Scripts/Signal/echarts.js",
                "~/Scripts/Signal/echarts-gl.js",
                "~/Scripts/Signal/ecStat.min.js",
                "~/Scripts/Signal/dataTool.min.js",
                "~/Scripts/Signal/china.js",
                "~/Scripts/Signal/world.js",
                "~/Scripts/Signal/bmap.min.js",
                "~/Scripts/Signal/simplex.js"
                ));
        }
    }
}
