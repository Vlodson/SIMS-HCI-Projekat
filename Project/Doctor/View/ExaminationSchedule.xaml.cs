using Controller;
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
using System.Windows.Shapes;

namespace Doctor.View
{
    /// <summary>
    /// Interaction logic for ExaminationSchedule.xaml
    /// </summary>
    public partial class ExaminationSchedule : Window
    {
        private ExamController _examController;
        private ExaminationRepo _examRepo;

        public static Examination SelectedItem
        {
            get;
            set;
        }

        public static ObservableCollection<Examination> Examinations
        {
            get;
            set;
        }

        public ExaminationSchedule()
        {
            InitializeComponent();
            this.DataContext = this;
            SelectedItem = null;

            

            string idDoc = "IDDOC";

            App app = Application.Current as App;
            _examRepo = app.ExaminationRepo;
            _examController = app.ExamController;
            if (File.Exists(_examRepo.dbPath))
                _examRepo.LoadExamination();

            Examinations = _examController.ReadDoctorExams(idDoc);
            //Equipment equipment1 = new Equipment("typeEquipment1", 2);

            /*Room r1 = new Room("idRoom1", 2, 1, false, "typeRoom1");
            List<Examination> examinationsDoctor1 = new List<Examination>();
            DateTime dtDoctor1 = DateTime.Now;
            Model.Doctor doctor = new Model.Doctor("idDoctor1", "nameDoctor1", "surnameDoctor1", dtDoctor1, DoctorType.Pulmonology, examinationsDoctor1);
            List<Examination> examinationsPatient1 = new List<Examination>();
            DateTime dtPatient1 = DateTime.Now;
            Model.Patient patient = new Model.Patient("idPAtient1", "namePatient1", "surnamePatient1", dtPatient1, examinationsPatient1);
            DateTime dtExam1 = DateTime.Now;
            Examination exam1 = new Examination(r1, dtExam1, "idExam1", 2, "operacija", patient, doctor);
            Examinations.Add(exam1);*/
            

        }

        private void Zakazi_Click(object sender, RoutedEventArgs e)
        {
            AddExamination addExamination = new AddExamination();
            addExamination.Show();
            
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            SelectedItem = dataGridExaminations.SelectedItem as Examination;
            UpdateExamination updateExamination = new UpdateExamination(SelectedItem);
            updateExamination.Show();

        }


        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            Examination selectedItem = dataGridExaminations.SelectedItem as Examination;
            if (selectedItem != null)
            {
                _examController.DoctorRemoveExam(selectedItem);
                Examinations.Remove(selectedItem);
                convertEntityToView();
            }

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _examRepo.SaveExamination();
        }

        public void convertEntityToView()
        {
            // stupid slow but i dont really care right now also probably needs a check to see if its null
            Examinations.Clear();
            string idDoc = "IDDOC";
            List<Examination> exams = this._examController.ReadAll(idDoc);
            foreach (Examination exam in exams)
                Examinations.Add(exam);
        }
    }
}
