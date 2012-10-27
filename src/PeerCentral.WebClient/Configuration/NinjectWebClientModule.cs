using System;
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
            this.Bind(typeof(IRepository<>)).To(typeof(NHRepository<>)).InRequestScope();

            this.Bind<ISessionFactory>()
                .ToMethod(x => Seed(SessionFactoryGateway.CreateSessionFactory()))
                .InSingletonScope();
            
            this.Bind<ISession>()
                .ToMethod(x => x.Kernel.Get<ISessionFactory>().OpenSession())
                .InRequestScope();
        }

        public ISessionFactory Seed(ISessionFactory factory)
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
                    using (var t = s.BeginTransaction())
                    {
                        users.ForEach(u =>
                                          {
                                              s.Save(u);

                                              s.Save(CreateBragForUser(u));
                                          });

                        t.Commit();
                    }
                }
            }

            return factory;
        }

        private static IBrag CreateBragForUser(IUser user)
        {
            return new Brag
                {
                    Title = "Brag #" + user.Name,
                    Description = "This is the wonderful world of Braggart #" + user.Name,
                    SubmittedOn = DateTime.Now,
                    Author = user
                };
        }
    }
}
