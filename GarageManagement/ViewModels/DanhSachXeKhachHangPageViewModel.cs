using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using GarageManagement.ViewModels.EntityViewModels;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace GarageManagement.ViewModels
{
    public partial class DanhSachXeKhachHangPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<XeKhachHangViewModel> xeCuaToiList = new ObservableCollection<XeKhachHangViewModel>();

        private readonly APIClientService<Xe> _xeService;
        private readonly APIClientService<KhachHang> _khachHangService;
        private readonly APIClientService<PhieuSuaChua> _phieuSuaChuaService;
        private readonly APIClientService<HieuXe> _hieuXeService;
        private readonly APIClientService<PhieuTiepNhan> _phieuTiepNhanService;

        public DanhSachXeKhachHangPageViewModel(
            APIClientService<Xe> xeService,
            APIClientService<KhachHang> khachHangService,
            APIClientService<PhieuSuaChua> phieuSuaChuaService,
            APIClientService<HieuXe> hieuXeService,
            APIClientService<PhieuTiepNhan> phieuTiepNhanService)
        {
            _xeService = xeService;
            _khachHangService = khachHangService;
            _phieuSuaChuaService = phieuSuaChuaService;
            _hieuXeService = hieuXeService;
            _phieuTiepNhanService = phieuTiepNhanService;
        }

        public async Task LoadAsync()
        {
            var session = await GetSessionFromSecureStorage();
            if (session == null) return;
            var khachHang=await _khachHangService.GetThroughtSpecialRoute($"account-id/{session.AccountId}");
            if (khachHang == null) return;
            var xeList= await _xeService.GetListOnSpecialRequirement($"customer-id/{khachHang.Id}");
            if (xeList is null) return;
            foreach (var xe in xeList)
            {
                var hieuXe = await _hieuXeService.GetByID(xe.HieuXeId);
                xe.TenHieuXe = hieuXe?.TenHieuXe ?? "Chưa xác định";
                var phieuTiepNhan = await _phieuTiepNhanService.GetListOnSpecialRequirement($"XeId/{xe.Id}");
                if (phieuTiepNhan is null || phieuTiepNhan.Count == 0) xe.TinhTrang = "Xe chưa được tiếp nhận lần nào";
                else
                {
                    var phieuGanNhat = phieuTiepNhan
                        .Where(p => p.NgayTiepNhan.HasValue)
                        .OrderByDescending(p => p.NgayTiepNhan)
                        .FirstOrDefault();
                    if (phieuGanNhat is not null)
                    {
                        if (phieuGanNhat.DaHoanThanhBaoTri) xe.TinhTrang = "Đã hoàn thành bảo trì";
                        else xe.TinhTrang = "Xe đang tiếp nhận và bảo trì trong Gara";
                    }
                    else xe.TinhTrang = "Xe chưa được tiếp nhận lần nào";
                }
                var lichSuSuaChua = await _phieuSuaChuaService.GetListOnSpecialRequirement($"GetListByBienSoXe/{xe.BienSo}");
                if (lichSuSuaChua is not null) xe.LichSuSuaChuaList = lichSuSuaChua;
                var lishSuTiepNhan = await _phieuTiepNhanService.GetListOnSpecialRequirement($"XeId/{xe.Id}");
                if (lishSuTiepNhan is not null)
                {
                    foreach (var phieu in lishSuTiepNhan)
                    {
                        if (phieu.DaHoanThanhBaoTri) phieu.TinhTrang = "Đã hoàn thành bảo trì";
                        else phieu.TinhTrang = "Đang tiếp nhận và bảo trì trong Gara";
                    }
                    xe.LichSuTiepNhanList = lishSuTiepNhan;
                }
            }
            XeCuaToiList = new ObservableCollection<XeKhachHangViewModel>(
                xeList.Select(xe => new XeKhachHangViewModel(xe))
            );
        }

        private async Task<taiKhoanSession?> GetSessionFromSecureStorage()
        {
            try
            {
                var json = await SecureStorage.Default.GetAsync("user-account-status");
                if (!string.IsNullOrWhiteSpace(json))
                {
                    return JsonSerializer.Deserialize<taiKhoanSession>(json);
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi truy xuất SecureStorage: " + ex.Message);
                return null;
            }
        }
    }
}
