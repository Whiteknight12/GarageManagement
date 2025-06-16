using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using GarageManagement.ViewModels;

public partial class ThemLoaiTienCongPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private string tenNoiDungSuaChua = string.Empty;

    [ObservableProperty]
    private string tenLoaiTienCong = string.Empty;

    [ObservableProperty]
    private string donGiaLoaiTienCong = string.Empty;

    private readonly APIClientService<TienCong> _tienCongService;
    private readonly APIClientService<NoiDungSuaChua> _noiDungService;

    public delegate void OnTienCongAddedDelegate(TienCong tienCong);
    public OnTienCongAddedDelegate OnTienCongAdded { get; set; }

    public ThemLoaiTienCongPageViewModel(
        APIClientService<TienCong> tienCongService,
        APIClientService<NoiDungSuaChua> noiDungService)
    {
        _tienCongService = tienCongService;
        _noiDungService = noiDungService;
    }

    [RelayCommand]
    private async Task Save()
    {
        // 1) Validate tất cả trường
        if (string.IsNullOrWhiteSpace(TenNoiDungSuaChua))
        {
            await Toast.Make("Vui lòng nhập tên nội dung sửa chữa",
                             CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
            return;
        }
        if (string.IsNullOrWhiteSpace(TenLoaiTienCong))
        {
            await Toast.Make("Vui lòng nhập tên loại tiền công",
                             CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
            return;
        }
        if (!double.TryParse(DonGiaLoaiTienCong, out var donGia) || donGia <= 0)
        {
            await Toast.Make("Đơn giá không hợp lệ",
                             CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
            return;
        }

        // 2) Kiểm tra trùng tên nội dung sửa chữa
        var allNd = await _noiDungService.GetAll() ?? new List<NoiDungSuaChua>();
        if (allNd.Any(x => x.TenNoiDungSuaChua
                           .Trim()
                           .Equals(TenNoiDungSuaChua.Trim(),
                                   StringComparison.OrdinalIgnoreCase)))
        {
            await Toast.Make("Tên nội dung sửa chữa đã tồn tại",
                             CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
            return;
        }

        // 3) Tạo mới NoiDungSuaChua
        var newNd = await _noiDungService.Create(new NoiDungSuaChua
        {
            Id = Guid.NewGuid(),
            TenNoiDungSuaChua = TenNoiDungSuaChua.Trim()
        });
        if (newNd == null)
        {
            await Toast.Make("Tạo nội dung sửa chữa thất bại",
                             CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
            return;
        }

        // 4) Kiểm tra trùng tên loại tiền công
        var allTc = await _tienCongService.GetAll() ?? new List<TienCong>();
        if (allTc.Any(x => x.TenLoaiTienCong
                           .Trim()
                           .Equals(TenLoaiTienCong.Trim(),
                                   StringComparison.OrdinalIgnoreCase)))
        {
            await Toast.Make("Tên loại tiền công bị trùng",
                             CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
            return;
        }

        // 5) Tạo mới TienCong, gán FK về nội dung vừa tạo
        var newTc = await _tienCongService.Create(new TienCong
        {
            Id = Guid.NewGuid(),
            TenLoaiTienCong = TenLoaiTienCong.Trim(),
            DonGiaLoaiTienCong = donGia,
            NoiDungSuaChuaId = newNd.Id
        });
        if (newTc == null)
        {
            await Toast.Make("Tạo loại tiền công thất bại",
                             CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
            return;
        }

        // 6) Thành công
        await Toast.Make("Thêm thành công",
                         CommunityToolkit.Maui.Core.ToastDuration.Short).Show();

        // 7) Reset form
        TenNoiDungSuaChua = string.Empty;
        TenLoaiTienCong = string.Empty;
        DonGiaLoaiTienCong = string.Empty;

        // 8) Callback để parent load lại
        OnTienCongAdded?.Invoke(newTc);
    }

    [RelayCommand]
    private void Cancel()
    {
        var window = Application.Current.Windows
                            .FirstOrDefault(w => w.Page is ThemLoaiTienCongPage);
        if (window != null)
            Application.Current.CloseWindow(window);
    }
}
