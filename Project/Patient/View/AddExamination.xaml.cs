﻿using Controller;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class AddExamination : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private ExamController _examController;
        private DoctorController _doctorController;
        private PatientController _patientController;
        private ExaminationRepo _examinationRepo;

        public int id = 0;
        private String doctorNameSurname;

        
        

        public ObservableCollection<Doctor> DoctorsObs
        {
            get;
            set;
        }

        public static DateTime startDate;
        private DateTime endDate;
        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
                OnPropertyChanged("StartDate");
            }
        }
        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
                OnPropertyChanged("EndDate");
            }
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
            StartDate = DateTime.Now.AddDays(1);
            EndDate = DateTime.Now.AddDays(7);

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
                //DateTime startDate = (DateTime)Start.SelectedDate;
                StartDate = (DateTime)Start.SelectedDate;
                //DateTime endDate = (DateTime)End.SelectedDate;
                EndDate = (DateTime)End.SelectedDate;

                bool priority = true; //ako je doktor onda je true, termin false
                if(doctorPriority.IsChecked == true)
                {
                    priority = true;
                }
                else
                {
                    priority = false;
                }
                
                List<Examination> listExaminations = _doctorController.GetFreeGetFreeExaminations(doctor, startDate, endDate, priority);
                foreach(Examination exam in listExaminations)
                {
                    exam.DoctorNameSurname = _doctorController.GetDoctor(doctor.Id).NameSurname;
                }
                //ExamsAvailable.ItemsSource = _doctorController.GetFreeGetFreeExaminations(doctor, startDate, endDate, priority);
                ExamsAvailable.ItemsSource = listExaminations;
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
<<<<<<< HEAD
                Examination newExam = new Examination(new Room("name", 1, 1, false, HospitalMain.Enums.RoomTypeEnum.Patient_Room), dt, RandomString(5), 2, HospitalMain.Enums.ExaminationTypeEnum.OrdinaryExamination, patient, doctor);
=======
                Room r1 = new Room("name", 1, 1, false, HospitalMain.Enums.RoomTypeEnum.Patient_Room);
                Examination newExam = new Examination(r1.Id, dt, RandomString(5), 2,HospitalMain.Enums.ExaminationTypeEnum.OrdinaryExamination, patient.ID, doctor.Id);
>>>>>>> 4cee4ae3c692b9c948d058c06accb897b183b6a5
                _examController.PatientCreateExam(newExam);
                //_examController.ReadPatientExams("idPatient1").Add(newExam);
                //MainWindow.Examinations.Add(newExam);
                _examinationRepo.SaveExamination();
                ObservableCollection<Examination> examinations = _examController.ReadPatientExams("2");
                foreach (Examination exam in examinations)
                {
                    //exam.DoctorType = _doctorController.GetDoctor(exam.DoctorId).Type;
                    exam.DoctorNameSurname = _doctorController.GetDoctor(exam.DoctorId).NameSurname;
                }
                ExaminationsList.Examinations = examinations;
                this.Close();
            }

        }
    }
}
