using System;
using System.Collections.ObjectModel;
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
using System.IO;

using Model;
using Utility;
using Controller;
using HospitalMain.Enums;
using Admin.ViewModel;

namespace Admin.View
{
    /// <summary>
    /// Interaction logic for EquipmentTableWindow.xaml
    /// </summary>
    public partial class TableWindow : Window
    {
        public TableWindow()
        {
            InitializeComponent();
            this.DataContext = new TableWindowViewModel();
        }
    }
}
