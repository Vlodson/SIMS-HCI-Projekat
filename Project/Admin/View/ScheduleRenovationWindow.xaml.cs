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

using Admin.Views;

namespace Admin.View
{
    /// <summary>
    /// Interaction logic for ScheduleRenovationWindow.xaml
    /// </summary>
    public partial class ScheduleRenovationWindow : Window
    {
        public ScheduleRenovationWindow()
        {
            InitializeComponent();
            this.DataContext = new ScheduleRenovationView();
        }
    }
}
