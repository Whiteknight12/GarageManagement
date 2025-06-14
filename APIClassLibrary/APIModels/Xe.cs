namespace APIClassLibrary.APIModels
{
    public class Xe
    {
        public Guid Id { get; set; } 
        public string Ten { get; set; }
        public Guid HieuXeId { get; set; }
        public string BienSo { get; set; }
        //car owner
        public Guid KhachHangId { get; set; }
        public bool? KhaDung { get; set; }
        public double? TienNo { get; set; }
        public string? ImageUrl { get; set; }

        //these fields are used for UI purposes only
        //we should separate them from the model, can use DTOs or ViewModels
        public string? TenHieuXe { get; set; }
        public string? TinhTrang { get; set; }  
        public List<PhieuSuaChua>? LichSuSuaChuaList { get; set; } = new List<PhieuSuaChua>();
        public List<PhieuTiepNhan>? LichSuTiepNhanList { get; set; } = new List<PhieuTiepNhan>();
    }
}
