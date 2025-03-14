using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class Car
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Model { get; set; }
        public string? BienSo { get; set; }
        public string? TenChuXe { get; set; }
        public string? DienThoai { get; set; }
        public string? DiaChi { get; set; }
        public bool? IsAvailable { get; set; }
        public double? TienNoCuaChuXe { get; set; }
    }
}
