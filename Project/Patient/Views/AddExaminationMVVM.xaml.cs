using Patient.ViewModel;
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

namespace Patient.Views
{
    /// <summary>
    /// Interaction logic for AddExaminationMVVM.xaml
    /// </summary>
    public partial class AddExaminationMVVM : Window
    {
        public AddExaminationMVVM(DateTime sDate)
        {
            InitializeComponent();
            this.DataContext = new AddExaminationViewModel(sDate, this);
        }

        
    }
}
