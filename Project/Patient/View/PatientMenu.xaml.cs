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
    /// Interaction logic for PatientMenu.xaml
    /// </summary>
    public partial class PatientMenu : Page
    {
        public PatientMenu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new ExaminationsList();
            //ExaminationsList examinationsList = new ExaminationsList();

            //ListExaminations listExaminations = new ListExaminations();
            //listExaminations.Show();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new Login();
        }

        private void Notifications_Click(object sender, RoutedEventArgs e)
        {
            Notifications notifications = new Notifications();
            notifications.Show();
        }
    }
}
