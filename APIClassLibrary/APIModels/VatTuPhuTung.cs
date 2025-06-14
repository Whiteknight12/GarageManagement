using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace APIClassLibrary.APIModels
{
    public class VatTuPhuTung : INotifyPropertyChanged
    {
        // 1. Khai báo event
        public event PropertyChangedEventHandler PropertyChanged;

        // 2. Helper để raise event
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // 3. Backing fields + properties
        private Guid _vatTuPhuTungId;
        public Guid VatTuPhuTungId
        {
            get => _vatTuPhuTungId;
            set
            {
                if (_vatTuPhuTungId != value)
                {
                    _vatTuPhuTungId = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _tenLoaiVatTuPhuTung;
        public string TenLoaiVatTuPhuTung
        {
            get => _tenLoaiVatTuPhuTung;
            set
            {
                if (_tenLoaiVatTuPhuTung != value)
                {
                    _tenLoaiVatTuPhuTung = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _soLuong;
        public int SoLuong
        {
            get => _soLuong;
            set
            {
                if (_soLuong != value)
                {
                    _soLuong = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _donGiaBanLoaiVatTuPhuTung;
        public double DonGiaBanLoaiVatTuPhuTung
        {
            get => _donGiaBanLoaiVatTuPhuTung;
            set
            {
                if (Math.Abs(_donGiaBanLoaiVatTuPhuTung - value) > 0.0001)
                {
                    _donGiaBanLoaiVatTuPhuTung = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
