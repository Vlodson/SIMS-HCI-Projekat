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
using HospitalMain.Repository;

namespace Patient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ExaminationRepo _examinationRepo;
        private DoctorRepo _doctorRepo;
        private QuestionnaireRepo _questionnaireRepo;
        private PatientRepo _patientRepo;

        public MainWindow()
        {
            InitializeComponent();
            App app = Application.Current as App;
            _examinationRepo = app.ExaminationRepo;
            _doctorRepo = app.DoctorRepo;
            _questionnaireRepo = app.QuestionnaireRepo;
            _patientRepo = app.PatientRepo;
            Main.Content = new Login();
        }

        

        private void Window_Closed(object sender, EventArgs e)
        {
            _examinationRepo.SaveExamination();
            _doctorRepo.SaveDoctor();
            _questionnaireRepo.SaveQuestionnaire();
            _patientRepo.SavePatient();
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
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new Login();
        }

        private void Notifications_Click(object sender, RoutedEventArgs e)
        {
            Notifications notifications = new Notifications();
            notifications.ShowDialog();
        }
    }
}
