using MvcContrib.TestHelper;
using NUnit.Framework;
using PeerCentral.Domain;
using PeerCentral.Storage.NHibernate.Domain;
using PeerCentral.WebClient.Controllers;
using Rhino.Mocks;

namespace PeerCentral.WebClient.UnitTests.Controllers
{
    public class BragControllerTests : ControllerTests
    {
        private IRepository<IBrag> repository;
        private BragController controller;

        [SetUp]
        public void Setup()
        {
            this.repository = MockRepository.GenerateMock<IRepository<IBrag>>();
            this.controller = this._builder.CreateController<BragController>(this._runtimeSession, repository);
        }

        [Test]
        public void New_ReturnsAView()
        {
            var result = this.controller.New();

            var viewModel = result.AssertViewRendered().WithViewData<NewBragViewModel>();

            Assert.That(viewModel.Title, Is.Null);
            Assert.That(viewModel.Description, Is.Null);
            Assert.That(viewModel.AuthorId, Is.Null);
        }

        [Test]
        public void Create_WithValidViewModel_ShouldSaveANewBrag()
        {
            // arrange
            var authenticatedUser = A_user_is_logged_in(1, "Amir");
            var viewModel = new NewBragViewModel
            {
                AuthorId = 1,
                Title = "Awesome thing",
                Description = "This thing I did was really awesome."
            };

            // act
            var result = this.controller.Create(viewModel);

            // assert
            repository.AssertWasCalled(r => r.Save(Arg<Brag>.Matches(b => 
                b.Author == authenticatedUser
                && b.Title == "Awesome thing")));
        }
    }
}