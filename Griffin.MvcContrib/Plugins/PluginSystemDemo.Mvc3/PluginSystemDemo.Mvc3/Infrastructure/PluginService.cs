using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Autofac;
using Griffin.MvcContrib.Plugins;
using Griffin.MvcContrib.VirtualPathProvider;
using PluginSystemDemo.Mvc3.Infrastructure;

[assembly:PreApplicationStartMethod(typeof(PluginService), "PreScan")]

namespace PluginSystemDemo.Mvc3.Infrastructure
{
    public class PluginService
    {
        private readonly IContainer _container;
        private EmbeddedViewFileProvider _embededProvider = new EmbeddedViewFileProvider(new ExternalViewFixer());
        PluginFileLocator _fileLocator = new PluginFileLocator();
        private ViewFileProvider _viewFileProvider;
        DiskFileLocator _diskFileLocator = new DiskFileLocator();

        public PluginService(IContainer container)
        {
            _container = container;
            _viewFileProvider = new ViewFileProvider(_fileLocator);
            GriffinVirtualPathProvider.Current.Add(_viewFileProvider);
            GriffinVirtualPathProvider.Current.Add(_embededProvider);
        }


        private static readonly PluginLoader _pluginLoader = new PluginLoader("~/Plugin");

        public static void PreScan()
        {
            _pluginLoader.Startup();
        }

        public void Startup()
        {
            foreach (var plugin in _pluginLoader.Assemblies)
            {
                // in this demo we'll assume that the project (and the root namespace is named after the area name).
                // adjust the second argument if you do not use that convention.
                _embededProvider.Add(new NamespaceMapping(plugin, Path.GetFileNameWithoutExtension(plugin.Location)));
                
                
                // All plugins must be copied to the "plugin" sub folder.
                _diskFileLocator.Add("~/", Path.GetFullPath(HostingEnvironment.MapPath("~/Plugins") + @"..\BasicPlugins.Lib\"));
            }
            //GriffinVirtualPathProvider.Current.Add(embeddedProvider);

            
        }
    }

    public class PluginFileLocator : IViewFileLocator
    {

        /// <summary>
        /// Get full path to a file
        /// </summary>
        /// <param name="uri">Requested uri</param>
        /// <returns>
        /// Full disk path if found; otherwise null.
        /// </returns>
        public string GetFullPath(string uri)
        {
            throw new NotImplementedException();
        }
    }
}