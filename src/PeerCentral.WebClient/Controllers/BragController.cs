using System;
using System.Web.Mvc;
using PeerCentral.WebClient.Models;

namespace PeerCentral.WebClient.Controllers
{
    public class BragController : Controller, IRestfulResources<int?>
    {
        public ActionResult Index()
        {
            throw new NotImplementedException();
        }

        public ActionResult Show(int? id)
        {
            throw new NotImplementedException();
        }

        public ActionResult New()
        {
            throw new System.NotImplementedException();
        }

        public ActionResult Create()
        {
            throw new NotImplementedException();
        }

        public ActionResult Edit(int? id)
        {
            throw new NotImplementedException();
        }

        public ActionResult Update(int? id)
        {
            throw new NotImplementedException();
        }

        public ActionResult Destroy(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
