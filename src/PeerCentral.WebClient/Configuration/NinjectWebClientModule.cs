using System;
using System.Linq;
using Ninject.Modules;
using PeerCentral.Domain;
using PeerCentral.WebClient.Models;

namespace PeerCentral.WebClient.Configuration
{
    public class NinjectWebClientModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IRuntimeSession>().To<HttpRuntimeSession>();
            this.Bind<IRepository<IUser>>().To<FakeUserRepository>();
            this.Bind<IRepository<IBrag>>().To<FakeBragRepository>();
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

    public class FakeUserRepository : IRepository<IUser>
    {
        public IQueryable<IUser> All()
        {
            return new[]
                       {
                           new User {Id = 1, Name = "Joe"},
                           new User {Id = 2, Name = "Anne"},
                           new User {Id = 3, Name = "Admin"}
                       }.AsQueryable();
        }
    }
}
