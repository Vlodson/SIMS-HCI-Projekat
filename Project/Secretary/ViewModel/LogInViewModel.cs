using Controller;
using HospitalMain.Enums;
using Secretary.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Secretary.ViewModel
{
    public class LogInViewModel : ViewModelBase
    {
        private readonly UserAccountController _userAccountController;

        private UserAccountViewModel _userAccountViewModel;
        public UserAccountViewModel UserAccountViewModel
        {
            get { return _userAccountViewModel; }
            set { _userAccountViewModel = value; OnPropertyChanged(nameof(UserAccountViewModel)); }
        }
    
        //korisnicko ime
        private String _userName;
        public String Username
        {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged(nameof(Username)); }
        }

        //lozinka
        private string _password;
        public String Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        //tip korisnika
        private UserType _type;
        public UserType Type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged(nameof(Type)); }
        }

        public ICommand SignInCommand { get; }
        public ICommand RegisterCommand { get; }
    
        private Window _window;

        public LogInViewModel(Window logInWindow)
        {
            var app = System.Windows.Application.Current as App;
            _userAccountController = app.UserAccountController;
            _window = logInWindow;

            SignInCommand = new SignInCommand(this, _userAccountController, _window);
            RegisterCommand = new RegisterCommand(this, _userAccountController, _window);          
            
        }

    }
}
