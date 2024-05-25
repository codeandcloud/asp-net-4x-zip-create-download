using System.Web.Mvc;

namespace ZipDownloader.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}