using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Entitys;
using SqlSugar;
using System.Reflection;

namespace Fresh.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ToolController : ControllerBase
    {
        // sqlsugar对象
        private readonly ISqlSugarClient _db;
        // 注入sqlsugar对象
        // PS:在Program中不要忘了注册，不然这里注入的就是null
        public ToolController(ISqlSugarClient db) => _db = db;
        [HttpGet]
        public string CodeFirst()
        {
            try
            {
                // 1、创建数据库
                // PS：如果不存在则创建数据库，存在则跳过 （不用担心重复执行）
                _db.DbMaintenance.CreateDatabase();

                #region 删除之前的表
                var list = _db.DbMaintenance.GetTableInfoList();
                if (list != null && list.Count > 0)
                {
                    list.ForEach(t =>
                    {
                        // 删除表
                        _db.DbMaintenance.DropTable(t.Name);
                    });
                }
                #endregion

                // 2、动态创建表
                // 当前项目有很多类文件，因此需要根据命名空间过滤，仅将下面空间视为实体表
                string nspace = "Model.Entitys";
                // 通过反射得到一个类型数组 (AppContext.BaseDirectory:获取项目运行的目录）
                Type[] ass = Assembly.LoadFrom(AppContext.BaseDirectory + "Model.dll").GetTypes()
                    .Where(s => s.Namespace == nspace).ToArray();
                // 拿到所有实体之后，通过codefirst方式生成表
                _db.CodeFirst.SetStringDefaultLength(200).InitTables(ass);
                // 3、添加测试数据 
                // 初始化用户、角色、菜单
                Users user1 = new Users()
                {
                    Name = "admin",
                    NickName = "炒鸡管理员",
                    Password = "123456",
                    UserType = 0,
                    IsEnable = true,
                    Description = "数据库初始化时默认添加的炒鸡管理员",
                    CreateDate = DateTime.Now,
                    CreateUserId = 0,
                };
                Users user2 = new Users()
                {
                    Name = "jack",
                    NickName = "杰克",
                    Password = "123456",
                    UserType = 1,
                    IsEnable = true,
                    Description = "数据库初始化时默认添加的普通用户",
                    CreateDate = DateTime.Now,
                    CreateUserId = 0,
                };
                long userId1 = _db.Insertable(user1).ExecuteReturnBigIdentity();
                long userId2 = _db.Insertable(user2).ExecuteReturnBigIdentity();
                long m_menuId = _db.Insertable(new Menu()
                {
                    Name = "菜单管理",
                    Index = "/Menu/Index",
                    Order = 1,
                    IsEnable = true,
                    Description = "数据库初始化时默认添加的默认菜单",
                    CreateDate = DateTime.Now,
                    CreateUserId = userId1
                }).ExecuteReturnBigIdentity();
                long m_roleId = _db.Insertable(new Menu()
                {
                    Name = "角色管理",
                    Index = "/Role/Index",
                    Order = 2,
                    IsEnable = true,
                    Description = "数据库初始化时默认添加的默认菜单",
                    CreateDate = DateTime.Now,
                    CreateUserId = userId1
                }).ExecuteReturnBigIdentity();
                long m_userId = _db.Insertable(new Menu()
                {
                    Name = "人员管理",
                    Index = "/User/Index",
                    Order = 3,
                    IsEnable = true,
                    Description = "数据库初始化时默认添加的默认菜单",
                    CreateDate = DateTime.Now,
                    CreateUserId = userId1
                }).ExecuteReturnBigIdentity();
                long m_proId = _db.Insertable(new Menu()
                {
                    Name = "产品管理",
                    Index = "/Product/Index",
                    Order = 3,
                    IsEnable = true,
                    Description = "数据库初始化时默认添加的默认菜单",
                    CreateDate = DateTime.Now,
                    CreateUserId = userId1
                }).ExecuteReturnBigIdentity();
                long m_orderId = _db.Insertable(new Menu()
                {
                    Name = "订单管理",
                    Index = "/Order/Index",
                    Order = 3,
                    IsEnable = true,
                    Description = "数据库初始化时默认添加的默认菜单",
                    CreateDate = DateTime.Now,
                    CreateUserId = userId1
                }).ExecuteReturnBigIdentity();
                Role role1 = new Role()
                {
                    Name = "管理员",
                    Order = 0,
                    IsEnable = true,
                    Description = "管理员角色"
                };
                Role role2 = new Role()
                {
                    Name = "普通用户",
                    Order = 0,
                    IsEnable = true,
                    Description = "普通用户角色"
                };
                long roleId1 = _db.Insertable(role1).ExecuteReturnBigIdentity();
                long roleId2 = _db.Insertable(role2).ExecuteReturnBigIdentity();
                // 给用户设置角色
                List<UserRoleRelation> urrList = new List<UserRoleRelation>()
                {
                    new UserRoleRelation{ UserId = userId1, RoleId = roleId1 },
                    new UserRoleRelation{ UserId = userId2, RoleId = roleId2 },
                };
                _db.Insertable(urrList).ExecuteCommand();
                // 给角色设置菜单
                List<MenuRoleRelation> mrrList = new List<MenuRoleRelation>() {
                    new MenuRoleRelation{ MenuId = m_menuId, RoleId = roleId1 },
                    new MenuRoleRelation{ MenuId = m_roleId, RoleId = roleId1 },
                    new MenuRoleRelation{ MenuId = m_userId, RoleId = roleId1 },
                    new MenuRoleRelation{ MenuId = m_proId, RoleId = roleId1 },
                    new MenuRoleRelation{ MenuId = m_orderId, RoleId = roleId1 },
                    new MenuRoleRelation{ MenuId = m_orderId, RoleId = roleId2 },
                };
                _db.Insertable(mrrList).ExecuteCommand();
                List<OrderInfo> orders = new List<OrderInfo>();
                //初始化订单数据
                for (int i = 0; i < 11; i++)
                {
                    orders.Add(new OrderInfo()
                    {
                        OrderNumber = DateTime.Now.ToString("yyyyMMddHHmmssffff"),
                        ProductId = "10010" + i,
                        Price = 200 + i,
                        Remark = "xxx页面下单" + i,
                        Description = "数据初始化"
                    });
                }
                _db.Insertable(orders).ExecuteCommand();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
