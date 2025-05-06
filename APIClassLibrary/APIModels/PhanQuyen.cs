using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class PhanQuyen
    {
        public Guid Id { get; set; } 
        public Guid NhomNguoiDungId { get; set; }
        public Guid ChucNangId { get; set; }
    }
}
