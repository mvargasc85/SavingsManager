using System.Web.Mvc;
using SavingsManager.Factory;

namespace SavingsManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISavingsProvider _planProvider;

        public HomeController()
        {
            _planProvider = SavingsProviderFactory.CreateSavingsModelObject("Plan");
        }

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
      
    }
}