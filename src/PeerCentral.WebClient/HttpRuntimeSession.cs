using System.Web;
using PeerCentral.Domain;

namespace PeerCentral.WebClient
{
    public class HttpRuntimeSession : IRuntimeSession
    {
        private const string CURRENTUSERKEY = "CurrentUser";

        public void Login(IUser user)
        {
            HttpContext.Current.Session[CURRENTUSERKEY] = user;
        }

        public IUser GetCurrentUser()
        {
            return HttpContext.Current.Session[CURRENTUSERKEY] as IUser;
        }

        public bool IsAuthenticated
        {
            get { return GetCurrentUser() != null; }
        }

        public void Logout()
        {
            HttpContext.Current.Session[CURRENTUSERKEY] = null;
        }
    }
}