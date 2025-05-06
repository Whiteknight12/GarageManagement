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
        public int GiaTri {  get; set; }
    }
}
