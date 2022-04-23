﻿using Controller;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace Doctor.View
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        private TherapyRepo _therapyRepo;
        private TherapyController _therapyController;
        private ReportController _reportController;
        private string patientBind;
        private DateTime dateBind;
        private Examination selectedExam;

        public static ObservableCollection<Therapy> therapyBind
        {
            get;
            set;
        }
        
        public ReportPage(Examination exam)
        {
            InitializeComponent();
            this.DataContext = this;
            this.PatientBind = exam.PatientId;
            this.DateBind = exam.Date;
            this.selectedExam = exam;

            App app = Application.Current as App;
            _therapyRepo = app.therapyRepo;
            _therapyController = app.therapyController;
            _reportController = app.reportController;

            /*if (File.Exists(_therapyRepo.dbPath))
                _therapyRepo.LoadTherapy();*/

            therapyBind = _therapyController.findById(exam.Id);
        }

        public string PatientBind { get => patientBind; set => patientBind = value; }
        public DateTime DateBind { get => dateBind; set => dateBind = value; }
        public Examination SelectedExam { get => selectedExam; set => selectedExam = value; }

        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox.Text == "Unesi izvestaj sa pregleda...")
                txtBox.Text = string.Empty;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            string medicine = comboBoxMedicine.Text;
            int duration = Int32.Parse(textBoxDuration.Text);
            int perDay = Int32.Parse(textBoxPerDay.Text);
            bool prescription = (bool)checkBoxPrescription.IsChecked;

            Therapy therapy = new Therapy(selectedExam.Id, medicine, duration, perDay, prescription);
            _therapyController.NewTherapy(therapy);
            dataGridTherapy.ItemsSource = _therapyController.findById(selectedExam.Id);
        }

        private void AddExam_Click(object sender, RoutedEventArgs e)
        {
            AddExamination addExamination = new AddExamination(this);
            NavigationService.Navigate(addExamination);
        }

        private void SaveReport_Click(object sender, RoutedEventArgs e)
        {
            string description = textBoxDescription.Text;
            ObservableCollection<Therapy> therapies = _therapyController.findById(selectedExam.Id);

            Report report = new Report(selectedExam.Id, description, selectedExam.Date, selectedExam.PatientId, selectedExam.DoctorId, therapies);
            _reportController.NewReport(report);
        }
    }
}
