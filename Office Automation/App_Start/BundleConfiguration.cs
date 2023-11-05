using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace CMSArticle.App_Start
{
    public class BundleConfiguration
    {
        public static void RegisterBundle(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/Css").Include(
                    "~/Content/bootstrap.min.css",
                    "~/Content/bootstrap-rtl.min.css",
                    "~/Content/Site.css"
                ));


            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
                    "~/Scripts/modernizr-2.6.2.js"
               ));
        }
    }
}