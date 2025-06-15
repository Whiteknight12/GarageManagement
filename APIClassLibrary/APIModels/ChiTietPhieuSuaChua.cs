using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APIClassLibrary.APIModels
{
    public class ChiTietPhieuSuaChua : INotifyPropertyChanged
    {
        public Guid Id { get; set; }
        public Guid PhieuSuaChuaId { get; set; }
        public Guid? TienCongId { get; set; }
        public Guid? NoiDungSuaChuaId { get; set; }

        public double? ThanhTien { get; set; }

        //ID de xu li trong UI, khong luu vao db
        public int? NoiDungId { get; set; }
        //gia tien cong de xu li UI, khong luu vao db
        public double? GiaTienCong { get; set; }
        //list nay de xu li UI, khong luu vao db
        public ObservableCollection<VTPTChiTietPhieuSuaChua>? ListSpecifiedVTPT { get; set; } = new();
        //ten noi dung sua chua de xu li trong UI, khong luu vao db
        public string? TenNoiDungSuaChua { get; set; }

        //For UI only
        private NoiDungSuaChua? _selectedNoiDungSuaChua;
        public NoiDungSuaChua? SelectedNoiDungSuaChua
        {
            get => _selectedNoiDungSuaChua;
            set
            {
                if (_selectedNoiDungSuaChua != value)
                {
                    _selectedNoiDungSuaChua = value;
                    OnPropertyChanged(nameof(SelectedNoiDungSuaChua));
                    NoiDungSuaChuaId = value?.Id;
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
