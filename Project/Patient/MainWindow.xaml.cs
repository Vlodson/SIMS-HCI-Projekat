using Controller;
using Model;
using Patient.View;
using Repository;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Patient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ExaminationRepo _examinationRepo;

        public MainWindow()
        {
            InitializeComponent();
            App app = Application.Current as App;
            _examinationRepo = app.ExaminationRepo;
            Main.Content = new Login();
        }

        

        private void Window_Closed(object sender, EventArgs e)
        {
            _examinationRepo.SaveExamination();
        }

        private void ListExaminations_Click(object sender, RoutedEventArgs e)
        {
            ListExaminations listExaminations = new ListExaminations();
            listExaminations.Show();
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ExaminationsList();
            //patientMenu.ShowsNavigationUI;
        }
    }
}
