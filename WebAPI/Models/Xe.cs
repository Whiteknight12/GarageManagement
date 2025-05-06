using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Xe
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public string Ten { get; set; }
        public Guid HieuXeId { get; set; }
        public string BienSo { get; set; }
        //car owner
        public Guid KhachHangId { get; set; }
        public bool? KhaDung { get; set; }
        public double? TienNo { get; set; }
    }
}
