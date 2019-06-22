using BundleTransformer.Core.Bundles;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Transformers;
using System.Web.Optimization;

namespace MMS.SECURITY
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;
            var cssTransformer = new StyleTransformer();
            var jsTransformer = new ScriptTransformer();
            var nullOrderer = new NullOrderer();

            var cssBundle = new StyleBundle("~/bundles/css");
            cssBundle.Include("~/assets/less/styles.less", "~/assets/less/Site.less");
            cssBundle.Transforms.Add(cssTransformer);
            cssBundle.Orderer = nullOrderer;
            bundles.Add(cssBundle);

            var cssDatepickerBundle = new StyleBundle("~/content/datepicker");
            cssDatepickerBundle.Include("~/Content/bootstrap-datepicker.css");
            cssDatepickerBundle.Transforms.Add(cssTransformer);
            cssDatepickerBundle.Orderer = nullOrderer;
            bundles.Add(cssDatepickerBundle);

            var jsDatepickerBundle = new StyleBundle("~/bundles/datepicker");
            jsDatepickerBundle.Include("~/Scripts/bootstrap-datepicker.js");
            jsDatepickerBundle.Transforms.Add(jsTransformer);
            jsDatepickerBundle.Orderer = nullOrderer;
            bundles.Add(jsDatepickerBundle);


            var jqueryBundle = new ScriptBundle("~/bundles/jquery");
            jqueryBundle.Include("~/Scripts/jquery-{version}.js", "~/Scripts_old/moment.js", "~/Scripts_old/custom.js");
            jqueryBundle.Transforms.Add(jsTransformer);
            jqueryBundle.Orderer = nullOrderer;
            bundles.Add(jqueryBundle);


            var jqueryvalBundle = new ScriptBundle("~/bundles/jqueryval");
            jqueryvalBundle.Include("~/assets/js/jquery.validate*");
            jqueryvalBundle.Transforms.Add(jsTransformer);
            jqueryvalBundle.Orderer = nullOrderer;
            bundles.Add(jqueryvalBundle);


            var bootstrapBundleTh = new CustomScriptBundle("~/bundles/bootstrapth");
            bootstrapBundleTh.Include("~/Scripts_old/typeahead.bundle.js");
            bootstrapBundleTh.Transforms.Add(jsTransformer);
            bootstrapBundleTh.Orderer = nullOrderer;
            bundles.Add(bootstrapBundleTh);




            var jqueryMask = new CustomScriptBundle("~/bundles/Mask");
            jqueryMask.Include("~/assets/demo/demo-mask.js", "~/assets/plugins/form-inputmask/jquery.inputmask.bundle.js");
            jqueryMask.Transforms.Add(jsTransformer);
            jqueryMask.Orderer = nullOrderer;
            bundles.Add(jqueryMask);

            //var jqueryvalBundle = new ScriptBundle("~/bundles/jqueryval");
            //jqueryvalBundle.Include("~/Scripts_old/jquery.validate*");
            //jqueryvalBundle.Transforms.Add(jsTransformer);
            //jqueryvalBundle.Orderer = nullOrderer;
            //bundles.Add(jqueryvalBundle);

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.

            var modernizrBundle = new ScriptBundle("~/bundles/modernizr");
            modernizrBundle.Include("~/assets/js/modernizr-*");
            modernizrBundle.Transforms.Add(jsTransformer);
            modernizrBundle.Orderer = nullOrderer;
            bundles.Add(modernizrBundle);

            var bootstrapBundle = new ScriptBundle("~/bundles/bootstrap");
            //bootstrapBundle.Include("~/assets/js/bootstrap.min.js", "~/assets/js/respond.js");
            bootstrapBundle.Include("~/Scripts/bootstrap.js"
                , "~/assets/js/respond.js"
                , "~/assets/js/enquire.js"
                , "~/assets/js/jquery.cookie.js"
                , "~/assets/js/jquery.nicescroll.min.js"
                , "~/assets/js/application.js"
                , "~/assets/plugins/sparklines/jquery.sparklines.min.js"
                , "~/assets/plugins/form-toggle/toggle.min.js"
                , "~/assets/plugins/codeprettifier/prettify.js"
                , "~/assets/demo/demo.js"
                );
            bootstrapBundle.Transforms.Add(jsTransformer);
            bootstrapBundle.Orderer = nullOrderer;
            bundles.Add(bootstrapBundle);

            //Added for toaster
            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                       "~/Scripts/toastr.js*"));
            //Modify for toastr
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css",
                                                                "~/Content/toastr.css"));
        }
    }
}