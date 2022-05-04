using Controller;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Patient.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private UserAccountController _userAccountController;
        private PatientController _patientController;

        public static String loggedId;
        public Login()
        {
            InitializeComponent();
            App app = Application.Current as App;
            _userAccountController = app.UserAccountController;
            _patientController = app.PatientController;
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            if(username.Text.Equals("") || password.Password.ToString().Equals(""))
            {

            }
            else
            {
                if(_userAccountController.LogIn(username.Text, password.Password.ToString(), HospitalMain.Enums.UserType.Patient) == true)
                {
                    loggedId = _patientController.ReadPatient(username.Text).ID;
                    Window.GetWindow(this).Content = new PatientMenu();
                }        
            }
            
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
    }
}
