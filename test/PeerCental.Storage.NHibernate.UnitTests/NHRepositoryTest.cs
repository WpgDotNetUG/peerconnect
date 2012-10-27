using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NUnit.Framework;
using PeerCentral.Domain;
using PeerCentral.Storage.NHibernate;
using Rhino.Mocks;

namespace PeerCental.Storage.NHibernate.UnitTests
{
    public class NHRepositoryTest
    {
        private ISession _session;
        private NHRepository<IUser> _sut;

        [SetUp]
        public void Before_each_test()
        {
            this._session = MockRepository.GenerateMock<ISession>();

            this._sut = new NHRepository<IUser>(this._session);
        }

        [Test]
        public void It_Should_query_the_session()
        {
            // arrange
            var expected = Given_I_have_ten_users();

            // act
            var actual = this._sut.All();

            // assert
            Assert.That(actual, Is.EqualTo(expected), "Both collections should contain same elements");
        }

        private static IEnumerable<IUser> Given_I_have_ten_users()
        {
            return Enumerable.Range(1, 10).Select(i => MockRepository.GenerateMock<IUser>());
        }
    }
}
