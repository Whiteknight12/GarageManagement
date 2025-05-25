using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class PhieuNhapVatTu
    {
        public Guid Id { get; set; }
        public DateTime NgayNhap { get; set; }
        public double TongTien { get; set; }

        public int STT { get; set; }
        public bool IsSelected { get; set; } = false;
    }
}
