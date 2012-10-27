using System.Web.Mvc;

namespace PeerCentral.WebClient.Models
{
    public interface IRestfulResource<in TId>
    {
        ActionResult Index();
        ActionResult Show(TId id);
        ActionResult New();
        ActionResult Create();
        ActionResult Edit(TId id);
        ActionResult Update(TId id);
        ActionResult Destroy(TId id);
    }

    public interface IRestfulResources<in TId>
    {
        ActionResult Index();
        ActionResult Show(TId id);
        ActionResult New();
        ActionResult Create();
        ActionResult Edit(TId id);
        ActionResult Update(TId id);
        ActionResult Destroy(TId id);
    }
}