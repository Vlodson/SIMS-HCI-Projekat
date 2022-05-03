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

namespace Admin.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private String _uid;
        public String UID
        {
            get { return _uid; }
            set
            {
                if (_uid != value)
                {
                    _uid = value;
                    OnPropertyChanged("UID");
                }
            }
        }

        private String _password;
        public String Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public Login()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void CanExecute_Record(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !(String.IsNullOrEmpty(UID) || String.IsNullOrEmpty(Password));
        }

        private void Execute_Record(object sender, ExecutedRoutedEventArgs e)
        {
        }

        private void Execute_Save(object sender, ExecutedRoutedEventArgs e)
        {
        }

        private void CanExecute_Save(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !(String.IsNullOrEmpty(UID) || String.IsNullOrEmpty(Password));
        }

        private void discardBtn_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
