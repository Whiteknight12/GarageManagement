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
        //Email => trich xuat email tu khach hang  
        public DateTime NgayThuTien { get; set; }
        public double SoTienThu { get; set; } // tao bien ThamSo de check : so tien thu duoc phep vuot qua so tien dang no hay khong?  
                                              // default: so tien thu khong duoc phep vuot qua so tien dang no 
    }
}
