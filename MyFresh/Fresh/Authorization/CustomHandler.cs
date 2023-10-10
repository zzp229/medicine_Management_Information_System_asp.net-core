using Interface;
using Microsoft.AspNetCore.Authorization;

namespace Fresh.Authorization
{
    public class CustomHandler : AuthorizationHandler<CustomRequirement>
    {
        private readonly IUserService _userService;
        public CustomHandler(IUserService userService) => _userService = userService;
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRequirement requirement)
        {
            DefaultHttpContext res = context.Resource! as DefaultHttpContext;
            RouteValueDictionary dic = res!.Request.RouteValues;

            // 验证你有没有登录过
            if (context.User.Claims.Count() == 0)
            {
                return Task.CompletedTask;
            } 
            // 登录了，首先验证用户信息是否正确
            string name = context.User.Claims.First(c => c.Type == "Name").Value;
            string password = context.User.Claims.First(c => c.Type == "Password").Value;
            var info = _userService.GetUser(name, password);
            if (info.Name != null)
            {
                // 其次验证用户角色与权限是否匹配
                // 获取当前访问的控制器名称和方法名称
                string currRouter = $"/{dic["controller"]}/";
                if (!"/Home/Index".Contains(currRouter))
                {
                    if (!_userService.GetUserMenus(info.Id).Any(p => p.Index.Contains(currRouter)))
                    {
                        return Task.CompletedTask;
                    }
                }
                context.Succeed(requirement); 
            }
            return Task.CompletedTask;
        }
    }
}
