using System.Linq;
using NHibernate;
using NHibernate.Linq;
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
            return this._session.Query<T>();
        }

        public void Save(T item)
        {
            this._session.SaveOrUpdate(item);
        }
    }
}
