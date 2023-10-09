using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Interface;
using SqlSugar;

namespace Service
{
    public class MenuService:IMenuService
    {
        private readonly ISqlSugarClient _db;
        private readonly IMapper _mapper;
        public MenuService(ISqlSugarClient db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        
    }
}
