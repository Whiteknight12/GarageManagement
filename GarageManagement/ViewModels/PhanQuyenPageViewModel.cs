using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Services;
using Microsoft.Maui.Controls.Internals;
using System.Collections.ObjectModel;

namespace GarageManagement.ViewModels
{
    public partial class PhanQuyenPageViewModel : BaseViewModel
    {
        private readonly APIClientService<PhanQuyen> _phanQuyenService;
        private readonly APIClientService<NhomNguoiDung> _nhomNguoiDungService;
        private readonly AuthenticationService _authenticationService;
        private readonly APIClientService<ChucNang> _chucNangService; 
        [ObservableProperty]
        private ObservableCollection<PhanQuyen> phanQuyenList = new();
        [ObservableProperty]
        private ObservableCollection<NhomNguoiDung> roleList = new();
        [ObservableProperty]
        private ObservableCollection<Guid> chucNangList = new();
        [ObservableProperty]
        private ObservableCollection<ChucNang> allChucNangList = new();
        public NhomNguoiDung nhomNguoiDung { get; set; } = new NhomNguoiDung();

        public Guid QuanLiDanhSachXeId;
        [ObservableProperty] private bool quanLiDanhSachXePermission;

        public Guid QuanLiDanhSachHieuXeId;
        [ObservableProperty] private bool quanLiDanhSachHieuXePermission;

        public Guid QuanLiDanhSachLoaiVatTuId;
        [ObservableProperty] private bool quanLiDanhSachLoaiVatTuPermission;

        public Guid QuanLiDanhSachLoaiTienCongId;
        [ObservableProperty] private bool quanLiDanhSachLoaiTienCongPermission;

        public  Guid QuanLiNhanVienId;
        [ObservableProperty] private bool quanLiNhanVienPermission;

        public  Guid QuanLiKhachHangId;
        [ObservableProperty] private bool quanLiKhachHangPermission;

        public  Guid QuanLiPhieuNhapId;
        [ObservableProperty] private bool quanLiPhieuNhapPermission;

        public  Guid QuanLiPhieuSuaChuaId;
        [ObservableProperty] private bool quanLiPhieuSuaChuaPermission;

        public  Guid QuanLiPhieuThuTienId;
        [ObservableProperty] private bool quanLiPhieuThuTienPermission;

        public  Guid QuanLiPhieuTiepNhanId;
        [ObservableProperty] private bool quanLiPhieuTiepNhanPermission;

        public  Guid QuanLiBaoCaoThangId;
        [ObservableProperty] private bool quanLiBaoCaoThangPermission;

        public  Guid QuanLiBaoCaoTonId;
        [ObservableProperty] private bool quanLiBaoCaoTonPermission;

        public Guid TiepNhanXeId;
        [ObservableProperty] private bool tiepNhanXePermission;

        public Guid LapPhieuSuaChuaId;
        [ObservableProperty] private bool lapPhieuSuaChuaPermission;

        public Guid LapPhieuNhapId;
        [ObservableProperty] private bool lapPhieuNhapPermission;

        public Guid LapPhieuThuTienId;
        [ObservableProperty] private bool lapPhieuThuTienPermission;

        public Guid PhanQuyenId;
        [ObservableProperty] private bool phanQuyenPermission;

        public Guid ThayDoiThamSoId;
        [ObservableProperty] private bool thayDoiThamSoPermission;

        public Guid QuanLiDanhSachTaiKhoanId;
        [ObservableProperty] private bool quanLiDanhSachTaiKhoanPermission;

        public Guid QuanLiLichSuId;
        [ObservableProperty] private bool quanLiLichSuPermission;

        public Guid KhachHangXemDanhSachXeId;
        [ObservableProperty] private bool khachHangXemDanhSachXePermission;

        public PhanQuyenPageViewModel(APIClientService<PhanQuyen> phanQuyenService,
            APIClientService<NhomNguoiDung> nhomNguoiDungService,
            AuthenticationService authenticationService,
            APIClientService<ChucNang> chucNangService)
        {
            _phanQuyenService = phanQuyenService;
            _nhomNguoiDungService = nhomNguoiDungService;
            _authenticationService = authenticationService;
            _chucNangService = chucNangService; 
            _ = LoadAsync();
        }
        public async Task LoadAsync()
        {
            var a = await _chucNangService.GetThroughtSpecialRoute("name", "quan li danh sach xe"); 
            QuanLiDanhSachXeId = (a).Id;
            QuanLiDanhSachHieuXeId = (await _chucNangService.GetThroughtSpecialRoute("name", "quan li danh sach hieu xe")).Id;
            QuanLiDanhSachLoaiVatTuId = (await _chucNangService.GetThroughtSpecialRoute("name", "quan li danh sach loai vat tu")).Id;
            QuanLiDanhSachLoaiTienCongId = (await _chucNangService.GetThroughtSpecialRoute("name", "quan li danh sach loai tien cong")).Id;
            QuanLiNhanVienId = (await _chucNangService.GetThroughtSpecialRoute("name", "quan li nhan vien")).Id;
            QuanLiKhachHangId = (await _chucNangService.GetThroughtSpecialRoute("name", "quan li khach hang")).Id;
            QuanLiPhieuNhapId = (await _chucNangService.GetThroughtSpecialRoute("name", "quan li phieu nhap")).Id;
            QuanLiPhieuSuaChuaId = (await _chucNangService.GetThroughtSpecialRoute("name", "quan li phieu sua chua")).Id;
            QuanLiPhieuThuTienId = (await _chucNangService.GetThroughtSpecialRoute("name", "quan li phieu thu tien")).Id;
            QuanLiPhieuTiepNhanId = (await _chucNangService.GetThroughtSpecialRoute("name", "quan li phieu tiep nhan")).Id;
            QuanLiBaoCaoThangId = (await _chucNangService.GetThroughtSpecialRoute("name", "quan li bao cao thang")).Id;
            QuanLiBaoCaoTonId = (await _chucNangService.GetThroughtSpecialRoute("name", "quan li bao cao ton")).Id;
            TiepNhanXeId = (await _chucNangService.GetThroughtSpecialRoute("name", "tiep nhan xe")).Id;
            LapPhieuSuaChuaId = (await _chucNangService.GetThroughtSpecialRoute("name", "lap phieu sua chua")).Id;
            LapPhieuNhapId = (await _chucNangService.GetThroughtSpecialRoute("name", "lap phieu nhap")).Id;
            LapPhieuThuTienId = (await _chucNangService.GetThroughtSpecialRoute("name", "lap phieu thu tien")).Id;
            PhanQuyenId = (await _chucNangService.GetThroughtSpecialRoute("name", "phan quyen")).Id;
            ThayDoiThamSoId = (await _chucNangService.GetThroughtSpecialRoute("name", "thay doi tham so")).Id;
            QuanLiDanhSachTaiKhoanId = (await _chucNangService.GetThroughtSpecialRoute("name", "quan li danh sach tai khoan")).Id;
            QuanLiLichSuId = (await _chucNangService.GetThroughtSpecialRoute("name", "quan li lich su")).Id;
            KhachHangXemDanhSachXeId = (await _chucNangService.GetThroughtSpecialRoute("name", "khach hang xem danh sach xe")).Id;
            try
            {
                _ = _authenticationService.FettaiKhoanSession();
                var token = _authenticationService.GetCurrentAccountStatus.Token;
                var client1 = _nhomNguoiDungService.GetHttpClient;
                client1.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var roles = await _nhomNguoiDungService.GetAll();
                if (roles != null)
                {
                    RoleList = new ObservableCollection<NhomNguoiDung>(roles);
                }
                var client2 = _phanQuyenService.GetHttpClient;
                client2.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var pqList = await _phanQuyenService.GetAll();
                if (pqList != null)
                {
                    PhanQuyenList = new ObservableCollection<PhanQuyen>(pqList);
                }

                var client3 = _chucNangService.GetHttpClient;
                client3.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var cnl = await _chucNangService.GetAll();
                AllChucNangList = new ObservableCollection<ChucNang>(cnl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [RelayCommand]
        public async Task SavePermissions()
        {
            if (nhomNguoiDung.Id == Guid.Empty)
            {
                // Thông báo lỗi: Chưa chọn vai trò
                return;
            }

            // Lấy danh sách chức năng hiện có trong DB cho vai trò này
            var allPhanQuyen = await _phanQuyenService.GetAll();
            var existingPermissions = allPhanQuyen.Where(p => p.NhomNguoiDungId == nhomNguoiDung.Id).ToList();
            var existingChucNangIds = existingPermissions.Select(p => p.ChucNangId).ToList();

            // Xác định các chức năng cần xóa (có trong DB nhưng không có trong ChucNangList)
            var chucNangIdsToRemove = existingChucNangIds.Except(ChucNangList).ToList();
            var toRemove = existingPermissions.Where(pq => chucNangIdsToRemove.Contains(pq.ChucNangId));
             
            foreach (var phanQuyen in toRemove)
            {
                await _phanQuyenService.Delete(phanQuyen.Id);
            }

            // Xác định các chức năng cần thêm (có trong ChucNangList nhưng không có trong DB)
            var toAdd = ChucNangList.Except(existingChucNangIds).ToList();
            foreach (var chucNangId in toAdd)
            {
                await _phanQuyenService.Create(new PhanQuyen
                {
                    Id = Guid.NewGuid(),
                    NhomNguoiDungId = nhomNguoiDung.Id,
                    ChucNangId = chucNangId
                });
            }

            // Thông báo thành công (giả định)
        }

        public async Task OnVaiTroPicked()
        {
            if (nhomNguoiDung.Id == Guid.Empty) return;

            // Tải danh sách phân quyền hiện có cho vai trò này
            var allPhanQuyen = await _phanQuyenService.GetAll();
            var existingPermissions = allPhanQuyen
                .Where(p => p.NhomNguoiDungId == nhomNguoiDung.Id)
                .Select(p => p.ChucNangId)
                .ToList();

            ChucNangList.Clear();

            // Reset tất cả các quyền
            QuanLiDanhSachXePermission = false;
            QuanLiDanhSachHieuXePermission = false;
            QuanLiDanhSachLoaiVatTuPermission = false;
            QuanLiDanhSachLoaiTienCongPermission = false;
            QuanLiNhanVienPermission = false;
            QuanLiKhachHangPermission = false;
            QuanLiPhieuNhapPermission = false;
            QuanLiPhieuSuaChuaPermission = false;
            QuanLiPhieuThuTienPermission = false;
            QuanLiPhieuTiepNhanPermission = false;
            QuanLiBaoCaoThangPermission = false;
            QuanLiBaoCaoTonPermission = false;
            TiepNhanXePermission = false;
            LapPhieuSuaChuaPermission = false;
            LapPhieuNhapPermission = false;
            LapPhieuThuTienPermission = false;
            PhanQuyenPermission = false;
            ThayDoiThamSoPermission = false;
            QuanLiDanhSachTaiKhoanPermission = false;
            QuanLiLichSuPermission = false;
            KhachHangXemDanhSachXePermission = false;

            // Áp dụng các quyền đã có
            foreach (var chucNangId in existingPermissions)
            {
                ChucNangList.Add(chucNangId);

                if (chucNangId == QuanLiDanhSachXeId) QuanLiDanhSachXePermission = true;
                else if (chucNangId == QuanLiDanhSachHieuXeId) QuanLiDanhSachHieuXePermission = true;
                else if (chucNangId == QuanLiDanhSachLoaiVatTuId) QuanLiDanhSachLoaiVatTuPermission = true;
                else if (chucNangId == QuanLiDanhSachLoaiTienCongId) QuanLiDanhSachLoaiTienCongPermission = true;
                else if (chucNangId == QuanLiNhanVienId) QuanLiNhanVienPermission = true;
                else if (chucNangId == QuanLiKhachHangId) QuanLiKhachHangPermission = true;
                else if (chucNangId == QuanLiPhieuNhapId) QuanLiPhieuNhapPermission = true;
                else if (chucNangId == QuanLiPhieuSuaChuaId) QuanLiPhieuSuaChuaPermission = true;
                else if (chucNangId == QuanLiPhieuThuTienId) QuanLiPhieuThuTienPermission = true;
                else if (chucNangId == QuanLiPhieuTiepNhanId) QuanLiPhieuTiepNhanPermission = true;
                else if (chucNangId == QuanLiBaoCaoThangId) QuanLiBaoCaoThangPermission = true;
                else if (chucNangId == QuanLiBaoCaoTonId) QuanLiBaoCaoTonPermission = true;
                else if (chucNangId == TiepNhanXeId) TiepNhanXePermission = true;
                else if (chucNangId == LapPhieuSuaChuaId) LapPhieuSuaChuaPermission = true;
                else if (chucNangId == LapPhieuNhapId) LapPhieuNhapPermission = true;
                else if (chucNangId == LapPhieuThuTienId) LapPhieuThuTienPermission = true;
                else if (chucNangId == PhanQuyenId) PhanQuyenPermission = true;
                else if (chucNangId == ThayDoiThamSoId) ThayDoiThamSoPermission = true;
                else if (chucNangId == QuanLiDanhSachTaiKhoanId) QuanLiDanhSachTaiKhoanPermission = true;
                else if (chucNangId == QuanLiLichSuId) QuanLiLichSuPermission = true;
                else if (chucNangId == KhachHangXemDanhSachXeId) KhachHangXemDanhSachXePermission = true;
            }
        }

    }
}
