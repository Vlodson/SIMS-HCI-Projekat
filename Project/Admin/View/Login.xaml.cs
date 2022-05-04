using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

using HospitalMain.Model;
using HospitalMain.Enums;
using Controller;

namespace Admin.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private UserAccountController _userAccountController;

        private String _uid;
        public String UID
        {
            get { return _uid; }
            set
            {
                if (_uid != value)
                {
                    _uid = value;
                    OnPropertyChanged("UID");
                }
            }
        }

        private String _password;
        public String Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public Login()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            _userAccountController = app.userAccountController;

        }

        private void CanExecute_Record(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !(String.IsNullOrEmpty(UID) || String.IsNullOrEmpty(pwPasswordBox.Password));
        }

        private void Execute_Record(object sender, ExecutedRoutedEventArgs e)
        {
            if(_userAccountController.ReadUserAccount(UID) == null)
            {
                MessageBox.Show("User ID does not exist");
                return;
            }

            if (_userAccountController.CheckUserType(UID) != UserType.Admin)
            {
                MessageBox.Show("Access not allowed for this user type");
                return;
            }

            Password = pwPasswordBox.Password;
            if(!_userAccountController.LogIn(UID, Password, UserType.Admin))
            {
                MessageBox.Show("User ID or password incorrect");
                return;
            }
            else
            {
                MainMenuWindow mainMenuWindow = new MainMenuWindow();
                Application.Current.MainWindow = mainMenuWindow;
                this.Close();
                Application.Current.MainWindow.Show();
            }

        }

        private void Execute_Save(object sender, ExecutedRoutedEventArgs e)
        {
            Password = pwPasswordBox.Password;
            if(!_userAccountController.Register(UID, Password, UserType.Admin))
            {
                MessageBox.Show("Registration failed. Try different user ID");
                return;
            }
        }

        private void CanExecute_Save(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !(String.IsNullOrEmpty(UID) || String.IsNullOrEmpty(pwPasswordBox.Password));
        }
    }
}
