using AutoMapper;
using Model.Dto.Menu;
using Model.Dto.Order;
using Model.Dto.Product;
using Model.Dto.Role;
using Model.Dto.User;
using Model.Entitys;

namespace Fresh.Config
{
    public class AutoMapperConfigs : Profile
    {
        /// <summary>
        /// 在构造函数中配置映射关系
        /// </summary>
        public AutoMapperConfigs()
        {
            // 角色
            CreateMap<Role, RoleRes>();
            CreateMap<RoleAdd, Role>();
            CreateMap<RoleEdit, Role>();
            // 用户
            CreateMap<Users, UserRes>();
            CreateMap<UserAdd, Users>();
            CreateMap<UserEdit, Users>();
            // 菜单
            CreateMap<Menu, MenuRes>();
            CreateMap<MenuAdd, Menu>();
            CreateMap<MenuEdit, Menu>();
            // 商品
            CreateMap<Product, ProductRes>();
            CreateMap<ProductAdd, Product>();
            CreateMap<ProductEdit, Product>();
            // 订单
            CreateMap<OrderInfo, OrderRes>();
            CreateMap<OrderRes, OrderInfo>();
        }

    }
}
