using System.Web.Mvc;
using System.Web.Routing;
using RestfulRouting;
using PeerCentral.WebClient.Controllers;

namespace PeerCentral.WebClient.Configuration
{
    public class Routes : RouteSet
    {
        public override void Map(IMapper map)
        {
            map.DebugRoute("routedebug");

            map.Root<HomeController>(c => c.Index());

            // use the standard restful routes for managing runtime Sessions:
            map.Resource<SessionController>(c => c.Only("create", "new", "destroy"));

            // add human-friendly shortcuts for logging in and out:
            map.Path("login").GetOnly().To<SessionController>(c => c.New()); 
            map.Path("logout").GetOnly().To<HomeController>(c => c.Logout());

            // HACK: add a standard MVC pattern-matching route limited 
            //       to the PartialController to support calls like:
            //       Html.RenderAction("login", "partial");
            map.Route(new Route("partial/{action}",
                                new RouteValueDictionary(new {controller = "partial"}),
                                new MvcRouteHandler()));
        }
    }
}