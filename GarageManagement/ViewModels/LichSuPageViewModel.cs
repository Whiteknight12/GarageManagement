using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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

        public LichSuPageViewModel(APIClientService<LichSu> lichSuService)
        {
            _lichSuService= lichSuService;
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
    }
}
