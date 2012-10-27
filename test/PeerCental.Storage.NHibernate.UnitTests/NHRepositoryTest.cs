using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Linq;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using PeerCentral.Domain;
using PeerCentral.Storage.NHibernate;
using Rhino.Mocks;
using Environment = NHibernate.Cfg.Environment;

namespace PeerCental.Storage.NHibernate.UnitTests
{
    public class NHRepositoryTest
    {
        private ISession _session;
        private NHRepository<IUser> _sut;
        private Configuration _configuration;
        private ISessionFactory _sessionFactory;

        [SetUp]
        public void Before_each_test()
        {
            this.NHConfig();

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
            var tenUsers = Enumerable
                .Range(1, 10)
                .Select(i => new User { Id = i, Name = "User " + i});

            tenUsers.ForEach(u => this._session.Save(u));

            return tenUsers;
        }


        private void NHConfig()
        {
            if (_configuration != null) return;

            this._configuration = new Configuration()
                .SetProperty(Environment.ReleaseConnections, "on_close")
                .SetProperty(Environment.Dialect, typeof(SQLiteDialect).AssemblyQualifiedName)
                .SetProperty(Environment.ConnectionDriver, typeof(SQLite20Driver).AssemblyQualifiedName)
                .SetProperty(Environment.ConnectionString, "data source=:memory:")
                .AddAssembly(Assembly.GetAssembly(typeof(User)));

            this._sessionFactory = this._configuration.BuildSessionFactory();

            this._session = this._sessionFactory.OpenSession();

            new SchemaExport(this._configuration).Execute(false, true, false, _session.Connection, null);
        }
    }
}