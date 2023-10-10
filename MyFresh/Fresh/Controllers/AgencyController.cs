using Fresh.Config;
using Interface;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.Menu;
using Model.Entitys;
using Model.Other;

namespace Fresh.Controllers
{
    public class AgencyController : BaseController
    {
        private readonly IAgencyService _agency;
        public AgencyController(IAgencyService agency) => _agency = agency;
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ApiResult> Add(Agency req)
        {
            return await ResultHelper.Success(_agency.Add(req));
        }

        [HttpPost]
        public async Task<ApiResult> Edit(MenuEdit req)
        {
            // 获取用户id
            userId = Convert.ToInt64(HttpContext.User.Claims.ToList()[0].Value);
            return await ResultHelper.Success(_agency.Edit(req, userId));
        }

        [HttpGet]
        public async Task<ApiResult> Del(long id)
        {
            return await ResultHelper.Success(_agency.Del(id));
        }

        [HttpGet]
        public async Task<ApiResult> BatchDel(string ids)
        {
            return await ResultHelper.Success(_agency.Batch(ids));
        }

        [HttpPost]
        public async Task<ApiResult> GetAgencys(Agency req)
        {
            return await ResultHelper.Success(_agency.GetAgencys(req));
        }
    }
}
