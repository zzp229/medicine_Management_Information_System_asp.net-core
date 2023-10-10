using Model.Dto.Menu;
using Model.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IMenuService
    {
        bool Add(MenuAdd req, long userId);
        bool Edit(MenuEdit req, long userId);
        bool Del(long id);
        bool Batch(string ids);
        PageInfo GetMenus(MenuReq req);
    }
}
