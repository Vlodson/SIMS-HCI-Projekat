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
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private UserAccountController _userAccountController;
        public Registration()
        {
            InitializeComponent();
            App app = Application.Current as App;
            _userAccountController = app.UserAccountController;
        }

        private void RegistrationClick(object sender, RoutedEventArgs e)
        {
            if(username.Text.Equals("") || password.Text.Equals("")){

            }else
            {
                if(_userAccountController.Register(username.Text, password.Text, HospitalMain.Enums.UserType.Patient) == true)
                {
                    this.Close();
                }
            }
        }
    }
}
