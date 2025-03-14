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
    public partial class QuanLiXePageViewModel: BaseViewModel
    {
        private string STORAGE_KEY = "user-account-status";
        private readonly APIClientService<Car> _carservice;
        private readonly APIClientService<PhieuSuaChua> _phieuservice;
        private readonly APIClientService<NoiDungPhieuSuaChua> _noidungphieuservice;

        [ObservableProperty]
        private ObservableCollection<Car> listcar = new ObservableCollection<Car>();

        public QuanLiXePageViewModel(APIClientService<Car> carservice,
            APIClientService<PhieuSuaChua> phieuservice, 
            APIClientService<NoiDungPhieuSuaChua> noidungphieuservice)
        {
            _carservice = carservice;
            _ = LoadAsync();
            _phieuservice = phieuservice;
            _noidungphieuservice = noidungphieuservice;
        }
        private async Task LoadAsync()
        {
            var list = await _carservice.GetAll();
            if (list is not null)
            {
                foreach (var item in list)
                {
                    if (item.TienNoCuaChuXe is null)
                    {
                        var phieusuachua = _phieuservice.GetThroughtSpecialRoute($"GetThroughBienSoXe/{item.BienSo}");
                        List<NoiDungPhieuSuaChua> listnoidung = await _noidungphieuservice.GetListOnSpecialRequirement($"GetListThroughPhieuSuaChuaID/{phieusuachua.Id}");
                        item.TienNoCuaChuXe = 0;
                        item.TienNoCuaChuXe = listnoidung.Sum(u => u.ThanhTien);
                        await _carservice.Update(item);
                    }
                    listcar.Add(item);
                }
            }
        }
        [RelayCommand]
        private async Task GoToChiTietXePage(int parameter)
        {
            await Shell.Current.GoToAsync($"//ChiTietXePage?parameterID={parameter}", true);
        }
        [RelayCommand]
        private async Task GoBack()
        {
            var json=await SecureStorage.Default.GetAsync(STORAGE_KEY);
            if (string.IsNullOrEmpty(json)) await Shell.Current.GoToAsync("//LoginPage");
            var currentaccount = JsonSerializer.Deserialize<UserAccountSession>(json);
            if (currentaccount.Role=="Member") await Shell.Current.GoToAsync("//MemberMainPage", true);
        }
    }
}
