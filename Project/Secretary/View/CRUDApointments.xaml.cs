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
using Secretary.View;
using Controller;
using System.Collections.ObjectModel;
using Model;
using Repository;
using Secretary.ViewModel;

namespace Secretary.View
{
    /// <summary>
    /// Interaction logic for CRUDApointments.xaml
    /// </summary>
    public partial class CRUDApointments : Window
    {
        public CRUDApointments()
        {
            InitializeComponent();
            this.DataContext = new CRUDAppointmentsViewModel();

        }

    }
}
