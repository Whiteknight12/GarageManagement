using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class ChiTietPhieuSuaChua
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PhieuSuaChuaId { get; set; }
        public Guid TienCongId { get; set; }
        public double TienCongApDung { get; set; }
        public Guid NoiDungSuaChuaId { get; set; }

        public double ThanhTien {  get; set; }
    }
}   
