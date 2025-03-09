using CommunityToolkit.Mvvm.Input;
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
            await Shell.Current.GoToAsync("//TiepNhanXePage");
        }
        [RelayCommand]
        public async void GotoPhieuSuaChuaPage()
        {
            await Shell.Current.GoToAsync("//TaoPhieuSuaChuaPage");
        }
    }
}
