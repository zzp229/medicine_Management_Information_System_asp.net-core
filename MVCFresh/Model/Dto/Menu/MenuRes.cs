using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto.Menu
{
    public class MenuRes
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Index { get; set; }
        public int Order { get; set; }
        public bool IsEnable { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
