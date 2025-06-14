using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class BaoCaoTon
    {
        [Key]
        public Guid Id { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
    }
}
