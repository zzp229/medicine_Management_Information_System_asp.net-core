using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto.Order
{
    public class OrderRes
    {
        // Title 使用NPOI导入导出时会用到的特性
        [Title(Titile = "Id")]
        public long Id { get; set; }
        [Title(Titile = "OrderNumber")]
        public string OrderNumber { get; set; }
        [Title(Titile = "Price")]
        public decimal Price { get; set; }
        [Title(Titile = "ProductId")]
        public string ProductId { get; set; }
        [Title(Titile = "Remark")]
        public string Remark { get; set; }
        [Title(Titile = "Description")]
        public string Description { get; set; }
        [Title(Titile = "CreateDate")]
        public DateTime CreateDate { get; set; } 
    }
}
