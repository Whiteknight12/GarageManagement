using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class NhanSuMainPageViewModel: BaseViewModel
    {
        [RelayCommand]
        public async void GotoTiepNhanXePage()
        {
            await Shell.Current.GoToAsync($"//{nameof(TiepNhanXePage)}", true);
        }
        [RelayCommand]
        public async void GotoPhieuSuaChuaPage()
        {
            await Shell.Current.GoToAsync($"//{nameof(TaoPhieuSuaChuaPage)}", true);
        }
        [RelayCommand]
        public async void GotoQuanLiXePage()
        {
            await Shell.Current.GoToAsync($"//{nameof(QuanLiXePage)}", true);
        }
        [RelayCommand]
        public async void GoToThuTienPage()
        {
            await Shell.Current.GoToAsync($"//{nameof(ThuTienPage)}", true);
        }
        [RelayCommand]
        public async void GoToBaoCaoDoanhSoPage()
        {
            await Shell.Current.GoToAsync($"//{nameof(BaoCaoDoanhSoPage)}", true);
        }
    }
}
