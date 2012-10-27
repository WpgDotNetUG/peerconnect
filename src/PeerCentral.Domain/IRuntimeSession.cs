namespace PeerCentral.Domain
{
    public interface IRuntimeSession
    {
        void Login(IUser user);
        IUser CurrentUser { get; }
        void Logout();
    }
}