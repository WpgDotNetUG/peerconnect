using System;
using System.Collections.Generic;

namespace PeerCentral.Domain
{
    public interface IUser
    {
        String Name { get; set; }
        int Id { get; set; }
        IEnumerable<IBrag> Braggings { get; }
    }
}
