using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcContrib.TestHelper;
using NUnit.Framework;
using PeerCentral.Domain;
using PeerCentral.WebClient.Controllers;
using Rhino.Mocks;

namespace PeerCentral.WebClient.UnitTests.Controllers
{
    public class SessionControllerTests
    {
        private SessionController _sut;
        private TestControllerBuilder _builder;
        private IRepository<IUser> _repository;
        private IRuntimeSession _runtimeSession;

        [SetUp]
        public void Setup()
        {
            this._builder = new TestControllerBuilder();
            this._repository = MockRepository.GenerateMock<IRepository<IUser>>();
            this._runtimeSession = MockRepository.GenerateMock<IRuntimeSession>();
            this._sut = _builder.CreateController<SessionController>(this._runtimeSession, this._repository);
        }

        [Test]
         public void New_Should_Return_A_List_Of_Users()
         {
             // arrange
             var expected = Given_I_have_a_bunch_of_users();

             // act
             var result = _sut.New();

             // assert
             var actual = result.AssertResultIs<ViewResult>().WithViewData<IEnumerable<IUser>>();

             Assert.That(actual, Is.EqualTo(expected));
         }

        [Test]
        public void Create_Should_set_the_session_to_the_specified_user()
        {
            // arrange
            var user = Given_I_have_a_user_with_id(1);

            // act
            var result = _sut.Create(1);

            // assert
            result.AssertActionRedirect().ToAction<HomeController>(c => c.Index());

            this._runtimeSession.AssertWasCalled(s => s.Login(user));
        }

        private IUser Given_I_have_a_user_with_id(int id)
        {
            var user = MockRepository.GenerateStub<IUser>();

            user.Id = id;

            this._repository.Stub(r => r.All()).Return(new[] { user }.AsQueryable());

            return user;
        }

        private IQueryable<IUser> Given_I_have_a_bunch_of_users()
        {
            var expected = Enumerable.Range(1, 10).Select(i => MockRepository.GenerateMock<IUser>()).AsQueryable();
            this._repository.Stub(r => r.All()).Return(expected);
            return expected;
        }
    }
}