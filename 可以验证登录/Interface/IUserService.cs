using Model.Dto.Menu;
using Model.Dto.User;

namespace Interface
{
    public interface IUserService
    {
        UserRes GetUser(string userName, string password);
        List<MenuRes> GetUserMenus(long id);
        UserRes Register(string userName, string nickName, string password, ref string msg);
    }
}