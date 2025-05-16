using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class NoiDungSuaChua
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string TenNoiDungSuaChua { get; set; }
    }
}
