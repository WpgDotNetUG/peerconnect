using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using PeerCentral.Domain;
using PeerCentral.Storage.NHibernate;
using PeerCentral.Storage.NHibernate.Domain;

namespace PeerCental.Storage.NHibernate.UnitTests
{
    public class NHRepositoryTest : IDisposable
    {
        private ISession _session;
        private ISessionFactory _sessionFactory;
        private NHRepository<IUser> _sut;

        [SetUp]
        public void Before_each_test()
        {
            this._session = this.BuildSession();

            this._sut = new NHRepository<IUser>(this._session);
        }

        [Test]
        public void It_Should_get_all_users_from_the_session()
        {
            // arrange
            var expected = Given_I_have_ten_users().ToList();

            // act
            var actual = this._sut.All().ToList();

            // assert
            Assert.That(actual, Is.EqualTo(expected), "Both collections should contain same elements");
        }

        private IEnumerable<IUser> Given_I_have_ten_users()
        {
            return Enumerable.Range(1, 10).Select(CreateUserInStorage);
        }

        private ISession BuildSession()
        {
            this._sessionFactory = Fluently
                .Configure()
                .Database(SQLiteConfiguration
                              .Standard
                              .UsingFile(CreateTempDbTestFile())
                              .ShowSql())
                .Mappings(m => m.AutoMappings.Add(SessionFactoryGateway.CreateAutomappings))
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();

            return this._sessionFactory.OpenSession();
        }

        private static string CreateTempDbTestFile()
        {
            return Path.GetTempFileName();
        }

        private static void BuildSchema(Configuration config)
        {
            new SchemaExport(config).Create(true, true);
        }

        private IUser CreateUserInStorage(int id)
        {
            var u = new User { Id = id, Name = "User " + id };

            this._session.Save(u);

            return u;
        }

        public void Dispose()
        {
            this._session.Dispose();
        }
    }
}