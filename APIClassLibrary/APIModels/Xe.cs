using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class Xe
    {
        public Guid Id { get; set; } 
        public string Ten { get; set; }
        public Guid HieuXeId { get; set; }
        public string BienSo { get; set; }
        //car owner
        public Guid KhachHangId { get; set; }
        public bool? KhaDung { get; set; }
        public double? TienNo { get; set; }
        public string? ImageUrl { get; set; }

        public string TenHieuXe { get; set; }

        public string TenChuXe { get; set; }
        public string CCCDChuXe { get; set; }
    }
}
