using Microsoft.AspNetCore.Mvc;

namespace Fresh.Controllers
{
    public class OrderController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
