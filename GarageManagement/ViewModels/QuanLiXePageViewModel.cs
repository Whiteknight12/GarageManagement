using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
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
        private readonly APIClientService<Xe> _carservice;
        private readonly APIClientService<PhieuSuaChua> _phieuservice;
        private readonly APIClientService<ChiTietPhieuSuaChua> _noidungphieuservice;

        [ObservableProperty]
        private ObservableCollection<Xe> listcar = new ObservableCollection<Xe>();

        public QuanLiXePageViewModel(APIClientService<Xe> carservice,
            APIClientService<PhieuSuaChua> phieuservice, 
            APIClientService<ChiTietPhieuSuaChua> noidungphieuservice)
        {
            _carservice = carservice;
            _ = LoadAsync();
            _phieuservice = phieuservice;
            _noidungphieuservice = noidungphieuservice;
        }
        public async Task LoadAsync()
        {
            var list = await _carservice.GetAll();
            if (list is not null)
            {
                listcar.Clear();
                foreach (var item in list)
                {
                    if (item.TienNo is null)
                    {
                        var phieusuachua = _phieuservice.GetThroughtSpecialRoute($"GetByBienSoXe/{item.BienSo}");
                        List<ChiTietPhieuSuaChua> listnoidung = await _noidungphieuservice.GetListOnSpecialRequirement($"GetListByPhieuSuaChuaID/{phieusuachua.Id}");
                        item.TienNo = 0;
                        item.TienNo = listnoidung.Sum(u => u.ThanhTien);
                        await _carservice.Update(item);
                    }
                    listcar.Add(item);
                }
            }
        }
        [RelayCommand]
        private async Task GoToChiTietXePage(int parameter)
        {
            await Shell.Current.GoToAsync($"//{nameof(ChiTietXePage)}?parameterID={parameter}", true);
        }
        [RelayCommand]
        private async Task GoBack()
        {
            var json=await SecureStorage.Default.GetAsync(STORAGE_KEY);
            if (string.IsNullOrEmpty(json)) await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            var currentaccount = JsonSerializer.Deserialize<UserAccountSession>(json);
            if (currentaccount.Role=="Member") await Shell.Current.GoToAsync($"//{nameof(NhanSuMainPage)}", true);
        }
    }
}
