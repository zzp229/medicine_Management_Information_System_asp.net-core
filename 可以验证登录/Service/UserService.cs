using AutoMapper;
using Interface;
using Model.Dto.Menu;
using Model.Dto.User;
using Model.Entitys;
using SqlSugar;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ISqlSugarClient _db;
        public UserService(IMapper mapper, ISqlSugarClient db)
        {
            _mapper = mapper;
            _db = db;
        }
        public UserRes GetUser(string userName, string password)
        {
            var user = _db.Queryable<Users>().Where(p => p.Name == userName && p.Password == password).First();
            if (user != null)
            {
                return _mapper.Map<UserRes>(user);
            }
            return new UserRes();
        }
        public List<MenuRes> GetUserMenus(long id)
        {
            var res = _db.Queryable<Menu>()
                .InnerJoin<MenuRoleRelation>((m, mmr) => m.Id == mmr.MenuId)
                .InnerJoin<Role>((m, mmr, r) => mmr.RoleId == r.Id)
                .InnerJoin<UserRoleRelation>((m, mmr, r, urr) => urr.RoleId == r.Id)
                .InnerJoin<Users>((m, mmr, r, urr, u) => urr.UserId == u.Id)
                .Select((m) => m)
                .ToList();
            return _mapper.Map<List<MenuRes>>(res);
        }
        public UserRes Register(string userName, string nickName, string password, ref string msg)
        {
            // 判断用户是否存在
            if (_db.Queryable<Users>().Any(u => u.Name == userName))
            {
                msg = $"用户名[{userName}]已存在！";
                return new UserRes();
            }
            Users user = new Users()
            {
                Name = userName,
                NickName = nickName,
                Password = password,
                UserType = 1,
                IsEnable = true,
                Description = "用户注册",
                CreateDate = DateTime.Now,
                CreateUserId = 0,
            };
            return _mapper.Map<UserRes>(_db.Insertable(user).ExecuteReturnEntity());
        }

    }
}