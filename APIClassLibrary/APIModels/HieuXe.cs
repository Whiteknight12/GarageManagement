using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class HieuXe: INotifyPropertyChanged
    {
        public Guid Id { get; set; } 
        public string? TenHieuXe { get; set; }
        public bool IsSelected { get; set; } = false;

        //de bieu dien UI
        public int? IdForUI { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
