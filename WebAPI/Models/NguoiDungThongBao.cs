using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class NguoiDungThongBao
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid thongBaoId { get; set; }

        public Guid nguoiDungId { get; set; }
    }
}
