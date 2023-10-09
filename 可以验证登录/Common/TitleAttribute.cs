using NPOI.HSSF.UserModel;

namespace Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TitleAttribute : Attribute
    {
        public string Titile;

        public HSSFCellStyle CellType;
    }
}