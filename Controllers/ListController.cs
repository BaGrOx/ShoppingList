using Microsoft.AspNetCore.Mvc;

namespace ShopList.Controllers
{
    public class ListController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

    }
}
