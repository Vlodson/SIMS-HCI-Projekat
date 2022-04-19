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

namespace Doctor.View
{
    /// <summary>
    /// Interaction logic for DoctorNavBar.xaml
    /// </summary>
    public partial class DoctorNavBar : Window
    {
        public DoctorNavBar()
        {
            InitializeComponent();
        }

        private void ButtonSchedule(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonExaminations(object sender, RoutedEventArgs e)
        {
            Main.Content = new EndedExaminations();
        }

        private void ButtonRequests(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonPatients(object sender, RoutedEventArgs e)
        {
            Main.Content = new Patients();
        }

        private void ButtonNotifications(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonLogOut(object sender, RoutedEventArgs e)
        {

        }


    }
}
