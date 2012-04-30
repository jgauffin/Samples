using System.Web.Routing;

namespace PluginSystemDemo.PluginBase
{
    /// <summary>
    /// Any extra routes (other than the area route)
    /// </summary>
    public interface ICustomRouteProvider
    {
        /// <summary>
        /// Register entries
        /// </summary>
        /// <param name="routes">Route to register</param>
        void Register(RouteTable routes);
    }
}