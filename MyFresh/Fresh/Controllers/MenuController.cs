using Fresh.Config;
using Interface;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.Menu;
using Model.Other;

namespace Fresh.Controllers
{
    public class MenuController : BaseController
    {
        private readonly IMenuService _menu;
        public MenuController(IMenuService menu) => _menu = menu;
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ApiResult> Add(MenuAdd req)
        {
            // 获取用户id
            userId = Convert.ToInt64(HttpContext.User.Claims.ToList()[0].Value);
            return await ResultHelper.Success(_menu.Add(req, userId));
        }
        [HttpPost]
        public async Task<ApiResult> Edit(MenuEdit req)
        {
            // 获取用户id
            userId = Convert.ToInt64(HttpContext.User.Claims.ToList()[0].Value);
            return await ResultHelper.Success(_menu.Edit(req, userId));
        }
        [HttpGet]
        public async Task<ApiResult> Del(long id)
        {
            return await ResultHelper.Success(_menu.Del(id));
        }
        [HttpGet]
        public async Task<ApiResult> BatchDel(string ids)
        {
            return await ResultHelper.Success(_menu.Batch(ids));
        }
        [HttpPost]
        public async Task<ApiResult> GetMenus(MenuReq req)
        {
            return await ResultHelper.Success(_menu.GetMenus(req));
        }
    }
}
