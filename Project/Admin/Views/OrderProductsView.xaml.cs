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
using System.Text.RegularExpressions;

using Admin.ViewModel;

namespace Admin.Views
{
    /// <summary>
    /// Interaction logic for OrderProductsView.xaml
    /// </summary>
    public partial class OrderProductsView : UserControl
    {
        public OrderProductsView()
        {
            InitializeComponent();
            this.DataContext = new OrderProductsViewModel();
        }

        private void AmountTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
