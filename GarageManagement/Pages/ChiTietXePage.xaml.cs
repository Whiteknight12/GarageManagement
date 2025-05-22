using APIClassLibrary;
using APIClassLibrary.APIModels;
using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class ChiTietXePage : ContentView
{
    public readonly ChiTietXePageViewModel _viewModel;
    private readonly APIClientService<Xe> _carservice;
    private readonly APIClientService<HieuXe> _hieuxeservice;
    private readonly APIClientService<KhachHang> _khachhangservice;

    public ChiTietXePage(APIClientService<Xe> carservice, 
        APIClientService<HieuXe> hieuxeservice, 
        APIClientService<KhachHang> khachhangservice,
        ChiTietXePageViewModel viewModel)
    {
        _carservice = carservice;
        _hieuxeservice = hieuxeservice;
        _khachhangservice = khachhangservice;
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        if (width>0 && height>0) _=_viewModel.LoadAsync();
    }

    public void setCarId(Guid id)
    {
        _viewModel.CarId = id;
        _= _viewModel.LoadAsync();
    }

    public async void SelectedKhachHangChanged(object sender, EventArgs e)
    {
        if (sender is Picker picker && picker.SelectedItem is KhachHang khachHang)
        {
            _viewModel.selectedKhachHangId = khachHang.Id;
            var result = await _khachhangservice.GetByID(khachHang.Id);
            if (result is not null)
            {
                _viewModel.TenChuXe = result.HoVaTen;
                _viewModel.DienThoai = result.SoDienThoai;
                _viewModel.DiaChi = result.DiaChi;
                var listXe=await _carservice.GetListOnSpecialRequirement($"PhoneNumber/{result.SoDienThoai}");
                if (listXe is not null)
                {
                    _viewModel.TienNoCuaChuXe = 0;
                    _viewModel.TienNoCuaChuXe+=listXe.Sum(x => x.TienNo ?? 0);
                }
            }
        }
    }
}