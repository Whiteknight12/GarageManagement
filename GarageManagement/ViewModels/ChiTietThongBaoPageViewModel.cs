using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class ChiTietThongBaoPageViewModel: BaseViewModel
    {
        private readonly APIClientService<ThongBao> _thongBaoService;
        private readonly APIClientService<NhomNguoiDung> _nhomService;

        public Guid thongBaoId { get; set; }

        [ObservableProperty]
        private string? noiDungThongBao;

        [ObservableProperty]
        private ObservableCollection<NhomNguoiDung> listNhomNguoiDung = new();

        [ObservableProperty]
        private NhomNguoiDung? selectedNhomNguoiDung;

        [ObservableProperty]
        private bool isEditMode = false;

        [ObservableProperty]
        private bool isNotEditMode = true;

        private ThongBao? originalThongBao;

        public ChiTietThongBaoPageViewModel(APIClientService<ThongBao> thongBaoService, APIClientService<NhomNguoiDung> nhomService)
        {
            _thongBaoService = thongBaoService;
            _nhomService = nhomService;
        }

        public async Task LoadAsync()
        {
            var tb = await _thongBaoService.GetByID(thongBaoId);
            if (tb is null) return;

            originalThongBao = tb;
            NoiDungThongBao = tb.Content;
            SelectedNhomNguoiDung = null;

            var roles = await _nhomService.GetAll();
            if (roles is not null)
            {
                ListNhomNguoiDung = new ObservableCollection<NhomNguoiDung>(roles);
                if (tb.NhomNguoiDungId.HasValue)
                    SelectedNhomNguoiDung = ListNhomNguoiDung.FirstOrDefault(r => r.Id == tb.NhomNguoiDungId);
            }
        }

        [RelayCommand]
        private void BatDauChinhSua()
        {
            IsEditMode = true;
            IsNotEditMode = false;
        }

        [RelayCommand]
        private async Task LuuThongBao()
        {
            if (originalThongBao == null) return;

            originalThongBao.Content = NoiDungThongBao;
            originalThongBao.NhomNguoiDungId = SelectedNhomNguoiDung?.Id;
            await _thongBaoService.Update(originalThongBao);
            MessagingCenter.Send(this, "NewThongBaoCreated");

            IsEditMode = false;
            IsNotEditMode = true;
        }

        [RelayCommand]
        private void HuyChinhSua()
        {
            if (originalThongBao == null) return;

            NoiDungThongBao = originalThongBao.Content;
            SelectedNhomNguoiDung = ListNhomNguoiDung.FirstOrDefault(r => r.Id == originalThongBao.NhomNguoiDungId);
            IsEditMode = false;
            IsNotEditMode = true;
        }
    }
}
