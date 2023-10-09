using Microsoft.AspNetCore.Mvc;

namespace Fresh.Controllers
{
    public class MenuController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
