using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Griffin.MvcContrib.Plugins;
using PluginSystemDemo.PluginBase;

namespace Messaging
{
    /// <summary>
    /// Use Autofacs IStartable to force the registration
    /// </summary>
    public class MenuRegistrar : IStartable
    {
        private readonly IMenuInitializer _menuInitializer;

        public MenuRegistrar(IMenuInitializer menuInitializer)
        {
            _menuInitializer = menuInitializer;

        }

        /// <summary>
        /// Perform once-off startup processing.
        /// </summary>
        public void Start()
        {
            var item = new RoutedMenuItem("mnuItem", "Messages",
                                          new
                                              {
                                                  controller = "Home",
                                                  action = "Index",
                                                  area = "Messaging"
                                              });
            _menuInitializer.MainMenu.Add(item);
        }
    }
}