using System.Web.Mvc;
using PeerCentral.Domain;
using PeerCentral.WebClient.Views.Home;

namespace PeerCentral.WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRuntimeSession _runtimeSession;

        public HomeController(IRuntimeSession runtimeSession)
        {
            _runtimeSession = runtimeSession;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View(new IndexViewModel
            {
                CurrentUser = this._runtimeSession.CurrentUser
            });
        }

        public ActionResult Logout()
        {
            _runtimeSession.Logout();

            return Redirect("~/");
        }
    }
}
