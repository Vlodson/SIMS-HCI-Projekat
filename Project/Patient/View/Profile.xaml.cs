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
using System.Windows.Shapes;

namespace Patient.View
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        private PatientController _patientController;
        private UserAccountController _userAccountController;
        

        public Profile()
        {
            InitializeComponent();
            App app = Application.Current as App;
            _patientController = app.PatientController;
            _userAccountController = app.UserAccountController;

            Model.Patient patient = _patientController.ReadPatient(Login.loggedId);
            Username.Content = patient.ID;
            Name.Content = patient.Name;
            Surname.Content = patient.Surname;


        }

        private void ValidateClick(object sender, RoutedEventArgs e)
        {
            if (oldPassword == null)
            {
                error.Content = "Morate uneti staru lozinku";
                error.Visibility = Visibility.Visible;
            }else if (!oldPassword.Equals(_userAccountController.ReadUserAccount(Login.loggedId)))
            {
                error.Content = "Neispravna stara lozinka";
                error.Visibility = Visibility.Visible;
            }
            else if(newPassword == null)
            {
                error.Content = "Morate uneti novu lozinku";
                error.Visibility = Visibility.Visible;
            }
            else
            {
                error.Visibility = Visibility.Hidden;
                this.Close();
            }
        }
    }
}
