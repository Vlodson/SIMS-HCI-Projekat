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
    public class RegisterCommand : CommandBase
    {
        private LogInViewModel _logInViewModel;
        private UserAccountController _userAccountController;
        private Window _window;

        public RegisterCommand(LogInViewModel logInViewModel, UserAccountController userAccountController, Window window)
        {
            _logInViewModel = logInViewModel;
            _userAccountController = userAccountController;
            _window = window;
        }

        public override void Execute(object? parameter)
        {
            //provera da li korisnik postoji
            if (_userAccountController.CheckIfUserExist(_logInViewModel.Username))
            {
                return;
            }

            if (_userAccountController.Register(_logInViewModel.Username, _logInViewModel.Password, UserType.Secretary))
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
