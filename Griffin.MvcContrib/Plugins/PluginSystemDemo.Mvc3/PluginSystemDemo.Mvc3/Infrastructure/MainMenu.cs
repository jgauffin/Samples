using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Griffin.MvcContrib.Plugins;

namespace PluginSystemDemo.Mvc3.Infrastructure
{
    public class MainMenu
    {
        public static IMenuWithChildren Current = new RoutedMenuItem("mnuMain", "Main",
                                                                     new {controller = "Home", action = "Index"});
    }
}