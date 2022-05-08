using Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Patient.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private UserAccountController _userAccountController;
        private PatientController _patientController;

        private String usernameInput;
        private String Username
        {
            get
            {
                return usernameInput;
            }
            set
            {
                usernameInput = value;
                OnPropertyChanged("Username");
            }
        }

        public static String loggedId;
        public Login()
        {
            InitializeComponent();
            App app = Application.Current as App;
            _userAccountController = app.UserAccountController;
            _patientController = app.PatientController;
            Username = username.Text;
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            //if(String.IsNullOrEmpty(username.Text))
            //{
            //    UsernameError.Visibility = Visibility.Visible;
            //}else if (password.Password.ToString().Length == 0)
            //{
            //    PasswordError.Visibility = Visibility.Visible;
            //}
            //else
            //{
            if (_userAccountController.LogIn(username.Text, password.Password.ToString(), HospitalMain.Enums.UserType.Patient) == true)
            {
                loggedId = _patientController.ReadPatient(username.Text).ID;
                Window.GetWindow(this).Content = new PatientMenu();
            }
            else
            {
                AccountValidation.Visibility = Visibility.Visible;
            }
            //}

        }
        private void ListExaminations_Click(object sender, RoutedEventArgs e)
        {
            
            //patientMenu.ShowsNavigationUI;
        }

        private void RegistrationForm(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
        }

        private void CheckUsername(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(username.Text))
            {
                UsernameValidation.Visibility = Visibility.Visible;
            }
            else
            {
                UsernameValidation.Visibility = Visibility.Hidden;
                AccountValidation.Visibility = Visibility.Hidden;
            }
        }

        private void CheckPassword(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(password.Password.ToString()))
            {
                PasswordValidation.Visibility = Visibility.Visible;
            }
            else
            {
                PasswordValidation.Visibility = Visibility.Hidden;
                AccountValidation.Visibility = Visibility.Hidden;
            }
        }
    }
}
