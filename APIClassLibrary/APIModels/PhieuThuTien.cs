using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class PhieuThuTien
    {
        public Guid Id { get; set; }
        public Guid KhachHangId { get; set; }
        public Guid XeId { get; set; }
        public string Email { get; set; }
        public DateTime NgayThuTien { get; set; }
        public double SoTienThu { get; set; }
    }
}
