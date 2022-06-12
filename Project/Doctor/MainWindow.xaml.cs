using Controller;
using Doctor.View;
using Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Doctor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
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
        private UserAccountRepo _userAccountRepo;

        public static string _uid;
        public string UID
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
        private bool _isFirst;
        public bool IsFirst
        {
            get { return _isFirst; }
            set
            {
                if (_isFirst != value)
                {
                    _isFirst = value;
                    OnPropertyChanged("IsFirst");
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

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            _userAccountController = app.userAccountController;
            _userAccountRepo = app.userAccountRepo;

           UsernameTxt.Focus();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            UID = UsernameTxt.Text;
            Password = PasswordTxt.Password;
            if (_userAccountController.LogIn(UID, Password, HospitalMain.Enums.UserType.Doctor))
            {
                if (_userAccountController.ReadUserAccount(UID).IsFirst)
                {
                    var win = new WizardWindow();
                    win.ShowDialog();
                    DoctorNavBar doctorNavBar = new DoctorNavBar();
                    doctorNavBar.Show();
                } else
                {
                    DoctorNavBar doctorNavBar = new DoctorNavBar();
                    doctorNavBar.Show();
                }
            }
            else
            {
                MessageBox.Show("Pogresno korisnicko ime ili lozinka!");
                return;
            }
            _userAccountController.ReadUserAccount(UID).IsFirst = false;
            _userAccountRepo.SaveUserAccounts();
            this.Close();
            
        }
    }
}
