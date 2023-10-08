using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using SqlSugar;

namespace MVCFresh.Config
{
    /// <summary>
    /// 新建一个拓展类：避免代码都在program中
    /// </summary>
    public static class HostBuilderExtend
    {
        public static void Register(this WebApplicationBuilder builder)
        {
            //使用Autofac代替IOC
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(container =>
            {
                #region 注册sqlsugar
                container.Register<ISqlSugarClient>(context =>
                {
                    SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
                    {
                        ConnectionString = builder.Configuration.GetSection("conn").Value,
                        DbType = DbType.SqlServer,
                        
                        IsAutoCloseConnection = true,
                    });
                    //开启sqlsugar的apo功能：记录执行的sql和参数
                    db.Aop.OnLogExecuted = (sql, par) =>
                    {
                        Console.WriteLine("=======");
                        Console.WriteLine($"sql语句{sql}");
                        List<string> list = new List<string>();
                        par.ToList().ForEach(p => { list.Add(p.Value != null ? p.Value.ToString() : ""); });
                        Console.WriteLine($"参数：{string.Join(",", list)}");
                    };
                    return db;
                });
                #endregion
            });

            //注册日志
            builder.Logging.AddLog4Net("CfgFile/log4net.Config");
        }
    }
}
