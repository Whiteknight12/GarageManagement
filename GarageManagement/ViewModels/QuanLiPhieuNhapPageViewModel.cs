using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Services;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace GarageManagement.ViewModels
{
    public partial class QuanLiPhieuNhapPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<PhieuNhapVatTu> listPhieuNhapVatTu = new();
        [ObservableProperty]
        private bool isDeleteMode;
        private readonly APIClientService<PhieuNhapVatTu> _PhieuNhapVatTuService;
        private readonly AuthenticationService _authenticationService;

        public QuanLiPhieuNhapPageViewModel(APIClientService<PhieuNhapVatTu> PhieuNhapVatTuService,
            ILogger<QuanLiPhieuNhapPageViewModel> logger,
            AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _PhieuNhapVatTuService = PhieuNhapVatTuService;
            _ = LoadAsync();
            IsDeleteMode = false;
        }

        public async Task LoadAsync()
        {
            _ = _authenticationService.FettaiKhoanSession();
            var httpClient = _PhieuNhapVatTuService.GetHttpClient;
            var token = _authenticationService.GetCurrentAccountStatus.Token;
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var list = await _PhieuNhapVatTuService.GetAll();

            foreach (var ps in list)
            {
                ps.STT = list.IndexOf(ps) + 1;
            }
            ListPhieuNhapVatTu = new ObservableCollection<PhieuNhapVatTu>(list);
        }

        public void Load(PhieuNhapVatTu item)
        {
            ListPhieuNhapVatTu.Add(item);
            
            for (int i = 0; i < ListPhieuNhapVatTu.Count; i++)
            {
                ListPhieuNhapVatTu[i].STT = i + 1;
            }
        }

        [RelayCommand]
        private void Add()
        {
            
        }

        [RelayCommand]
        private void Edit(Guid id)
        {
            
        }

        [RelayCommand]
        private void ToggleDeleteMode()
        {
            IsDeleteMode = !IsDeleteMode;
            if (!IsDeleteMode) 
            {
                var updatedList = new List<PhieuNhapVatTu>(ListPhieuNhapVatTu);
                foreach (var ps in updatedList)
                {
                    ps.IsSelected = false;
                }
                
                ListPhieuNhapVatTu = new ObservableCollection<PhieuNhapVatTu>(updatedList);
            }
        }

        [RelayCommand]
        private async Task Delete()
        {
            var selectedItems = ListPhieuNhapVatTu.Where(x => x.IsSelected).ToList();
            foreach (var item in selectedItems)
            {
                await _PhieuNhapVatTuService.Delete(item.Id);
                ListPhieuNhapVatTu.Remove(item);
            }
            
            for (int i = 0; i < ListPhieuNhapVatTu.Count; i++)
            {
                ListPhieuNhapVatTu[i].STT = i + 1;
            }
            IsDeleteMode = false;
        }
    }
}
