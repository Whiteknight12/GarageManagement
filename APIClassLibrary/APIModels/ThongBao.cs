
using System.ComponentModel;

namespace APIClassLibrary.APIModels
{
    public class ThongBao
    {
        public Guid Id { get; set; } 

        public string? Content { get; set; }

        public Guid? NhomNguoiDungId { get; set; }

        public bool? isForAll { get; set; }

        public DateTime taoVaoLuc { get; set; }

        public Guid xeId { get; set; }

        //for UI only
        public bool? DaDoc = false;
        public bool? Visible = true;
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
