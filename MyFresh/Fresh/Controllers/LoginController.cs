using Fresh.Config;
using Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.User;
using Model.Other;
using System.Security.Claims;
using Service;

namespace Fresh.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        public LoginController(IUserService userService) => _userService = userService;
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 没有授权
        /// </summary>
        /// <returns></returns>
        public IActionResult NoAuthority()
        {
            return View();
        }

        [HttpPost]
        public async Task<ApiResult> Submit(string name, string password)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
            {
                return await ResultHelper.Error("参数不能为空！");
            }
            var info = _userService.GetUser(name, password);
            if (info.Name != null)
            {
                // 记录用户信息
                RecordAuthentication(info, password);
                return await ResultHelper.Success(info);
            }
            else
            {
                return await ResultHelper.Error("用户名或密码错误！");
            }
        }

        private void RecordAuthentication(UserRes info, string password)
        {
            // 定义
            var claims = new List<Claim>()//鉴别你是谁，相关信息
                    {
                        new Claim("UserId",info.Id.ToString()),
                        new Claim("Name",info.Name),
                        new Claim("NickName",info.NickName),
                        new Claim("Password",password),//可以写入任意数据 
                    };
            // 使用
            ClaimsPrincipal userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Customer"));
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(30),//过期时间：30分钟
            }).Wait();
        }

        public async Task<ApiResult> CreateUser(string name, string nickName, string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(nickName) || string.IsNullOrEmpty(password))
            {
                return await ResultHelper.Error("参数不能为空");
            }
            if (password != confirmPassword)
            {
                return await ResultHelper.Error("两次输入的密码不一致！");
            }
            string msg = string.Empty;
            var info = _userService.Register(name, nickName, password, ref msg);
            if (info.Name != null && msg == null)
            {
                RecordAuthentication(info, password);
                return await ResultHelper.Success(info);
            }
            return await ResultHelper.Success("");
        }
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();
            // 重定向到登录页
            return Redirect("/Login/Index");
        }
    }
}
