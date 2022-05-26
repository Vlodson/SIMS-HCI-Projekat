using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Admin.View;
using Model;
using Controller;
using Utility;
using HospitalMain.Enums;
using Enums;
using System.Windows.Controls;

namespace Admin.ViewModel
{
    public class LoginViewModel: BindableBase
    {
        public ICommandTemplate<object> LoginCommand;
        public ICommandTemplate<object> RegisterCommand;

        private UserAccountController _userAccountController;

        private String uid;
        private String password;

        public String UID
        {
            get { return uid; }
            set
            {
                if(uid != value)
                {
                    uid = value;
                    OnPropertyChanged("UID");
                }
            }
        }

        public LoginViewModel()
        {
            LoginCommand = new ICommandTemplate<object>(OnLogin, CanLoginRegister);
            RegisterCommand = new ICommandTemplate<object>(OnRegister, CanLoginRegister);

            var app = Application.Current as App;
            _userAccountController = app.userAccountController;
        }

        public void OnLogin(object passwordBoxObj)
        {
            if (_userAccountController.ReadUserAccount(UID) == null)
            {
                MessageBox.Show("User ID does not exist");
                return;
            }

            if (_userAccountController.CheckUserType(UID) != UserType.Admin)
            {
                MessageBox.Show("Access not allowed for this user type");
                return;
            }

            if (!_userAccountController.LogIn(UID, password, UserType.Admin))
            {
                MessageBox.Show("User ID or password incorrect");
                return;
            }
            else
            {
                // change view to main menu
            }
        }

        public bool CanLoginRegister(object passwordBoxObj)
        {
            PasswordBox passwordBox = passwordBoxObj as PasswordBox;
            password = passwordBox.Password;

            return !String.IsNullOrEmpty(UID) && !String.IsNullOrEmpty(password);
        }

        public void OnRegister(object passwordBoxObj)
        {
            if (!_userAccountController.Register(UID, password, UserType.Admin))
            {
                MessageBox.Show("Registration failed. Try different user ID");
                // change view
                return;
            }
        }
    }
}
