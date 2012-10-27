using NHibernate;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using PeerCentral.Domain;
using PeerCentral.Storage.NHibernate;

namespace PeerCentral.WebClient.Configuration
{
    public class NinjectWebClientModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IRuntimeSession>().To<HttpRuntimeSession>();
            this.Bind(typeof (IRepository<>)).To(typeof (NHRepository<>)).InRequestScope();

            this.Bind<ISessionFactory>()
                .ToMethod(x => FakeDataProvider.Seed(SessionFactoryGateway.CreateSessionFactory()))
                .InSingletonScope();

            this.Bind<ISession>()
                .ToMethod(x => x.Kernel.Get<ISessionFactory>().OpenSession())
                .InRequestScope();
        }
    }
}
