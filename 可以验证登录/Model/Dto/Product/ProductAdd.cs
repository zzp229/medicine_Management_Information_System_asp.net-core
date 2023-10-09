﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto.Product
{
    public class ProductAdd
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
    }
}
