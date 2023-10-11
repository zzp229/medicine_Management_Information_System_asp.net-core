using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fresh.Controllers
{
    /// <summary>
    /// 带权限才能访问
    /// </summary>
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Policy = "Policy")]
    public class BaseController : Controller
    {
        public long userId;

        //这里控制权限，设置哪个用户该使用什么
        public bool IsShow = false;
    }
}
