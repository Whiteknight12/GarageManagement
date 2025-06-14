using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class LichSu
    {
        public Guid Id { get; set; } 
        public DateTime ThoiDiemThucHien { get; set; }
        public Guid ThucTheLienQuanId { get; set; }
        public string LoaiThucTheLienQuan { get; set; }
        public string NoiDung { get; set; }
        public string HanhDong { get; set; }
    }
}
