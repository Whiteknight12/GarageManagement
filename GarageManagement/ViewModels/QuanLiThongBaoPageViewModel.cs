using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class QuanLiThongBaoPageViewModel: BaseViewModel
    {
        private readonly APIClientService<ThongBao> _thongBaoService;
        private readonly APIClientService<NhomNguoiDung> _roleService;

        private readonly TaoThongBaoPage _taoThongBaoPage;

        private bool _deleteMode = false;
        private List<ThongBao> _allThongBao = new();

        [ObservableProperty]
        private ObservableCollection<ThongBao> listThongBao = new ObservableCollection<ThongBao>();

        [ObservableProperty]
        private string deleteButtonText = "Xóa";
        
        public QuanLiThongBaoPageViewModel(APIClientService<ThongBao> thongBaoService,
            APIClientService<NhomNguoiDung> roleService,
            TaoThongBaoPage taoThongBaoPage)
        {
            _thongBaoService = thongBaoService;
            _roleService = roleService;
            _taoThongBaoPage = taoThongBaoPage;
        }

        public async Task LoadAsync()
        {
            var list = await _thongBaoService.GetAll();
            if (list is not null)
            {
                foreach (var item in list)
                {
                    if (item.isForAll == null || item.isForAll == false)
                    {
                        var targetRole = await _roleService.GetByID(item.NhomNguoiDungId ?? Guid.Empty);
                        item.DanhCho = targetRole?.TenNhom ?? "";
                    }
                    else item.DanhCho = "Tất cả";
                }
                _allThongBao = list;
                ListThongBao = new ObservableCollection<ThongBao>(list);
            }
            var roles=await _roleService.GetAll();
            if (roles is not null)
            {
                ListRole = new ObservableCollection<NhomNguoiDung>(roles);
            }
            ListRole.Add(new NhomNguoiDung
            {
                Id=Guid.NewGuid(),
                TenNhom="Tất cả"
            });
        }

        [ObservableProperty]
        private ObservableCollection<NhomNguoiDung> listRole;

        [ObservableProperty]
        private NhomNguoiDung? selectedRole;

        [ObservableProperty]
        private string contentFilter = string.Empty;

        [ObservableProperty]
        private DateTime? selectedDateFilter;

        public bool IsDeleteMode
        {
            get => _deleteMode;
            set
            {
                SetProperty(ref _deleteMode, value);
                if (!_deleteMode)
                {
                    foreach (var tb in _allThongBao)
                        tb.IsSelected = false;
                    ApplyFilter();
                }
            }
        }

        partial void OnContentFilterChanged(string value) => ApplyFilter();
        partial void OnSelectedRoleChanged(NhomNguoiDung? value) => ApplyFilter();
        partial void OnSelectedDateFilterChanged(DateTime? value) => ApplyFilter();

        private void ApplyFilter()
        {
            var filtered = _allThongBao.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(ContentFilter))
            {
                filtered = filtered.Where(tb =>
                    tb.Content?.Contains(ContentFilter, StringComparison.OrdinalIgnoreCase) == true);
            }

            if (SelectedRole != null)
            {
                if (SelectedRole.TenNhom=="Tất cả")
                {
                    //do nothing
                }
                else filtered = filtered.Where(tb => tb.NhomNguoiDungId == SelectedRole.Id);
            }

            if (SelectedDateFilter.HasValue)
            {
                var dateOnly = SelectedDateFilter.Value.Date;
                filtered = filtered.Where(tb => tb.taoVaoLuc.Date == dateOnly);
            }

            ListThongBao = new ObservableCollection<ThongBao>(filtered);
        }

        [RelayCommand]
        public async Task DeleteSelected()
        {
            var toDelete = ListThongBao.Where(x => x.IsSelected).ToList();
            if (!toDelete.Any()) return;

            foreach (var tb in toDelete)
            {
                await _thongBaoService.Delete(tb.Id);
                _allThongBao.Remove(tb);
            }

            ApplyFilter();
            IsDeleteMode = false;
            await LoadAsync();
        }

        [RelayCommand]
        public async Task Add()
        {
            var wrapper = new ContentPage
            {
                Content = _taoThongBaoPage,
                Padding = 0
            };
            await _taoThongBaoPage._viewModel.LoadAsync();
            var win = new Window
            {
                Page = wrapper,
                Title = "Tạo thông báo mới",
                MaximumHeight = 1000,
                MaximumWidth = 1200,
                MinimumHeight = 1000,
                MinimumWidth = 1200
            };
            Application.Current?.OpenWindow(win);
        }

        [RelayCommand]
        private void ToggleDeleteMode()
        {
            IsDeleteMode = !IsDeleteMode;
            if (IsDeleteMode) DeleteButtonText = "Hủy";
            else DeleteButtonText = "Xóa";
        }

        [RelayCommand]
        private void ShowThongBaoDetail(Guid id)
        {
            // Ví dụ xử lý: mở popup hoặc chuyển trang, load chi tiết thông báo
        }
    }
}
