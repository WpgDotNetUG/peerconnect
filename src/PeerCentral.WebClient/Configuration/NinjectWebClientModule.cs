using System.Linq;
using NHibernate;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using PeerCentral.Domain;
using PeerCentral.Storage.NHibernate;
using PeerCentral.WebClient.Models;

namespace PeerCentral.WebClient.Configuration
{
    public class NinjectWebClientModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IRuntimeSession>().To<HttpRuntimeSession>();
            this.Bind<IRepository<IBrag>>().To<FakeBragRepository>();
            this.Bind(typeof(IRepository<>)).To(typeof(NHRepository<>)).InRequestScope();

            this.Bind<ISessionFactory>()
                .ToMethod(x => SessionFactoryGateway.CreateSessionFactory())
                .InSingletonScope();
            
            this.Bind<ISession>()
                .ToMethod(x => x.Kernel.Get<ISessionFactory>().OpenSession())
                .InRequestScope();
        }
    }

    public class FakeBragRepository : IRepository<IBrag>
    {
        public IQueryable<IBrag> All()
        {
            return Enumerable.Range(1, 5).Select(i => new Brag()
            {
                Id = i,
                Title = "Brag #" + i,
                Description = "This is the wonderful world of Braggart #" + i,
                SubmittedOn = DateTime.Now,
                Author = new User { Id = i * 100, Name = "User #" + 1 }
            }
            ).AsQueryable().Take(0);
        }
    }
}
