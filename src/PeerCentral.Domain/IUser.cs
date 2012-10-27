using System;

namespace PeerCentral.Domain
{
    public interface IUser
    {
        String Name { get; set; }
        int Id { get; set; }
    }
}
