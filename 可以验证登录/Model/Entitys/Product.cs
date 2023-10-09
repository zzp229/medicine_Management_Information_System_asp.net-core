using Model.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entitys
{

    public class Product : IEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Name { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public decimal Price { get; set; }
        /// <summary>
        /// 图片
        /// </summary>

        [SugarColumn(IsNullable = false)]
        public string Image { get; set; }
        /// <summary>
        /// 状态 （0=下架，1=上架）
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int Status { get; set; }
    }
}
