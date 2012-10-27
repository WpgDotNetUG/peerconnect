using System.Linq;

namespace PeerCentral.Domain
{
    public interface IRepository<T>
    {
        IQueryable<T> All();

        void Save(T item);
    }
}
