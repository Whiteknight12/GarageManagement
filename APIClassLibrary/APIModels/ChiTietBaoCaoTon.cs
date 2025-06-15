using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class ChiTietBaoCaoTon
    {
        public Guid Id { get; set; }
        public Guid VatTuPhuTungId { get; set; }
        public Guid BaoCaoTonId { get; set; }
        public int TonDau { get; set; }
        public int PhatSinh { get; set; }
        public int TonCuoi { get; set; }

        //these fields are for UI display, should be put in viewModels or Dtos
        public string? TenLoaiVatTuPhuTung { get; set; }
    }
}
