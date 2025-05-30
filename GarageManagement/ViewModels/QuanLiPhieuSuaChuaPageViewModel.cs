﻿using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Services;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace GarageManagement.ViewModels
{
    public partial class QuanLiPhieuSuaChuaPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<PhieuSuaChua> listPhieuSuaChua = new();
        [ObservableProperty]
        private bool isDeleteMode;
        private readonly APIClientService<PhieuSuaChua> _phieuSuaChuaService;
        private readonly AuthenticationService _authenticationService;
        private readonly APIClientService<Xe> _xeService; // Service để lấy thông tin xe

        public QuanLiPhieuSuaChuaPageViewModel(APIClientService<PhieuSuaChua> phieuSuaChuaService,
            APIClientService<Xe> xeService,
            ILogger<QuanLiPhieuSuaChuaPageViewModel> logger,
            AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _phieuSuaChuaService = phieuSuaChuaService;
            _xeService = xeService;
            _ = LoadAsync();
            IsDeleteMode = false;
        }

        public async Task LoadAsync()
        {
            _ = _authenticationService.FettaiKhoanSession();
            var httpClient = _phieuSuaChuaService.GetHttpClient;
            var token = _authenticationService.GetCurrentAccountStatus.Token;
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var listPhieu = await _phieuSuaChuaService.GetAll();

            foreach (var ps in listPhieu)
            {
                ps.STT = listPhieu.IndexOf(ps) + 1;
                // Load thông tin xe dựa trên XeId
                var xe = await _xeService.GetByID(ps.XeId);
                if (xe != null)
                {
                    ps.TenXe = xe.Ten; // Giả định Xe có thuộc tính TenXe
                    ps.BienSoXe = xe.BienSo; // Giả định Xe có thuộc tính BienSoXe
                }
            }
            ListPhieuSuaChua = new ObservableCollection<PhieuSuaChua>(listPhieu);
        }

        public void Load(PhieuSuaChua item)
        {
            ListPhieuSuaChua.Add(item);
            // Cập nhật STT cho tất cả phiếu sửa chữa
            for (int i = 0; i < ListPhieuSuaChua.Count; i++)
            {
                ListPhieuSuaChua[i].STT = i + 1;
            }
        }

        [RelayCommand]
        private void Add()
        {
            // Logic để thêm phiếu sửa chữa mới (có thể mở một trang mới để nhập thông tin)
        }

        [RelayCommand]
        private void Edit(Guid id)
        {
            // Logic để sửa phiếu sửa chữa dựa trên id (có thể mở một trang để chỉnh sửa thông tin)
        }

        [RelayCommand]
        private void ToggleDeleteMode()
        {
            IsDeleteMode = !IsDeleteMode;
            if (!IsDeleteMode) // Khi hủy chế độ xóa
            {
                var updatedList = new List<PhieuSuaChua>(ListPhieuSuaChua);
                foreach (var ps in updatedList)
                {
                    ps.IsSelected = false;
                }
                // Gán lại ListPhieuSuaChua để buộc giao diện làm mới
                ListPhieuSuaChua = new ObservableCollection<PhieuSuaChua>(updatedList);
            }
        }

        [RelayCommand]
        private async Task Delete()
        {
            var selectedItems = ListPhieuSuaChua.Where(x => x.IsSelected).ToList();
            foreach (var item in selectedItems)
            {
                await _phieuSuaChuaService.Delete(item.Id);
                ListPhieuSuaChua.Remove(item);
            }
            // Cập nhật STT sau khi xóa
            for (int i = 0; i < ListPhieuSuaChua.Count; i++)
            {
                ListPhieuSuaChua[i].STT = i + 1;
            }
            IsDeleteMode = false;
        }

        [RelayCommand]
        private void ViewDetailPhieuSuaChua(Guid id)
        {
           MessagingCenter.Send(this, "ViewChiTietPhieuSuaChua", id);
        }
    }
}