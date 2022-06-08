using Secretary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Secretary.Commands
{
    public class UpdateViewCommand : CommandBase
    {
        private MainViewModel _mainViewModel;
        private MainWindow _mainWindow;

        public UpdateViewCommand(MainViewModel mainViewModel, MainWindow mainWindow)
        {
            _mainViewModel = mainViewModel;
            _mainWindow = mainWindow;
        }

        public override void Execute(object? parameter)
        {
            if(parameter.ToString() == "Book")
            {
                _mainViewModel.CurrentViewModel = new BookViewModel(_mainViewModel);
            } 
            else if(parameter.ToString() == "UserAccounts")
            {
                _mainViewModel.CurrentViewModel = new AccountsViewModel();
            } 
            else if (parameter.ToString() == "MedicalRecords")
            {
                _mainViewModel.CurrentViewModel = new MedicalRecordsViewModel();
            }
            else if (parameter.ToString() == "Emergency")
            {
                _mainViewModel.CurrentViewModel = new EmergencyGeneralViewModel();
            } else if(parameter.ToString() == "Requests")
            {
                _mainViewModel.CurrentViewModel = new RequestsViewModel(_mainViewModel);
            } else if(parameter.ToString() == "Logout")
            {
                LogInWindow _loginWindow = new LogInWindow();
                Application.Current.MainWindow = _loginWindow;
                _loginWindow.Show();

                _mainWindow.Close();
            } else if(parameter.ToString() == "HomePage")
            {
                _mainViewModel.CurrentViewModel = new HomeViewModel();
            } else if(parameter.ToString() == "Izvestaj")
            {
                _mainViewModel.CurrentViewModel = new RoomOccupancyReportViewModel(_mainViewModel);
            }
        }
    }
}
