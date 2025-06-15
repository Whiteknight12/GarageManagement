using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class ChiTietPhieuNhapVatTu : INotifyPropertyChanged
    {
        public Guid Id { get; set; }
        public Guid PhieuNhapId { get; set; }
        public Guid? VatTuId { get; set; }
        public double? DonGia { get; set; }
        public int? SoLuong { get; set; }

        //de xu li UI
        public string? TenVatTu { get; set; }
        public double? ThanhTien { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        VatTuPhuTung? selectedVatTu;
        public VatTuPhuTung? SelectedVatTu
        {
            get => selectedVatTu;
            set
            {
                if (selectedVatTu != value)
                {
                    selectedVatTu = value;
                    VatTuId = value?.VatTuPhuTungId;
                    DonGia = value?.DonGiaBanLoaiVatTuPhuTung;
                    OnPropertyChanged(nameof(SelectedVatTu));
                    OnPropertyChanged(nameof(VatTuId));
                    OnPropertyChanged(nameof(DonGia));
                }
            }
        }
    }
}
