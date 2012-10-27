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

        public IUser CurrentUser
        {
            get { return HttpContext.Current.Session[CURRENTUSERKEY] as IUser; }
        }

        public void Logout()
        {
            HttpContext.Current.Session[CURRENTUSERKEY] = null;
        }
    }
}