using Model.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entitys
{
    public class OrderInfo : IEntity
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string OrderNumber { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string ProductId { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public decimal Price { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
