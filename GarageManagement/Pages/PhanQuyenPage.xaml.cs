namespace GarageManagement.Pages;

using APIClassLibrary.APIModels;
using GarageManagement.ViewModels;

public partial class PhanQuyenPage : ContentView
{
	private readonly PhanQuyenPageViewModel _viewModel;
    public PhanQuyenPage(PhanQuyenPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_viewModel = viewModel;
    }
    private void OnVaiTroPicked(object sender, EventArgs e)
	{
		if(sender is Picker picker && picker.SelectedItem is NhomNguoiDung selectedRole)
        {
            _viewModel.nhomNguoiDung = selectedRole;
            _ = _viewModel.OnVaiTroPicked();
        }
    }
    private void OnCheckBoxChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox checkBox && BindingContext is PhanQuyenPageViewModel viewModel)
        {
            Guid chucNangId = Guid.Empty;
            bool isChecked = e.Value;

            // Xác định Guid của chức năng dựa trên x:Name của CheckBox
            switch (checkBox.AutomationId)
            {
                case "QuanLyDsXeCheckBox":
                    chucNangId = PhanQuyenPageViewModel.QuanLyDsXeId;
                    break;
                case "QuanLyDsHieuXeCheckBox":
                    chucNangId = PhanQuyenPageViewModel.QuanLyDsHieuXeId;
                    break;
                case "QuanLyDsVatTuPhuTungCheckBox":
                    chucNangId = PhanQuyenPageViewModel.QuanLyDsVatTuPhuTungId;
                    break;
                case "QuanLyDsTienCongCheckBox":
                    chucNangId = PhanQuyenPageViewModel.QuanLyDsTienCongId;
                    break;
                case "BaoCaoDoanhSoCheckBox":
                    chucNangId = PhanQuyenPageViewModel.BaoCaoDoanhSoId;
                    break;
                case "TiepNhanXeCheckBox":
                    chucNangId = PhanQuyenPageViewModel.TiepNhanXeId;
                    break;
                case "LapPhieuSuaChuaCheckBox":
                    chucNangId = PhanQuyenPageViewModel.LapPhieuSuaChuaId;
                    break;
                case "LapPhieuNhapCheckBox":
                    chucNangId = PhanQuyenPageViewModel.LapPhieuNhapId;
                    break;
                case "LapPhieuThuTienCheckBox":
                    chucNangId = PhanQuyenPageViewModel.LapPhieuThuTienId;
                    break;
                case "PhanQuyenCheckBox":
                    chucNangId = PhanQuyenPageViewModel.PhanQuyenId;
                    break;
                case "ThayDoiThamSoCheckBox":
                    chucNangId = PhanQuyenPageViewModel.ThayDoiThamSoId;
                    break;
            }

            if (chucNangId != Guid.Empty)
            {
                if (isChecked)
                {
                    // Thêm ChucNangId vào ChucNangList nếu được check
                    if (!viewModel.ChucNangList.Contains(chucNangId))
                    {
                        viewModel.ChucNangList.Add(chucNangId);
                    }
                }
                else
                {
                    // Xóa ChucNangId khỏi ChucNangList nếu bỏ check
                    if (viewModel.ChucNangList.Contains(chucNangId))
                    {
                        viewModel.ChucNangList.Remove(chucNangId);
                    }
                }
            }
        }
    }
}