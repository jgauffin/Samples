using Griffin.MvcContrib.Plugins;
using PluginSystemDemo.PluginBase;

namespace Plugin.Messaging
{
    /// <summary>
    /// Use Autofacs IStartable to force the registration
    /// </summary>
    public class MenuRegistrar : IMenuRegistrar
    {
        public void Register(IMenuWithChildren mainMenu)
        {
            var item = new RoutedMenuItem("mnuItem", "Messages",
                                          new
                                          {
                                              controller = "Home",
                                              action = "Index",
                                              area = "Messaging"
                                          });
            mainMenu.Add(item);
        }
    }
}