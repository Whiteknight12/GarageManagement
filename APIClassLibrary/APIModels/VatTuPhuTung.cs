using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class VatTuPhuTung
    {
        public Guid VatTuPhuTungId { get; set; } 
        public string TenLoaiVatTuPhuTung { get; set; }
        public int SoLuong { get; set; }
        public double DonGiaBanLoaiVatTuPhuTung { get; set; }
        public bool IsSelected { get; set; } = false;
    }
}
