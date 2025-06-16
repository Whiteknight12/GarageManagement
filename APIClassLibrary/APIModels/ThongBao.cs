
namespace APIClassLibrary.APIModels
{
    public class ThongBao
    {
        public Guid Id { get; set; } 

        public string? Content { get; set; }

        public Guid? NhomNguoiDungId { get; set; }

        public bool? isForAll { get; set; }

        public DateTime taoVaoLuc { get; set; }

        public Guid xeId { get; set; }
    }
}
