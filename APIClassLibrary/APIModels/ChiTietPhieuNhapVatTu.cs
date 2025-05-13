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

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
