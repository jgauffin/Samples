using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Hosting;
using System.Web.Routing;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Griffin.MvcContrib.Logging;
using Griffin.MvcContrib.Plugins;
using Griffin.MvcContrib.VirtualPathProvider;
using PluginSystemDemo.Mvc3.Infrastructure;
using PluginSystemDemo.PluginBase;

[assembly: PreApplicationStartMethod(typeof (PluginService), "PreScan")]

namespace PluginSystemDemo.Mvc3.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>Credits: http://shazwazza.com/post/Developing-a-plugin-framework-in-ASPNET-with-medium-trust.aspx</remarks>
    public class PluginLoader2
    {
        private readonly List<Assembly> _assemblies = new List<Assembly>();
        private readonly ILogger _logger = LogProvider.Current.GetLogger<PluginLoader>();
        private readonly DirectoryInfo _pluginFolder;

        /// <summary>
        ///   Initializes the <see cref="PluginLoader" /> class.
        /// </summary>
        /// <param name="virtualPluginFolderPath"> App relative path to plugin folder </param>
        /// <example>
        ///   <code>var loader = new PluginLoader("~/"); // all plugins are located in the root folder.</code>
        /// </example>
        public PluginLoader2(string virtualPluginFolderPath)
        {
            if (virtualPluginFolderPath == null) throw new ArgumentNullException("virtualPluginFolderPath");
            var path = virtualPluginFolderPath.StartsWith("~")
                           ? HostingEnvironment.MapPath(virtualPluginFolderPath)
                           : virtualPluginFolderPath;
            if (path == null)
                throw new InvalidOperationException(string.Format("Failed to map path '{0}'.", virtualPluginFolderPath));

            _pluginFolder = new DirectoryInfo(path);
        }

        /// <summary>
        ///   Get all plugin assemblies.
        /// </summary>
        public IEnumerable<Assembly> Assemblies
        {
            get { return _assemblies; }
        }

        /// <summary>
        ///   Called during startup to scan for all plugin assemblies
        /// </summary>
        public void Startup()
        {
            CopyPluginDlls(_pluginFolder, AppDomain.CurrentDomain.DynamicDirectory);
        }

        private void CopyPluginDlls(DirectoryInfo sourceFolder, string destinationFolder)
        {
            foreach (var file in Directory.GetFiles(sourceFolder.FullName, "Plugin.*.dll"))
            {
                LoadPluginAssembly(file);
                
            }
            return;
            foreach (var plug in sourceFolder.GetFiles("*.dll", SearchOption.AllDirectories))
            {
                if (!plug.Name.StartsWith("Plugin."))
                    continue;
                //var dest = Path.Combine(destinationFolder, plug.Name);
                //if (!File.Exists(Path.Combine(destinationFolder, plug.Name)))
                //{
                //File.Copy(plug.FullName, dest, true);
                //}
                LoadPluginAssembly(plug.FullName);
            }
        }

        private void LoadPluginAssembly(string fullPath)
        {
            if (fullPath == null) throw new ArgumentNullException("fullPath");

            try
            {
                var assembly = Assembly.LoadFrom(fullPath);
                BuildManager.AddReferencedAssembly(assembly);
                _assemblies.Add(assembly);
            }
            catch (Exception err)
            {
                _logger.Warning("Failed to load " + fullPath + ".", err);

                var loaderEx = err as ReflectionTypeLoadException;
                if (loaderEx != null)
                {
                    foreach (var exception in loaderEx.LoaderExceptions)
                    {
                        _logger.Warning(string.Format("Loader exception for file '{0}'.", fullPath), exception);
                    }
                }

                throw;
            }
        }
    }

    public class PluginService
    {
        private static PluginLoader2 _pluginLoader;
        private readonly DiskFileLocator _diskFileLocator = new DiskFileLocator();

        private readonly EmbeddedViewFileProvider _embededProvider =
            new EmbeddedViewFileProvider(new ExternalViewFixer());

        private readonly PluginFileLocator _fileLocator = new PluginFileLocator();
        private readonly ViewFileProvider _viewFileProvider;

        public PluginService()
        {
            _viewFileProvider = new ViewFileProvider(_fileLocator);
            GriffinVirtualPathProvider.Current.Add(_viewFileProvider);
            GriffinVirtualPathProvider.Current.Add(_embededProvider);
        }


        public static void PreScan()
        {
            _pluginLoader = VisualStudioHelper.IsInVisualStudio
                                ? new PluginLoader2("~/bin/")
                                : new PluginLoader2("~/");
            _pluginLoader.Startup();
        }

        public void Startup(ContainerBuilder builder)
        {
            foreach (var plugin in _pluginLoader.Assemblies)
            {
                // in this demo we'll assume that the project (and the root namespace is named after the area name).
                // adjust the second argument if you do not use that convention.
                _embededProvider.Add(new NamespaceMapping(plugin, Path.GetFileNameWithoutExtension(plugin.Location)));

                builder.RegisterControllers(plugin);

                // All plugins must be copied to the "plugin" sub folder.
                _diskFileLocator.Add("~/",
                                     Path.GetFullPath(HostingEnvironment.MapPath("~/") + @"\..\..\" +
                                                      Path.GetFileNameWithoutExtension(plugin.Location)));

                var moduleType = typeof (IModule);
                var modules = plugin.GetTypes().Where(moduleType.IsAssignableFrom);
                foreach (var module in modules)
                {
                    var mod = (IModule) Activator.CreateInstance(module);
                    builder.RegisterModule(mod);
                }
            }
            //GriffinVirtualPathProvider.Current.Add(embeddedProvider);
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

    public class PluginFileLocator : IViewFileLocator
    {
        #region IViewFileLocator Members

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

        #endregion
    }
}