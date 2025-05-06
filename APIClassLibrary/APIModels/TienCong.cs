using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class TienCong
    {
        public Guid Id { get; set; }
        public string TenLoaiTienCong { get; set; }
        public double DonGiaLoaiTienCong { get; set; }
    }
}
