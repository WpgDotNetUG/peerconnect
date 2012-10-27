using System;
using System.Collections.Generic;
using PeerCentral.Domain;

namespace PeerCentral.Storage.NHibernate.Domain
{
    public class User : IUser
    {
        public virtual String Name { get; set; }

        public virtual int Id { get; set; }

        public virtual IEnumerable<IBrag> Braggings { get; set; }
    }
}