using HospitalMain.Enums;
using HospitalMain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.ViewModel
{
    public class UserAccountViewModel : ViewModelBase
    {

        private readonly UserAccount _user;

        public String Username => _user.UserName;
        public String Password => _user.Password;
        public UserType Type => _user.Type;

        public UserAccountViewModel(UserAccount user)
        {
            _user = user;
        }
    }
}
