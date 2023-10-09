using Microsoft.AspNetCore.Mvc;

namespace Fresh.Controllers
{
    public class UserController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
