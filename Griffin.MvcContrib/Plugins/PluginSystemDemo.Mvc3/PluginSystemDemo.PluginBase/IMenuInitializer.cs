using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using Autofac;
using Griffin.MvcContrib.Plugins;

namespace PluginSystemDemo.PluginBase
{
    /// <summary>
    /// Initializeed a plgin
    /// </summary>
    public interface IMenuInitializer
    {
        /// <summary>
        /// Expose menu to let all plugins register their items
        /// </summary>
        IMenuWithChildren MainMenu { get; }
    }

    public class PluginHandler : IMenuInitializer
    {
        private readonly RouteTable _routeTable;
        RoutedMenuItem _mainMenu = new RoutedMenuItem("mainMenu", "Min menu", new{});

        public PluginHandler(RouteTable routeTable, IContainer container)
        {
            _routeTable = routeTable;
        }

        /// <summary>
        /// Expose menu to let all plugins register their items
        /// </summary>
        public IMenuWithChildren MainMenu
        {
            get { return _mainMenu; }
        }

        /// <summary>
        /// Register entries
        /// </summary>
        /// <param name="routes">Route to register</param>
        public void Register(RouteTable routes)
        {
            
        }
    }

    
}
