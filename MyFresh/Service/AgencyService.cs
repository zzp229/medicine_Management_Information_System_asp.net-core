using AutoMapper;
using Interface;
using Model.Dto.Agency;
using Model.Dto.Menu;
using Model.Dto.User;
using Model.Entitys;
using Model.Other;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AgencyService : IAgencyService
    {
        private readonly ISqlSugarClient _db;
        private readonly IMapper _mapper;
        public AgencyService(ISqlSugarClient db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public bool Add(Agency req)
        {
            return _db.Insertable(req).ExecuteCommand() > 0;
        }

        public bool Batch(string ids)
        {
            throw new NotImplementedException();
        }

        public bool Del(long id)
        {
            throw new NotImplementedException();
        }

        public bool Edit(MenuEdit req, long userId)
        {
            throw new NotImplementedException();
        }

        public PageInfo GetAgencys(MenuReq req)
        {
            PageInfo pageInfo = new PageInfo();
            int total = 0;
            var res = _db.Queryable<Agency>();
            pageInfo.Data = res;    //数据库获取的数据
            pageInfo.Total = total;
            // 手工分页
            // var res = exp.Skip((req.PageIndex - 1) * req.PageSize)
            // .Take(req.PageSize)
            // .ToList();
            return pageInfo;
        }

        public PageInfo GetAgencys(Agency req)
        {
            PageInfo pageInfo= new PageInfo();
            int total = 1;
            var res = _db.Queryable<Agency>().ToList();
            pageInfo.Data = res;
            pageInfo.Total = total;
            return pageInfo;
        }
    }
}
