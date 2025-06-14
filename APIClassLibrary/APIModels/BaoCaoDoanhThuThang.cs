using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class BaoCaoDoanhThuThang
    {
        public Guid Id { get; set; }
        public int Thang { get; set; }
        public double TongDoanhThu { get; set; }
        public int IdForUI { get; set; }
        public bool IsSelected { get; set; }
    }
}
