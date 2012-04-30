using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Griffin.MvcContrib.VirtualPathProvider;

namespace PluginSystemDemo.Mvc3
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            RegisterViews();
        }

        protected void RegisterViews()
        {
            var embeddedProvider = new EmbeddedViewFileProvider(new ExternalViewFixer());
            embeddedProvider.Add(new NamespaceMapping(typeof(Lib.Areas.Some.Controllers.MyController).Assembly, "BasicPlugins.Lib"));
            //GriffinVirtualPathProvider.Current.Add(embeddedProvider);

            var diskLocator = new DiskFileLocator();
            diskLocator.Add("~/", Path.GetFullPath(Server.MapPath("~/") + @"..\BasicPlugins.Lib\"));
            var viewProvider = new ViewFileProvider(diskLocator, new ExternalViewFixer());
            GriffinVirtualPathProvider.Current.Add(viewProvider);

            HostingEnvironment.RegisterVirtualPathProvider(GriffinVirtualPathProvider.Current);
        }
    }
}