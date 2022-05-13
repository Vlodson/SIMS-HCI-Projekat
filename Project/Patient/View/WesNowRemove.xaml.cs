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
    /// Interaction logic for WesNowRemove.xaml
    /// </summary>
    public partial class WesNowRemove : Window
    {

        public WesNowRemove()
        {
            InitializeComponent();
            ExaminationsList.remove = false;
        }

        private void YesRemoveClick(object sender, RoutedEventArgs e)
        {
            ExaminationsList.remove = true;
            this.Close();
        }

        private void NoRemoveClick(object sender, RoutedEventArgs e)
        {
            ExaminationsList.remove = false;
            this.Close();
        }
    }
}
