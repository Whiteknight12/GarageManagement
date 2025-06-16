namespace GarageManagement.Pages;

using APIClassLibrary.APIModels;
using GarageManagement.ViewModels;

public partial class PhanQuyenPage : ContentView
{
	public readonly PhanQuyenPageViewModel _viewModel;
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

            // Xác định Guid của chức năng dựa trên AutomationId của CheckBox
            switch (checkBox.AutomationId)
            {
                case "QuanLiDanhSachXeCheckBox":
                    chucNangId = _viewModel.QuanLiDanhSachXeId;
                    break;
                case "QuanLiDanhSachHieuXeCheckBox":
                    chucNangId = _viewModel.QuanLiDanhSachHieuXeId;
                    break;
                case "QuanLiDanhSachLoaiVatTuCheckBox":
                    chucNangId = _viewModel.QuanLiDanhSachLoaiVatTuId;
                    break;
                case "QuanLiDanhSachLoaiTienCongCheckBox":
                    chucNangId = _viewModel.QuanLiDanhSachLoaiTienCongId;
                    break;
                case "QuanLiNhanVienCheckBox":
                    chucNangId = _viewModel.QuanLiNhanVienId;
                    break;
                case "QuanLiKhachHangCheckBox":
                    chucNangId = _viewModel.QuanLiKhachHangId;
                    break;
                case "QuanLiPhieuNhapCheckBox":
                    chucNangId = _viewModel.QuanLiPhieuNhapId;
                    break;
                case "QuanLiPhieuSuaChuaCheckBox":
                    chucNangId =    _viewModel.QuanLiPhieuSuaChuaId;
                    break;
                case "QuanLiPhieuThuTienCheckBox":
                    chucNangId = _viewModel.QuanLiPhieuThuTienId;
                    break;
                case "QuanLiPhieuTiepNhanCheckBox":
                    chucNangId = _viewModel.QuanLiPhieuTiepNhanId;
                    break;
                case "QuanLiBaoCaoThangCheckBox":
                    chucNangId = _viewModel.QuanLiBaoCaoThangId;
                    break;
                case "QuanLiBaoCaoTonCheckBox":
                    chucNangId = _viewModel.QuanLiBaoCaoTonId;
                    break;
                case "TiepNhanXeCheckBox":
                    chucNangId = _viewModel.TiepNhanXeId;
                    break;
                case "LapPhieuSuaChuaCheckBox":
                    chucNangId = _viewModel.LapPhieuSuaChuaId;
                    break;
                case "LapPhieuNhapCheckBox":
                    chucNangId = _viewModel.LapPhieuNhapId;
                    break;
                case "LapPhieuThuTienCheckBox":
                    chucNangId = _viewModel.LapPhieuThuTienId;
                    break;
                case "PhanQuyenCheckBox":
                    chucNangId = _viewModel.PhanQuyenId;
                    break;
                case "ThayDoiThamSoCheckBox":
                    chucNangId = _viewModel.ThayDoiThamSoId;
                    break;
                case "QuanLiDanhSachTaiKhoanCheckBox":
                    chucNangId = _viewModel.QuanLiDanhSachTaiKhoanId;
                    break;
                case "QuanLiLichSuCheckBox":
                    chucNangId = _viewModel.QuanLiLichSuId;
                    break;
                case "KhachHangXemDanhSachXeCheckBox":
                    chucNangId = _viewModel.KhachHangXemDanhSachXeId;
                    break;
            }

            if (chucNangId != Guid.Empty)
            {
                if (isChecked)
                {
                    // Thêm Permission nếu được check
                    if (!viewModel.ChucNangList.Contains(chucNangId))
                        viewModel.ChucNangList.Add(chucNangId);
                }
                else
                {
                    // Bỏ Permission nếu bỏ check
                    if (viewModel.ChucNangList.Contains(chucNangId))
                        viewModel.ChucNangList.Remove(chucNangId);
                }
            }
        }
    }

}