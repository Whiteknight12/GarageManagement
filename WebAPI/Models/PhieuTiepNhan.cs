using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class PhieuTiepNhan
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime? NgayTiepNhan {  get; set; }
        //khoa ngoai 
        public Guid XeId { get; set; }
        public bool DaHoanThanhBaoTri { get; set; }  
    }
}
