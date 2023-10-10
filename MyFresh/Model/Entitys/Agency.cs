using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entitys
{
    public class Agency
    {
        [SugarColumn(IsNullable = false)]
        public string Ano { get; set; }
        [SugarColumn(IsNullable = false)]
        public string Aname { get; set; }
    }
}
