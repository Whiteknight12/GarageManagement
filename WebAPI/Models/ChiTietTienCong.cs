using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class ChiTietTienCong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int? TienCongID { get; set; }
        public int? NoiDungPhieuSuaChuaID {  get; set; }
    }
}
