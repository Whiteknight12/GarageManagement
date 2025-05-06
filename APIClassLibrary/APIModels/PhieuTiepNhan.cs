using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class PhieuTiepNhan
    {
        public Guid Id { get; set; } 
        public DateTime? NgayTiepNhan { get; set; }
        //khoa ngoai 
        public Guid XeId { get; set; }
    }
}
