using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Interface;
using Model.Dto.Menu;
using Model.Entitys;
using Model.Other;
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
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool Add(MenuAdd req, long userId)
        {
            Menu info = _mapper.Map<Menu>(req);
            info.CreateDate = DateTime.Now;
            info.CreateUserId = userId;
            info.IsDeleted = 0;
            return _db.Insertable(info).ExecuteCommand() > 0;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool Edit(MenuEdit req, long userId)
        {
            var info = _db.Queryable<Menu>().First(p => p.Id == req.Id);
            if (info != null)
            {
                _mapper.Map(req, info);
                info.ModifyDate = DateTime.Now;
                info.ModifyUserId = userId;
                return _db.Updateable(info).ExecuteCommand() > 0;
            }
            return false;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Del(long id)
        {
            var info = _db.Queryable<Menu>().First(p => p.Id == id);
            if (info != null)
            {
                return _db.Deleteable(info).ExecuteCommand() > 0;
            }
            return false;
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool Batch(string ids)
        {
            return _db.Ado.ExecuteCommand($"DELETE Menu WHERE Id IN({ids})") > 0;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public PageInfo GetMenus(MenuReq req)
        {
            PageInfo pageInfo = new PageInfo();
            int total = 0;
            //test
            //var res1 = _db.Queryable<Menu>().
            //    Select((u) => new MenuRes()
            //    {
            //        Id = u.Id
            //    ,
            //        Name = u.Name
            //    ,
            //        Index = u.Index
            //    ,
            //        Order = u.Order
            //    ,
            //        IsEnable = u.IsEnable
            //    ,
            //        Description = u.Description
            //    ,
            //        CreateDate = u.CreateDate
            //    }).ToPageList(req.PageIndex, req.PageSize, ref total);
            //var res2 = _db.Queryable<Menu>().ToList();
            var res = _db.Queryable<Menu>()
            .WhereIF(!string.IsNullOrEmpty(req.Name), u => u.Name.Contains(req.Name))
            .WhereIF(!string.IsNullOrEmpty(req.Index), u => u.Name.Contains(req.Index))
            .WhereIF(!string.IsNullOrEmpty(req.Description), u => u.Name.Contains(req.Description))
            .OrderBy((u) => u.Order)
            .Select((u) => new MenuRes()
            {
                Id = u.Id
                ,
                Name = u.Name
                ,
                Index = u.Index
                ,
                Order = u.Order
                ,
                IsEnable = u.IsEnable
                ,
                Description = u.Description
                ,
                CreateDate = u.CreateDate
            }).ToPageList(req.PageIndex, req.PageSize, ref total);
            pageInfo.Data = res;    //数据库获取的数据
            pageInfo.Total = total;
            // 手工分页
            // var res = exp.Skip((req.PageIndex - 1) * req.PageSize)
            // .Take(req.PageSize)
            // .ToList();
            return pageInfo;
        }
    }
}
