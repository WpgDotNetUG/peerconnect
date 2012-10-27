using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using PeerCentral.Domain;
using PeerCentral.Storage.NHibernate;
using PeerCentral.Storage.NHibernate.Domain;

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
                .ToMethod(x => SeedUsers(SessionFactoryGateway.CreateSessionFactory()))
                .InSingletonScope();
            
            this.Bind<ISession>()
                .ToMethod(x => x.Kernel.Get<ISessionFactory>().OpenSession())
                .InRequestScope();
        }

        public ISessionFactory SeedUsers(ISessionFactory factory)
        {
            var users = new[]
                       {
                           new User {Name = "Joe"},
                           new User {Name = "Anne"},
                           new User {Name = "Admin"}
                       };

            using (var s = factory.OpenSession())
            {
                if (!s.Query<IUser>().Any())
                {
                    users.ForEach(u => s.Save(u));
                }
            }

            return factory;
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
