using HospitalMain.Controller;
using HospitalMain.Enums;
using Secretary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Secretary.Commands
{
    public class SignInCommand : CommandBase
    {
        private LogInViewModel _logInViewModel;
        private UserAccountController _userAccountController;
        private Window _window;
        
        public SignInCommand(LogInViewModel logInViewModel, UserAccountController userAccountController, Window logInWindow)
        {
            _logInViewModel = logInViewModel;
            _window = logInWindow;
            _userAccountController = userAccountController;
        }

        public override void Execute(object? parameter)
        {
            //provera da li korisnik postoji
            if(_userAccountController.CheckIfUserExist(_logInViewModel.Username) == false)
            {
                return;
            }

            //provera da li je korisnik sekretar
            if (_userAccountController.CheckUserType(_logInViewModel.Username) != UserType.Secretary)
            {
                return;
            }

            if (_userAccountController.LogIn(_logInViewModel.Username, _logInViewModel.Password, _logInViewModel.Type))
            {
                //otvaranje aplikacije
                MainWindow _mainWindow = new MainWindow();
                Application.Current.MainWindow = _mainWindow;
                _mainWindow.Show();

                //zatvaranje logIn prozora
                _window.Close();
            }
        }
    }
}
