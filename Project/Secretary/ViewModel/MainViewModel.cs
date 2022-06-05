using Secretary.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm;
using Microsoft.Toolkit.Mvvm.Input;
using Secretary.Commands;
using HospitalMain.Controller;
using System.Threading;

namespace Secretary.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigatiorStore;

        private ViewModelBase _currentViewModel;
        private String _username;

        public String Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set { _currentViewModel = value; OnPropertyChanged(nameof(CurrentViewModel)); }
        }

        public ICommand UpdateViewCommand { get; set; }

        private DynamicEquipmentController _dynamicEquipmentController;
        private LogInViewModel _logInViewModel;

        public LogInViewModel LogInViewModel => _logInViewModel;

        public MainViewModel(MainWindow mainWindow, LogInViewModel logInViewModel)
        {
            var app = System.Windows.Application.Current as App;
            _dynamicEquipmentController = app.DynamicEquipmentController;
            
            ThreadStart ts = new ThreadStart(CheckIfEquipmentArrived);
            Thread thread = new Thread(ts);
            thread.IsBackground = true;
            thread.Start();

            _logInViewModel = logInViewModel;
            CurrentViewModel = new HomeViewModel();

            Username = logInViewModel.Username;

            UpdateViewCommand = new UpdateViewCommand(this, mainWindow);
        }

        private void CheckIfEquipmentArrived()
        {
            while (true)
            {
                _dynamicEquipmentController.CheckIfOrderArrived();

                //proverava na svakih minut
                Thread.Sleep(60 * 1000);
            }
        }
    }
}
