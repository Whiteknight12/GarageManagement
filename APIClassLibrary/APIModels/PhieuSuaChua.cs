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
        public DateTime NgaySuaChua { get; set; }
        public double TongTien { get; set; }
    }
}
