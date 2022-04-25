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

using Model;
using Controller;

namespace Admin.View
{
    /// <summary>
    /// Interaction logic for RecordRenovationWindow.xaml
    /// </summary>
    public partial class RecordRenovationWindow : Window
    {
        public Renovation renovation { get; set; }

        private RenovationController _renovationController;

        public RecordRenovationWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            _renovationController = app.renovationController;

            renovation = _renovationController.ReadAll().Last();

            renoTypeTextBox.Text = renovation.Type.ToString();
            //parcellingTextBox.Text = "";
            startDateTextBox.Text = renovation.StartDate.ToString();
            endDateTextBox.Text = renovation.EndDate.ToString();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            _renovationController.RecordRenovation(renovation.Id, signatureTextBox.Text);
            this.Close();
            this.Owner.Show();
        }

        private void discardBtn_Click(object sender, RoutedEventArgs e)
        {
            _renovationController.DeleteRenovation(renovation.Id);
            this.Close();
            this.Owner.Show();
        }
    }
}
