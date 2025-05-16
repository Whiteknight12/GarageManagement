using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class TienCong
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string TenLoaiTienCong { get; set; }
        public double DonGiaLoaiTienCong { get; set; }
        public Guid NoiDungSuaChuaId { get; set; }
    }
}
