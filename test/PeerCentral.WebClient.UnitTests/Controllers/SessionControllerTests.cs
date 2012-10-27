using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcContrib.TestHelper;
using NUnit.Framework;
using PeerCentral.Domain;
using PeerCentral.WebClient.Controllers;
using PeerCentral.WebClient.Models;
using Rhino.Mocks;

namespace PeerCentral.WebClient.UnitTests.Controllers
{
    public class SessionControllerTests
    {
        private SessionController _controller;
        private TestControllerBuilder _builder;
        private IRepository<IUser> _repository;
        private IRuntimeSession _runtimeSession;

        [SetUp]
        public void Setup()
        {
            this._builder = new TestControllerBuilder();
            this._repository = MockRepository.GenerateMock<IRepository<IUser>>();
            this._runtimeSession = MockRepository.GenerateMock<IRuntimeSession>();
            this._controller = _builder.CreateController<SessionController>(this._runtimeSession, this._repository);
        }

        [Test]
         public void New_Should_Return_A_List_Of_Users()
         {
             // arrange
              var expected = Enumerable.Range(1, 10).Select(i => MockRepository.GenerateMock<IUser>()).AsQueryable();
             this._repository.Stub(r => r.All()).Return(expected);

             // act
             var result = _controller.New();

             // assert
             var actual = result.AssertResultIs<ViewResult>().WithViewData<IEnumerable<IUser>>();

             Assert.That(actual, Is.EqualTo(expected));
         }

        [Test]
        public void Create_Should_Set_The_Session_To_The_Specified_user()
        {
            // arrange
            var user = new User {Id = 1, Name = "user"};
            this._repository.Stub(r => r.All()).Return(new [] {user}.AsQueryable());

            // act
            var result = _controller.Create(1);

            // assert
            result.AssertActionRedirect().ToAction<HomeController>(c => c.Index());

            this._runtimeSession.AssertWasCalled(s => s.Login(user));
        }
    }
}