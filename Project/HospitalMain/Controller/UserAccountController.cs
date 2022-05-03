using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HospitalMain.Enums;
using HospitalMain.Model;
using Service;

namespace HospitalMain.Controller
{
    public class UserAccountController
    {

        private readonly UserAccountService _userAccountService;

        public UserAccountController(UserAccountService service)
        {
            _userAccountService = service;
        }

        public bool LogIn(String uid, String password, UserType type)
        {
            return _userAccountService.LogIn(uid, password, type);
        }

        public bool Register(String uid, String password, UserType type)
        {
            return _userAccountService.Register(uid, password, type);
        }

        public bool DeleteUser(String username)
        {
            return _userAccountService.DeleteUser(username);
        }

    }
}
