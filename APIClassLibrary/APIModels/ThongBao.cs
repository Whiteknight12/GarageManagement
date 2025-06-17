
using System.ComponentModel;

namespace APIClassLibrary.APIModels
{
    public class ThongBao: INotifyPropertyChanged
    {
        public Guid Id { get; set; } 

        public string? Content { get; set; }

        public Guid? NhomNguoiDungId { get; set; }

        public bool? isForAll { get; set; }

        public DateTime taoVaoLuc { get; set; }

        public Guid xeId { get; set; }

        //for UI only
        private bool? _daDoc = false;
        public bool? DaDoc
        {
            get => _daDoc;
            set
            {
                if (_daDoc != value)
                {
                    _daDoc = value;
                    OnPropertyChanged(nameof(DaDoc));
                }
            }
        }

        private bool? _visible = true;
        public bool? Visible
        {
            get => _visible;
            set
            {
                if (_visible != value)
                {
                    _visible = value;
                    OnPropertyChanged(nameof(Visible));
                }
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected)));
                }
            }
        }

        public string? DanhCho { get; set; }    

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
