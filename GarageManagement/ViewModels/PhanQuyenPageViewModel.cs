using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Services;
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

        [ObservableProperty] private bool quanLyDsXePermission;
        [ObservableProperty] private bool quanLyDsHieuXePermission;
        [ObservableProperty] private bool quanLyDsVatTuPhuTungPermission;
        [ObservableProperty] private bool quanLyDsTienCongPermission;
        [ObservableProperty] private bool baoCaoDoanhSoPermission;
        [ObservableProperty] private bool tiepNhanXePermission;
        [ObservableProperty] private bool lapPhieuSuaChuaPermission;
        [ObservableProperty] private bool lapPhieuNhapPermission;
        [ObservableProperty] private bool lapPhieuThuTienPermission;
        [ObservableProperty] private bool phanQuyenPermission;
        [ObservableProperty] private bool thayDoiThamSoPermission;

        public static readonly Guid QuanLyDsXeId = Guid.Parse("C4499274-4040-484A-A2E0-8A720D20F746");
        public static readonly Guid QuanLyDsHieuXeId = Guid.Parse("56AE504B-B798-480D-8F85-4790C5B9CDA4");
        public static readonly Guid QuanLyDsVatTuPhuTungId = Guid.Parse("38A14419-703A-4AF2-AC20-FC20638651EA");
        public static readonly Guid QuanLyDsTienCongId = Guid.Parse("9AF76DFB-A2FF-4808-B19A-568696C8114F");
        public static readonly Guid BaoCaoDoanhSoId = Guid.Parse("307E83AD-C656-4F30-B46E-70C6B3864EEA");
        public static readonly Guid TiepNhanXeId = Guid.Parse("C325D903-549A-4DED-B93C-140C34233854");
        public static readonly Guid LapPhieuSuaChuaId = Guid.Parse("6405F669-38D7-48E4-B127-D5FAD358AB5D");
        public static readonly Guid LapPhieuNhapId = Guid.Parse("BABDD343-BCC0-4455-83D0-9522F9952CBC");
        public static readonly Guid LapPhieuThuTienId = Guid.Parse("F2B39EC8-F20F-45FF-88FB-579402B88AF6");
        public static readonly Guid PhanQuyenId = Guid.Parse("94521B9A-1BBB-48E1-B254-E7D76408EE69");
        public static readonly Guid ThayDoiThamSoId = Guid.Parse("BCB8F586-C5D8-456B-A01A-037E6B866FC6");

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

            // Tải danh sách chức năng đã được phân quyền cho vai trò này
            var allPhanQuyen = await _phanQuyenService.GetAll();
            var existingPermissions = allPhanQuyen.Where(p => p.NhomNguoiDungId == nhomNguoiDung.Id).ToList();
            ChucNangList.Clear();

            // Reset tất cả các quyền
            QuanLyDsXePermission = false;
            QuanLyDsHieuXePermission = false;
            QuanLyDsVatTuPhuTungPermission = false;
            QuanLyDsTienCongPermission = false;
            BaoCaoDoanhSoPermission = false;
            TiepNhanXePermission = false;
            LapPhieuSuaChuaPermission = false;
            LapPhieuNhapPermission = false;
            LapPhieuThuTienPermission = false;
            PhanQuyenPermission = false;
            ThayDoiThamSoPermission = false;

            // Cập nhật trạng thái quyền dựa trên dữ liệu từ DB
            foreach (var permission in existingPermissions)
            {
                var chucNangId = permission.ChucNangId;
                ChucNangList.Add(chucNangId);

                if (chucNangId == QuanLyDsXeId) QuanLyDsXePermission = true;
                else if (chucNangId == QuanLyDsHieuXeId) QuanLyDsHieuXePermission = true;
                else if (chucNangId == QuanLyDsVatTuPhuTungId) QuanLyDsVatTuPhuTungPermission = true;
                else if (chucNangId == QuanLyDsTienCongId) QuanLyDsTienCongPermission = true;
                else if (chucNangId == BaoCaoDoanhSoId) BaoCaoDoanhSoPermission = true;
                else if (chucNangId == TiepNhanXeId) TiepNhanXePermission = true;
                else if (chucNangId == LapPhieuSuaChuaId) LapPhieuSuaChuaPermission = true;
                else if (chucNangId == LapPhieuNhapId) LapPhieuNhapPermission = true;
                else if (chucNangId == LapPhieuThuTienId) LapPhieuThuTienPermission = true;
                else if (chucNangId == PhanQuyenId) PhanQuyenPermission = true;
                else if (chucNangId == ThayDoiThamSoId) ThayDoiThamSoPermission = true;
            }
        }
    }
}
