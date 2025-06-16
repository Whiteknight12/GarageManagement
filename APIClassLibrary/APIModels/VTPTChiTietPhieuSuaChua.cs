using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class VTPTChiTietPhieuSuaChua: INotifyPropertyChanged
    {
        public Guid Id { get; set; } 
        public Guid ChiTietPhieuSuaChuaId { get; set; }
        public Guid VatTuPhuTungId { get; set; }
        public double DonGiaVTPTApDung { get; set; }
        public int SoLuong { get; set; }

        //don gia vtpt de xu li UI, khong luu vao db
        public double? DonGia { get; set; }
        //IdForUI de xu li trong UI, khong luu vao db
        public int? IdForUI { get; set; }
        //IdForDeleteUI de xu li trong UI, khong luu vao db
        public int? IdForDeleteUI { get; set; }
        //ten loai vtpt de xu li trong UI, khong luu vao db
        public string? TenLoaiVatTuPhuTung { get; set; }
        public Guid? SelectedVTPTId { get; set; }
        //for UI only
        public bool IsFirstTextChanged { get; set; } = true;
        //for UI only
        public bool isUpdatingVTPT { get; set; }
        //for UI only 
        public int oldValue { get; set; } = 0;

        //for UI only
        private VatTuPhuTung? _selectedVatTu;
        public VatTuPhuTung? SelectedVatTu
        {
            get => _selectedVatTu;
            set
            {
                if (_selectedVatTu != value)
                {
                    _selectedVatTu = value;
                    OnPropertyChanged(nameof(SelectedVatTu));
                    VatTuPhuTungId = value?.VatTuPhuTungId ?? Guid.Empty;
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
