using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    class ChiTietPhieuNhapVatTu
    {
        public Guid PhieuNhapId { get; set; }
        public Guid VatTuId { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }
    }
}
