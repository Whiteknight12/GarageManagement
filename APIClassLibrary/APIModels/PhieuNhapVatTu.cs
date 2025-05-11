using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    class PhieuNhapVatTu
    {
        public Guid Id { get; set; }
        public DateTime NgayNhap { get; set; }
        public double TongTien { get; set; }
    }
}
