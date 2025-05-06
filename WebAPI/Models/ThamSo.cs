using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class ThamSo
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public int SoXeTiepNhanToiDaMotNgay {  get; set; }
    }
}
