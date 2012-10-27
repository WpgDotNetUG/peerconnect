using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using PeerCentral.Domain;
using PeerCentral.Storage.NHibernate.Domain;
using PeerCentral.WebClient.Models;

namespace PeerCentral.WebClient.Controllers
{
    public class BragController : Controller
    {
        private readonly IRuntimeSession _runtimeSession;
        private IRepository<IBrag> _repository;

        public BragController(IRuntimeSession runtimeSession, IRepository<IBrag> repository)
        {
            _runtimeSession = runtimeSession;
            _repository = repository;
        }

        public ActionResult New()
        {
            var vm = new NewBragViewModel();

            return View(vm);
        }

        public ActionResult Create(NewBragViewModel viewModel)
        {
            var brag = new Brag
            {
                Author = this._runtimeSession.GetCurrentUser(),
                Title = viewModel.Title,
                Description = viewModel.Description,
                SubmittedOn = DateTime.Now
            };

            this._repository.Save(brag);

            return null;
        }
    }

    public class NewBragViewModel 
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [HiddenInput]
        public int? AuthorId { get; set; }
    }
}
