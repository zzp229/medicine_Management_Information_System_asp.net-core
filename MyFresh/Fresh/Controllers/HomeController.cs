using Fresh.Filter;
using Fresh.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Fresh.Controllers
{
    public class HomeController : BaseController
    {
        // 日志对象
        private readonly ILogger<HomeController> _logger;

        // 构造函数注入日志
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [TypeFilter(typeof(CustomResourceFilter))]
        [TypeFilter(typeof(CustomActionFilter))]
        [TypeFilter(typeof(CustomResultFilter))]
        [TypeFilter(typeof(CustomExceptionFilter))]
        public IActionResult Index()
        {
            _logger.LogInformation("这里是首页");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        #region 图表数据
        public JsonResult GetData()
        {
            List<int> ints = new List<int>() { 5, 20, 36, 10, 10, 20 };
            List<string> strings = new List<string>() { "Shirts", "Cardigans", "Chiffons", "Pants", "Heels", "Socks" };
            return Json(new { ints, strings });
        }
        #endregion
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}