using System.Web.Mvc;

namespace DrinkMachine.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ViewResult Index()
        {
            return View();
        }
    }
}