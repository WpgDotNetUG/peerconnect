using System;
using PeerCentral.Domain;

namespace PeerCentral.Storage.NHibernate.Domain
{
    public class Brag : IBrag
    {
        public virtual int Id { get; set; }
        public virtual IUser Author { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime SubmittedOn { get; set; }
    }
}