using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class LichSu
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime ThoiDiemThucHien { get; set; } = DateTime.Now;
        public Guid ThucTheLienQuanId { get; set; }
        public string LoaiThucTheLienQuan { get; set; } 
        public string NoiDung { get; set; } 
        public string HanhDong { get; set; } 
    }
}
