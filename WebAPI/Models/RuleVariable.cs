using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("RULEVARIABLE")]
    public class RuleVariable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        [Column("SOXETIEPNHANTOIDAMOTNGAY")]
        public int SoXeTiepNhanToiDaMotNgay {  get; set; }
    }
}
