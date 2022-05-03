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
using System.ComponentModel;

using Model;
using Controller;

namespace Admin.View
{
    /// <summary>
    /// Interaction logic for RecordRenovationWindow.xaml
    /// </summary>
    public partial class RecordRenovationWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public Renovation renovation { get; set; }

        private RenovationController _renovationController;

        private String _signature;
        public String Signature
        {
            get { return _signature; }
            set
            {
                if(_signature != value)
                {
                    _signature = value;
                    OnPropertyChanged("Signature");
                }
            }
        }

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

        private void Execute_Save(object sender, ExecutedRoutedEventArgs e)
        {
            _renovationController.RecordRenovation(renovation.Id, Signature);
            this.Close();
            this.Owner.Show();
        }

        private void CanExecute_Save(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !String.IsNullOrEmpty(Signature);
        }

        private void discardBtn_Click(object sender, RoutedEventArgs e)
        {
            _renovationController.DeleteRenovation(renovation.Id);
            this.Close();
            this.Owner.Show();
        }
    }
}
