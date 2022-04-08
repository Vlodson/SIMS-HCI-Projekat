using ClassDijagramV1._0.Controller;
using Controller;
using Model;
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

namespace ClassDijagramV1._0.View.Patient
{
    /// <summary>
    /// Interaction logic for AddExaminationPatient.xaml
    /// </summary>
    public partial class AddExaminationPatient : Window, INotifyPropertyChanged
    {

        private DoctorController _doctorController;
        private Doctor _sdoctor;
        private DateTime dateTime;
        private ExamController _examController;
        private PatientController _patientController;


        private Doctor doctor;
        public List<DateTime> freeExaminationsForDoctor;

        
        public ObservableCollection<Doctor> DoctorsObs
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public DateTime DateTimePRoperty
        {
            get { return dateTime; }
            set {
                if (value != dateTime)
                {
                    dateTime = value;
                    NotifyPropertyChanged("DateTimePr");
                }
            }
        }
        public List<DateTime> FreeExaminationsForDoctor
        {
            get
            {
                return freeExaminationsForDoctor;
            }
            set
            {
                if(value != freeExaminationsForDoctor)
                {
                    freeExaminationsForDoctor = value;
                    NotifyPropertyChanged("FreeExaminationsForDoctor");
                }
            }
        }

        

        public AddExaminationPatient()
        {
            InitializeComponent();
            this.DataContext = this;

            //DoctorsObs = new ObservableCollection<Doctor>();

            var app = Application.Current as App;
            _doctorController = app.DoctorController;
            _examController = app.ExamController;
            _patientController = app.PatientController;
            
            DoctorsObs = new ObservableCollection<Doctor>(FindAllDoctors());

            //freeExaminationsForDoctor = new List<DateTime>();
            freeExaminationsForDoctor = new List<DateTime>();
            freeExaminationsForDoctor.Add(DateTime.Now);
            //freeExaminationsForDoctor.Add(DateTime.Now);


        }

        

        

        private IList<Doctor> FindAllDoctors()
        {
            return _doctorController.GetAll()
                .ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            
            Model.Patient patient = _patientController.ReadPatient("idPatient1");
            DateTime dt1 = new DateTime(2021, 5, 5, 12, 12, 12);
            Room r1 = new Room("idRoom", new List<Equipment>(), 1, 1, false, "type1");
            Doctor doctor1 = new Doctor("idDoctor1", "nameDoctor1", "surnameDoctor1", new DateTime(1985,12,12), DoctorType.Pulmonology, new List<Examination>());
            Examination newExam = new Examination(r1, new DateTime(2012, 12, 12), "idExam", 2, patient, doctor1);
            _examController.PatientCreateExam(newExam);
            
        }

        private List<DateTime> findFreeExaminations(Doctor doc)
        {
            return _doctorController.GetFreeGetFreeExaminations(doc);
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            doctor = doctorsCombo.SelectedItem as Doctor;
            freeExaminationsForDoctor = findFreeExaminations(doctor);
            
        }
    }
}
