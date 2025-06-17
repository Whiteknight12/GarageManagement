using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace GarageManagement.ViewModels
{
    public partial class TaoThongBaoPageViewModel: BaseViewModel
    {
        private readonly APIClientService<ThongBao> _thongBaoService;
        private readonly APIClientService<NhomNguoiDung> _nhomNguoiDungService;

        [ObservableProperty]
        private string noiDungThongBao;

        [ObservableProperty]
        private NhomNguoiDung selectedNhomNguoiDung;

        [ObservableProperty]
        private ObservableCollection<NhomNguoiDung> listNhomNguoiDung = new ObservableCollection<NhomNguoiDung>();

        public TaoThongBaoPageViewModel(APIClientService<ThongBao> thongBaoService, 
            APIClientService<NhomNguoiDung> nhomNguoiDungService)
        {
            _thongBaoService = thongBaoService;
            _nhomNguoiDungService = nhomNguoiDungService;
        }

        public async Task LoadAsync()
        {
            var list = await _nhomNguoiDungService.GetAll();
            list.Add(new NhomNguoiDung()
            {
                Id=Guid.NewGuid(),
                TenNhom="Tất cả"
            });
            ListNhomNguoiDung=new ObservableCollection<NhomNguoiDung>(list);
        }

        [RelayCommand]
        private async Task GuiThongBao()
        {
            ThongBao thongBao;
            if (SelectedNhomNguoiDung.TenNhom=="Tất cả")
            {
                thongBao = new ThongBao()
                {
                    Id = Guid.NewGuid(),
                    Content = NoiDungThongBao,
                    isForAll=true,
                    taoVaoLuc=DateTime.UtcNow.ToLocalTime()
                };
            }
            else
            {
                thongBao = new ThongBao()
                {
                    Id = Guid.NewGuid(),
                    Content = NoiDungThongBao,
                    NhomNguoiDungId = SelectedNhomNguoiDung.Id,
                    taoVaoLuc = DateTime.UtcNow.ToLocalTime()
                };
            }
            var createdThongBao = await _thongBaoService.Create(thongBao);
            if (createdThongBao != null)
            {
                var toast=Toast.Make("Tạo thông báo thành công", CommunityToolkit.Maui.Core.ToastDuration.Short);
                await toast.Show();
            }
            else
            {
                await Shell.Current.DisplayAlert("Thông báo", "Tạo thông báo thất bại", "OK");
            }
            SelectedNhomNguoiDung = null;
            NoiDungThongBao = "";
            MessagingCenter.Send(this, "NewThongBaoCreated");
        }
    }
}
