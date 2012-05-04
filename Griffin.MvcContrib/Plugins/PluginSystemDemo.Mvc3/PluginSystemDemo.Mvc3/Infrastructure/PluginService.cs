using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Griffin.MvcContrib;
using Griffin.MvcContrib.Plugins;
using Griffin.MvcContrib.VirtualPathProvider;
using PluginSystemDemo.Mvc3.Infrastructure;
using PluginSystemDemo.PluginBase;

[assembly: PreApplicationStartMethod(typeof(PluginService), "PreScan")]

namespace PluginSystemDemo.Mvc3.Infrastructure
{
    public class PluginService
    {
        private static PluginFinder _finder;
        private readonly DiskFileLocator _diskFileLocator = new DiskFileLocator();

        private readonly EmbeddedViewFileProvider _embededProvider =
            new EmbeddedViewFileProvider(VirtualPathUtility.ToAbsolute("~/"), new ExternalViewFixer());

        private readonly PluginFileLocator _fileLocator = new PluginFileLocator();
        private readonly ViewFileProvider _diskFileProvider;

        public PluginService()
        {
            _diskFileProvider = new ViewFileProvider(_fileLocator, new ExternalViewFixer());

            if (VisualStudioHelper.IsInVisualStudio)
                GriffinVirtualPathProvider.Current.Add(_diskFileProvider);

            GriffinVirtualPathProvider.Current.Add(_embededProvider);
        }


        public static void PreScan()
        {
            _finder = new PluginFinder("~/bin/");
            _finder.Find();
        }

        public void Startup(ContainerBuilder builder)
        {
            foreach (var assembly in _finder.Assemblies)
            {
                // in this demo we'll assume that the project (and the root namespace is named after the area name).
                // adjust the second argument if you do not use that convention.
                _embededProvider.Add(new NamespaceMapping(assembly, Path.GetFileNameWithoutExtension(assembly.Location)));

                builder.RegisterControllers(assembly);

                // All plugins must be copied to the "plugin" sub folder.
                _diskFileLocator.Add("~/",
                                     Path.GetFullPath(HostingEnvironment.MapPath("~/") + @"\..\..\" +
                                                      Path.GetFileNameWithoutExtension(assembly.Location)));

                var moduleType = typeof(IModule);
                var modules = assembly.GetTypes().Where(moduleType.IsAssignableFrom);
                foreach (var module in modules)
                {
                    var mod = (IModule)Activator.CreateInstance(module);
                    builder.RegisterModule(mod);
                }
            }
        }

        public void Integrate(IContainer container)
        {
            foreach (var registrar in container.Resolve<IEnumerable<IMenuRegistrar>>())
            {
                registrar.Register(MainMenu.Current);
            }

            foreach (var registrar in container.Resolve<IEnumerable<IRouteRegistrar>>())
            {
                registrar.Register(RouteTable.Routes);
            }
        }
    }
}