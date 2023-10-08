using NPOI.HSSF.UserModel;

namespace Common
{
    /// <summary>
    /// 使用NPOI导入导出使用
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)] 
    public class TitleAttribute : Attribute
    {
        public string Titile;

        public HSSFCellStyle CellType;
    }
}