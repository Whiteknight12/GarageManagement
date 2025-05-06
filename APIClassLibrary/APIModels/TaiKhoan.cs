using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class TaiKhoan
    {
        public Guid Id { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public Guid NguoiDungId { get; set; }
    }
}
