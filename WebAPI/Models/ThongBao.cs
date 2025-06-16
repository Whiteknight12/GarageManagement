using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class ThongBao
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? Content { get; set; }

        public Guid? NhomNguoiDungId { get; set; }

        public bool? isForAll { get; set; }

        public DateTime taoVaoLuc {  get; set; }

        public Guid xeId { get; set; }
    }
}
