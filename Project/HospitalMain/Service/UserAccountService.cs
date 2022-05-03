using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HospitalMain.Enums;
using HospitalMain.Model;
using Repository;

namespace Service
{
    public class UserAccountService
    {
        private readonly UserAccountRepo _userAccountRepo;

        public UserAccountService(UserAccountRepo repo)
        {
            _userAccountRepo = repo;
        }

        public UserType CheckUserType(String uid)
        {
            return _userAccountRepo.ReadUserAccount(uid).Type;
        }

        public bool LogIn(String uid, String password, UserType type)
        {
            foreach(UserAccount user in _userAccountRepo.GetAllUserAccounts())
            {
                if (uid.Equals(user.UserName) && password.Equals(user.Password) && type == user.Type)
                    return true;
            }

            return false;
        }

        public bool Register(String uid, String password, UserType type)
        {
            foreach(UserAccount user in _userAccountRepo.GetAllUserAccounts())
            {
                if (uid.Equals(user.UserName))
                    return false;
            }

            _userAccountRepo.AddUserAccount(new UserAccount(uid, password, type));
            return true;
        }

        public bool DeleteUser(String username)
        {
            return _userAccountRepo.DeleteUserAccount(username);
        }

    }
}
