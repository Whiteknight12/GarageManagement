using APIClassLibrary;
using APIClassLibrary.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.Services
{
    public class UniqueConstraintCheckingService
    {
        private readonly APIClientService<Car> _carservice;
        private readonly APIClientService<User> _userservice;

        public UniqueConstraintCheckingService(APIClientService<Car> carservice, APIClientService<User> userservice)
        {
            _carservice = carservice;
            _userservice = userservice;
        }

        public async Task<bool> CheckUniqueBienSo(string bienso)
        {
            var carlist=await _carservice.GetAll();
            return carlist.Any(x => x.BienSo == bienso);
        }

        public async Task<bool> CheckUniquePhoneNumber(string phonenumber)
        {
            var userlist = await _userservice.GetAll();
            return userlist.Any(x => x.PhoneNumber == phonenumber);
        }
    }
}
