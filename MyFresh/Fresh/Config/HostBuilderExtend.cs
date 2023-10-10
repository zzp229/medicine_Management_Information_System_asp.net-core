using Autofac;
using Autofac.Extensions.DependencyInjection;
using Fresh.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SqlSugar;

namespace Fresh.Config
{
    /// <summary>
    /// 新建一个扩展类：避免代码都写在program中
    /// </summary>
    public static class HostBuilderExtend
    {
        public static void Register(this WebApplicationBuilder builder)
        {
            // 使用Autofac替换内置IOC
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(container =>
            {
                #region 通过模块化的方式注册接口层和实现层 
                container.RegisterModule(new AutofacModuleRegister());
                #endregion
                #region 注册SqlSugar
                container.Register<ISqlSugarClient>(context =>
                {
                    SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
                    {
                        // 通过配置读取字符串更灵活，避免代码写死
                        ConnectionString = builder.Configuration.GetSection("conn").Value,
                        DbType = DbType.SqlServer,
                        IsAutoCloseConnection = true,
                    });
                    // 开启sqlsugar的aop功能：记录每次执行的sql和参数
                    db.Aop.OnLogExecuted = (sql, par) =>
                    {
                        Console.WriteLine($"==================");
                        Console.WriteLine($"Sql语句：{sql}");
                        List<string> list = new List<string>();
                        par.ToList().ForEach(p => { list.Add(p.Value != null ? p.Value.ToString() : ""); });
                        Console.WriteLine($"参数:{string.Join(",", list)}");
                    };
                    return db;
                });
                #endregion
            });

            // 注册日志
            builder.Logging.AddLog4Net("CfgFile/log4net.Config");

            //设置JSON返回日期格式
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();//设置JSON返回格式同model一致
            });

            // Auaomapper映射
            builder.Services.AddAutoMapper(typeof(AutoMapperConfigs));

            #region 配置鉴权
            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, option =>
            {
                option.LoginPath = "/Login/Index";//如果没有找到用户信息---鉴权失败--授权也失败了---就跳转到指定的Action
                option.AccessDeniedPath = "/Login/NoAuthority";
            });
            #endregion

            #region 定义策略
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Policy", policyBuilder =>
                {
                    policyBuilder.AddRequirements(new CustomRequirement());
                });
            });
            builder.Services.AddTransient<IAuthorizationHandler, CustomHandler>();
            #endregion
        }
    }
}
