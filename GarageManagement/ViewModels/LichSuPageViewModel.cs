using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class LichSuPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<LichSu> lichSuList= new ObservableCollection<LichSu>();

        [ObservableProperty]
        private DateTime tuNgay = DateTime.UtcNow.ToLocalTime();

        [ObservableProperty]
        private DateTime denNgay = DateTime.UtcNow.ToLocalTime();

        [ObservableProperty]
        private string selectedAction;

        [ObservableProperty]
        private ObservableCollection<string> listHanhDong= new ObservableCollection<string>();

        [ObservableProperty]
        private string selectedDataType;

        [ObservableProperty]
        private ObservableCollection<string> listLoaiDuLieu=new ObservableCollection<string>();

        [ObservableProperty]
        private Guid idValue;

        private readonly APIClientService<LichSu> _lichSuService;
        private readonly APIClientService<Xe> _xeService;
        private readonly APIClientService<PhieuTiepNhan> _phieuTiepNhanService;
        private readonly APIClientService<ThamSo> _thamSoService;
        private readonly APIClientService<HieuXe> _hieuXeService;
        private readonly APIClientService<TienCong> _tienCongService;
        private readonly APIClientService<PhieuSuaChua> _phieuSuaChuaService;
        private readonly APIClientService<ChiTietPhieuSuaChua> _chiTietPhieuSuaChuaService;
        private readonly APIClientService<VatTuPhuTung> _vatTuPhuTungService;
        private readonly APIClientService<KhachHang> _khachHangService;
        private readonly APIClientService<PhieuThuTien> _phieuThuTienService;
        private readonly APIClientService<PhanQuyen> _phanQuyenService;
        private readonly APIClientService<NhomNguoiDung> _nhomNguoiDungService;
        private readonly APIClientService<ChucNang> _chucNangService;
        private readonly APIClientService<TaiKhoan> _taiKhoanService;
        private readonly APIClientService<NhanVien> _nhanVienService;
        private readonly APIClientService<PhieuNhapVatTu> _phieuNhapVatTuService;
        private readonly APIClientService<ChiTietPhieuNhapVatTu> _chiTietPhieuNhapVatTuService;
        private readonly APIClientService<BaoCaoDoanhThuThang> _baoCaoDoanhThuThangService;
        private readonly APIClientService<ChiTietBaoCaoDoanhThuThang> _chiTietBaoCaoDoanhThuThangService;
        private readonly APIClientService<BaoCaoTon> _baoCaoTonService;
        private readonly APIClientService<ChiTietBaoCaoTon> _chiTietBaoCaoTonService;
        private readonly APIClientService<NoiDungSuaChua> _noiDungSuaChuaService;
        private readonly APIClientService<VTPTChiTietPhieuSuaChua> _vtptChiTietPhieuSuaChuaService;
        private readonly APIClientService<ThongBao> _thongBaoService;
        private readonly APIClientService<NguoiDungThongBao> _nguoiDungThongBaoService;



        public LichSuPageViewModel(
        APIClientService<LichSu> lichSuService,
        APIClientService<Xe> xeService,
        APIClientService<PhieuTiepNhan> phieuTiepNhanService,
        APIClientService<ThamSo> thamSoService,
        APIClientService<HieuXe> hieuXeService,
        APIClientService<TienCong> tienCongService,
        APIClientService<PhieuSuaChua> phieuSuaChuaService,
        APIClientService<ChiTietPhieuSuaChua> chiTietPhieuSuaChuaService,
        APIClientService<VatTuPhuTung> vatTuPhuTungService,
        APIClientService<KhachHang> khachHangService,
        APIClientService<PhieuThuTien> phieuThuTienService,
        APIClientService<PhanQuyen> phanQuyenService,
        APIClientService<NhomNguoiDung> nhomNguoiDungService,
        APIClientService<ChucNang> chucNangService,
        APIClientService<TaiKhoan> taiKhoanService,
        APIClientService<NhanVien> nhanVienService,
        APIClientService<PhieuNhapVatTu> phieuNhapVatTuService,
        APIClientService<ChiTietPhieuNhapVatTu> chiTietPhieuNhapVatTuService,
        APIClientService<BaoCaoDoanhThuThang> baoCaoDoanhThuThangService,
        APIClientService<ChiTietBaoCaoDoanhThuThang> chiTietBaoCaoDoanhThuThangService,
        APIClientService<BaoCaoTon> baoCaoTonService,
        APIClientService<ChiTietBaoCaoTon> chiTietBaoCaoTonService,
        APIClientService<NoiDungSuaChua> noiDungSuaChuaService,
        APIClientService<VTPTChiTietPhieuSuaChua> vtptChiTietPhieuSuaChuaService,
        APIClientService<ThongBao> thongBaoService,
        APIClientService<NguoiDungThongBao> nguoiDungThongBaoService
    )
        {
            _lichSuService = lichSuService;
            _xeService = xeService;
            _phieuTiepNhanService = phieuTiepNhanService;
            _thamSoService = thamSoService;
            _hieuXeService = hieuXeService;
            _tienCongService = tienCongService;
            _phieuSuaChuaService = phieuSuaChuaService;
            _chiTietPhieuSuaChuaService = chiTietPhieuSuaChuaService;
            _vatTuPhuTungService = vatTuPhuTungService;
            _khachHangService = khachHangService;
            _phieuThuTienService = phieuThuTienService;
            _phanQuyenService = phanQuyenService;
            _nhomNguoiDungService = nhomNguoiDungService;
            _chucNangService = chucNangService;
            _taiKhoanService = taiKhoanService;
            _nhanVienService = nhanVienService;
            _phieuNhapVatTuService = phieuNhapVatTuService;
            _chiTietPhieuNhapVatTuService = chiTietPhieuNhapVatTuService;
            _baoCaoDoanhThuThangService = baoCaoDoanhThuThangService;
            _chiTietBaoCaoDoanhThuThangService = chiTietBaoCaoDoanhThuThangService;
            _baoCaoTonService = baoCaoTonService;
            _chiTietBaoCaoTonService = chiTietBaoCaoTonService;
            _noiDungSuaChuaService = noiDungSuaChuaService;
            _vtptChiTietPhieuSuaChuaService = vtptChiTietPhieuSuaChuaService;
            _thongBaoService = thongBaoService;
            _nguoiDungThongBaoService = nguoiDungThongBaoService;
        }

        public async Task LoadAsync()
        {
            var lichSu = await _lichSuService.GetAll();
            if (lichSu is not null) LichSuList = new ObservableCollection<LichSu>(lichSu.OrderByDescending(ls => ls.ThoiDiemThucHien));
            ListHanhDong = new ObservableCollection<string>
            {
                "Tạo mới",
                "Cập nhật",
                "Xóa",
                "Tất cả"
            };
            ListLoaiDuLieu = new ObservableCollection<string>
            { 
                "BaoCaoDoanhThuThang",
                "BaoCaoTon",
                "ChiTietBaoCaoDoanhThuThang",
                "ChiTietBaoCaoTon",
                "ChiTietPhieuNhapVatTu",
                "ChiTietPhieuSuaChua",
                "ChucNang",
                "HieuXe",
                "KhachHang",
                "Tất cả",
                "NhanVien",
                "NhomNguoiDung",
                "NoiDungSuaChua",
                "PhanQuyen",
                "PhieuNhapVatTu",
                "PhieuSuaChua",
                "PhieuThuTien",
                "PhieuTiepNhan",
                "TaiKhoan",
                "ThamSo",
                "TienCong",
                "VatTuPhuTung",
                "VTPTChiTietPhieuSuaChua",
                "Xe"
            };
        }

        [RelayCommand]
        private async Task Filter()
        {
            var lichSu = await _lichSuService.GetAll();
            if (lichSu is not null) LichSuList = new ObservableCollection<LichSu>(lichSu.OrderByDescending(ls => ls.ThoiDiemThucHien));

            var queryResults = LichSuList.ToList();

            DateTime fromDate = TuNgay.Date;
            DateTime toDate = DenNgay != default ? DenNgay.Date.AddDays(1).AddTicks(-1) : DateTime.Now;

            var timeFiltered = LichSuList
                .Where(u => u.ThoiDiemThucHien >= fromDate && u.ThoiDiemThucHien <= toDate)
                .ToList();

            queryResults = timeFiltered;

            if (!string.IsNullOrEmpty(SelectedAction) && SelectedAction != "Tất cả")
            {
                queryResults = queryResults
                    .Where(ls => ls.HanhDong == SelectedAction)
                    .ToList();
            }

            if (!string.IsNullOrEmpty(SelectedDataType) && SelectedDataType!="Tất cả")
            {
                queryResults = queryResults
                    .Where(ls => ls.LoaiThucTheLienQuan == SelectedDataType)
                    .ToList();
            }

            if (IdValue != Guid.Empty)
            {
                queryResults = queryResults
                    .Where(ls => ls.ThucTheLienQuanId==IdValue)
                    .ToList();
            }

            LichSuList = new ObservableCollection<LichSu>(queryResults.OrderByDescending(x => x.ThoiDiemThucHien));
        }

        [RelayCommand]
        private async Task Rollback(LichSu lichSu)
        {
            if (lichSu == null || string.IsNullOrEmpty(lichSu.LoaiThucTheLienQuan) || string.IsNullOrEmpty(lichSu.NoiDung))
                return;

            // Tìm type thực thể
            var entityType = Type.GetType($"APIClassLibrary.APIModels.{lichSu.LoaiThucTheLienQuan}, APIClassLibrary");
            if (entityType == null)
            {
                await Shell.Current.DisplayAlert("Lỗi", "Không tìm thấy loại thực thể.", "OK");
                return;
            }

            // Deserialize dữ liệu
            var entity = JsonSerializer.Deserialize(lichSu.NoiDung, entityType);
            if (entity == null)
            {
                await Shell.Current.DisplayAlert("Lỗi", "Không phân tích được nội dung.", "OK");
                return;
            }

            // Lấy đúng DI tương ứng
            object? service = lichSu.LoaiThucTheLienQuan switch
            {
                nameof(Xe) => _xeService,
                nameof(PhieuTiepNhan) => _phieuTiepNhanService,
                nameof(ThamSo) => _thamSoService,
                nameof(HieuXe) => _hieuXeService,
                nameof(TienCong) => _tienCongService,
                nameof(PhieuSuaChua) => _phieuSuaChuaService,
                nameof(ChiTietPhieuSuaChua) => _chiTietPhieuSuaChuaService,
                nameof(VatTuPhuTung) => _vatTuPhuTungService,
                nameof(KhachHang) => _khachHangService,
                nameof(PhieuThuTien) => _phieuThuTienService,
                nameof(PhanQuyen) => _phanQuyenService,
                nameof(NhomNguoiDung) => _nhomNguoiDungService,
                nameof(ChucNang) => _chucNangService,
                nameof(TaiKhoan) => _taiKhoanService,
                nameof(NhanVien) => _nhanVienService,
                nameof(PhieuNhapVatTu) => _phieuNhapVatTuService,
                nameof(ChiTietPhieuNhapVatTu) => _chiTietPhieuNhapVatTuService,
                nameof(BaoCaoDoanhThuThang) => _baoCaoDoanhThuThangService,
                nameof(ChiTietBaoCaoDoanhThuThang) => _chiTietBaoCaoDoanhThuThangService,
                nameof(BaoCaoTon) => _baoCaoTonService,
                nameof(ChiTietBaoCaoTon) => _chiTietBaoCaoTonService,
                nameof(NoiDungSuaChua) => _noiDungSuaChuaService,
                nameof(VTPTChiTietPhieuSuaChua) => _vtptChiTietPhieuSuaChuaService,
                nameof(ThongBao) => _thongBaoService,
                nameof(NguoiDungThongBao) => _nguoiDungThongBaoService,
                _ => null
            };

            if (service == null)
            {
                await Shell.Current.DisplayAlert("Lỗi", "Không tìm thấy service phù hợp.", "OK");
                return;
            }

            // Xác định hành động rollback
            string methodName = lichSu.HanhDong switch
            {
                "Xóa" => "Create",         // rollback xóa = tạo lại
                "Cập nhật" => "Update",    // rollback cập nhật = cập nhật lại
                "Tạo mới" => "Delete",     // rollback tạo mới = xóa đi
                _ => ""
            };

            if (string.IsNullOrEmpty(methodName))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Hành động không hợp lệ.", "OK");
                return;
            }

            // Gọi method tương ứng
            var method = service.GetType().GetMethod(methodName);
            if (method == null)
            {
                await Shell.Current.DisplayAlert("Lỗi", $"Không tìm thấy phương thức {methodName}.", "OK");
                return;
            }

            object[] parameters = methodName == "Delete"
                ? new object[] { lichSu.ThucTheLienQuanId }
                : new object[] { entity };

            var result = method.Invoke(service, parameters);

            if (result is Task task)
                await task;

            await Shell.Current.DisplayAlert("Thành công", "Rollback hoàn tất.", "OK");
            await Filter();
        }
    }
}
