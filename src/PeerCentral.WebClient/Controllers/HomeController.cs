using System.Web.Mvc;
using PeerCentral.Domain;
using PeerCentral.WebClient.Views.Home;

namespace PeerCentral.WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRuntimeSession _runtimeSession;
        private readonly IRepository<IBrag> _bragRepository;

        public HomeController(IRuntimeSession runtimeSession, IRepository<IBrag> bragRepository)
        {
            _runtimeSession = runtimeSession;
            _bragRepository = bragRepository;
        }

        public ActionResult Index()
        {
            if (!_runtimeSession.IsAuthenticated)
            {
                return View("Home");
            }

            return View("Dashboard", new DashboardViewModel
                                         {
                                             CurrentUser = this._runtimeSession.GetCurrentUser(),
                                             RecentBrags = this._bragRepository.All()
                                         });
        }

        public ActionResult Logout()
        {
            _runtimeSession.Logout();

            return Redirect("~/");
        }
    }
}
