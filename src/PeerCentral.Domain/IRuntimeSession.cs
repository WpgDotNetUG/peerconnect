namespace PeerCentral.Domain
{
    public interface IRuntimeSession
    {
        void Login(IUser user);
        bool IsAuthenticated { get; }
        IUser GetCurrentUser();
        void Logout();
    }
}