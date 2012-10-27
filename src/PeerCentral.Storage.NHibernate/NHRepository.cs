using System.Linq;
using NHibernate;
using PeerCentral.Domain;

namespace PeerCentral.Storage.NHibernate
{
    public class NHRepository<T> : IRepository<T>
    {
        private readonly ISession _session;

        public NHRepository(ISession session)
        {
            this._session = session;
        }

        public IQueryable<T> All()
        {
            return Enumerable.Empty<T>().AsQueryable();
        }
    }
}
