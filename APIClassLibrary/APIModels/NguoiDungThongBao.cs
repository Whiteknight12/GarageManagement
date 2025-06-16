using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class NguoiDungThongBao
    {
        public Guid Id { get; set; }

        public Guid thongBaoId { get; set; }

        public Guid nguoiDungId { get; set; }
    }
}
