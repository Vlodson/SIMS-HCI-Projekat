﻿using Controller;
using Model;
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
using System.Windows.Shapes;

namespace Patient.View
{
    /// <summary>
    /// Interaction logic for AddExamination.xaml
    /// </summary>
    public partial class AddExamination : Window
    {
        private ExamController _examController;
        private DoctorController _doctorController;
        private PatientController _patientController;
        private ExaminationRepo _examinationRepo;

        public int id = 0;

        

        public ObservableCollection<Doctor> DoctorsObs
        {
            get;
            set;
        }
        

        private static Random random = new Random((int)DateTime.Now.Ticks);
        private string RandomString(int Size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < Size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }
        public AddExamination()
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            _examController = app.ExamController;
            _doctorController = app.DoctorController;
            _patientController = app.PatientController;
            _examinationRepo = app.ExaminationRepo;

            DoctorsObs = new ObservableCollection<Doctor>(FindAllDoctors());

        }

        private IList<Doctor> FindAllDoctors()
        {
            return _doctorController.GetAll()
                .ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(DoctorCombo.SelectedIndex != -1)
            {
                this.DataContext = this;
                Doctor doctor = (Doctor)DoctorCombo.SelectedItem;
                ExamsAvailable.ItemsSource = _doctorController.GetFreeGetFreeExaminations(doctor);
                
            }
            
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            if(ExamsAvailable.SelectedIndex != -1)
            {
                Model.Patient patient = _patientController.ReadPatient("2");
                Doctor doctor = (Doctor)DoctorCombo.SelectedItem;
                Examination selectedExamination = (Examination)ExamsAvailable.SelectedItem;
                DateTime dt = selectedExamination.Date;
                id++;
                //String idExam = "idExam" + id.ToString();
                Examination newExam = new Examination(new Room("name", 1, 1, false, "type"), dt, RandomString(5), 2,"pregled", patient, doctor);
                _examController.PatientCreateExam(newExam);
                //_examController.ReadPatientExams("idPatient1").Add(newExam);
                //MainWindow.Examinations.Add(newExam);
                _examinationRepo.SaveExamination();
                this.Close();
            }

        }
    }
}
