using Microsoft.AspNetCore.Mvc;

namespace Fresh.Controllers
{
    public class ProductController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
