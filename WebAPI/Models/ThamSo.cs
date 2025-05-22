using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class ThamSo // vi du so xe tiep nhan 1 ngay,...
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string TenThamSo { get; set; }
        public double GiaTri {  get; set; }
        public string MoTa { get; set; }
    }
}
