using Microsoft.AspNetCore.Mvc;

namespace Fresh.Controllers
{
    public class RoleController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
