using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using PeerCentral.Domain;
using PeerCentral.Storage.NHibernate.Domain;

namespace PeerCentral.WebClient.Configuration
{
    /// <summary>
    /// Provides a set of fake data that we can use to bootstrap development,
    /// populating the NHibernate <see cref="ISessionFactory"/> with what is
    /// essentially design-time data.
    /// </summary>
    /// <remarks>Note for use in production.</remarks>
    public class FakeDataProvider 
    {
        public static ISessionFactory Seed(ISessionFactory factory)
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