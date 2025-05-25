using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class PhieuSuaChua
    {
        public Guid Id { get; set; } 
        public Guid XeId { get; set; }
        public string TenXe { get; set; }
        public string BienSoXe { get; set; }
        public DateTime NgaySuaChua { get; set; }
        public double TongTien { get; set; }

        public int STT { get; set; }
        public bool IsSelected { get; set; } = false; 
    }
}
