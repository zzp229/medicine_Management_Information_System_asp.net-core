using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto.Product
{
    public class ProductRes
    {
        public long Id { get; set; }
        public string Name { get; set; } 
        public decimal Price { get; set; }
        public string Image { get; set; } 
        public string Description { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifyUserId { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int IsDeleted { get; set; }
    }
}
