using System;
using PeerCentral.Domain;

namespace PeerCentral.Storage.NHibernate.Domain
{
    public class User : IUser
    {
        public virtual String Name { get; set; }

        public virtual int Id { get; set; }
    }
}