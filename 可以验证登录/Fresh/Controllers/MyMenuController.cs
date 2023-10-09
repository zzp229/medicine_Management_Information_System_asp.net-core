using Fresh.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Model.Other;

namespace Fresh.Controllers
{
    public class MyMenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        
    }
}
