using Controller;
using Model;
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

namespace ClassDijagramV1._0.View.Patient
{
    /// <summary>
    /// Interaction logic for ExaminationsPatient.xaml
    /// </summary>
    public partial class ExaminationsPatient : Window
    {

        private ExamController _examController;
        private PatientController _patientController;

        public ObservableCollection<Examination> Examinations
        {
            get;
            set;
        }
        public ExaminationsPatient()
        {
            InitializeComponent();
            this.DataContext = this;

            Examinations = new ObservableCollection<Examination>();
            /*
            Equipment equipment1 = new Equipment("typeEquipment1", 2);
            List<Equipment> equipmentList1 = new List<Equipment>();
            equipmentList1.Add(equipment1);
            Room r1 = new Room("idRoom1", equipmentList1, 2, 1, false, "typeRoom1");

            List<Examination> examinationsDoctor1 = new List<Examination>();
            DateTime dtDoctor1 = DateTime.Now;
            Doctor doctor = new Doctor("idDoctor1", "nameDoctor1", "surnameDoctor1", dtDoctor1, DoctorType.Pulmonology, examinationsDoctor1);

            List<Examination> examinationsPatient1 = new List<Examination>();
            DateTime dtPatient1 = DateTime.Now;
            Model.Patient patient = new Model.Patient("idPAtient1", "namePatient1", "surnamePatient1", dtPatient1, examinationsPatient1);

            DateTime dtExam1 = DateTime.Now;
            Examination exam1 = new Examination(r1, dtExam1, "idExam1", 2, patient, doctor);
            Examinations.Add(exam1);
            */

            //zakucana vrednost posto nema logovanja jos
            string idPatient = "idPatient1";

            var app = Application.Current as App;
            _examController = app.ExamController;
            Examinations = new ObservableCollection<Examination>(FindAllExaminationsFromPatient(idPatient));
            
        }

        private IList<Examination> FindAllExaminationsFromPatient(string id)
        {
            return _examController.ReadPatientExams(id)
                .ToList();
        }
    }
}
