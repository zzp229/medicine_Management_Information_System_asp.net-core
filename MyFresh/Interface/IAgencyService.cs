using Model.Dto.Menu;
using Model.Entitys;
using Model.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IAgencyService
    {
        bool Add(Agency req);
        bool Edit(MenuEdit req, long userId);
        bool Del(long id);
        bool Batch(string ids);
        PageInfo GetAgencys(Agency req);
    }
}
