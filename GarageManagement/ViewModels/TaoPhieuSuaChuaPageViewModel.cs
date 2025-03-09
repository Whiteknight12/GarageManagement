using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class TaoPhieuSuaChuaPageViewModel: BaseViewModel
    {
        private readonly APIClientService<Car> _carservice;
        private readonly APIClientService<TienCong> _tiencongservice;
        private readonly APIClientService<VatTuPhuTung> _vattuservice;
        private readonly APIClientService<NoiDungPhieuSuaChua> _noidungphieuservice;
        private readonly APIClientService<ChiTietTienCong> _chitiettiencongservice;
        private readonly APIClientService<PhieuSuaChua> _phieuservice;

        [ObservableProperty]
        private ObservableCollection<string> listbiensoxe=new ObservableCollection<string>();

        [ObservableProperty]
        private string selectedbiensoxe;

        [ObservableProperty]
        private DateTime ngaysuachua=DateTime.Now;

        [ObservableProperty]
        private ObservableCollection<NoiDungPhieuSuaChua> listnoidung = new ObservableCollection<NoiDungPhieuSuaChua>();

        [ObservableProperty]
        private ObservableCollection<TienCong> listtiencong=new ObservableCollection<TienCong>();

        [ObservableProperty]
        private ObservableCollection<VatTuPhuTung> listvattuphutung=new ObservableCollection<VatTuPhuTung>();

        public TaoPhieuSuaChuaPageViewModel(APIClientService<Car> carservice, 
            APIClientService<TienCong> tiencongservice,
            APIClientService<VatTuPhuTung> vattuservice,
            APIClientService<NoiDungPhieuSuaChua> noidungphieuservice,
            APIClientService<ChiTietTienCong> chitiettiencongservice,
            APIClientService<PhieuSuaChua> phieuservice)
        {
            _carservice = carservice;
            _tiencongservice = tiencongservice;
            _vattuservice = vattuservice;
            _noidungphieuservice = noidungphieuservice;
            _chitiettiencongservice = chitiettiencongservice;
            _phieuservice = phieuservice;
            _=LoadAsync();
        }
        private async Task LoadAsync()
        {
            var listcar = await _carservice.GetAll();
            if (listcar is not null)
            {
                listbiensoxe.Clear();
                foreach (var car in listcar)
                {
                    listbiensoxe.Add(car.BienSo);
                }
                OnPropertyChanged(nameof(Listbiensoxe));
            }
            var listcong = await _tiencongservice.GetAll();
            if (listcong is not null)
            {
                listtiencong.Clear();
                foreach (var tiencong in listcong) listtiencong.Add(tiencong);
                OnPropertyChanged(nameof(Listtiencong));
            }
            var listphutung = await _vattuservice.GetAll();
            if (listphutung is not null)
            {
                listvattuphutung.Clear();
                foreach (var vattu in listphutung) listvattuphutung.Add(vattu);
                OnPropertyChanged(nameof(Listvattuphutung));
            }
            listnoidung.Add(new NoiDungPhieuSuaChua());
        }

        [RelayCommand]
        public void ThemNoiDungSuaChua()
        {
            listnoidung.Add(new NoiDungPhieuSuaChua
            {

            });
        }
        [RelayCommand]
        public void LuuPhieuSuaChua()
        {

        }
        [RelayCommand]
        public async void BackToMainPage()
        {
            await Shell.Current.GoToAsync("//MemberMainPage");
        }
    }
}
