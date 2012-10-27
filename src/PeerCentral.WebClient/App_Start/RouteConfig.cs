using System.Web.Routing;
using PeerCentral.WebClient.Configuration;
using RestfulRouting;

namespace PeerCentral.WebClient.App_Start
{
    /// <summary>
    /// Configures routing for the app
    /// </summary>
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Clear();
            routes.MapRoutes<Routes>();
        }
    }
}