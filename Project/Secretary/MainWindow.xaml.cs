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
using Secretary.View;
using System.Diagnostics;
using Secretary.Stores;
using Secretary.Commands;

namespace Secretary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ICommand CRUDAppointmentsCommand { get;}

        public MainWindow()
        {
            InitializeComponent();

            //CRUDAppointmentsCommand = new NavigationCommand(navigationStore);
        }

        private void CRUDAcc_Click(object sender, RoutedEventArgs e)
        {
            CRUDAccountOptions crudAO = new CRUDAccountOptions();
            crudAO.Show();
        }

        private void CRUDMedRecords_Click(object sender, RoutedEventArgs e)
        {
            CRUDMedicalRecord crudMedRecords = new CRUDMedicalRecord();
            crudMedRecords.Show();
        }

        private void CRUDAppointments_Click(object sender, RoutedEventArgs e)
        {
            CRUDApointments crudAppointments = new CRUDApointments();
            crudAppointments.Show();
        }
    }
}
