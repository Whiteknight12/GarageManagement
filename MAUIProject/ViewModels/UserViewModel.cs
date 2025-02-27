using APIClient;
using MAUIProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MAUIProject.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private readonly APIClientService<User> _service;
        public User user { get; set; }
        public UserViewModel(APIClientService<User> service)
        {
            _service = service;
        }
        public async void CreateUser(User user)
        {
            await _service.Create(user);
        }
    }
}
