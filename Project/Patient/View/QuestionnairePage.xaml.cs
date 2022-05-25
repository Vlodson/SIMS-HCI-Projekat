using Controller;
using HospitalMain.Model;
using Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Patient.View
{
    /// <summary>
    /// Interaction logic for QuestionnairePage.xaml
    /// </summary>
    public partial class QuestionnairePage : Page
    {
        public List<String> HospitalQuestionnary { get; set; }
        public List<String> DoctorQuestionnary { get; set; }

        public List<Doctor> DoctorsAvailable { get; set; }
        public PatientController _patientController;
        public DoctorController _doctorController;
        MedicalRecordController _medicalRecordController;

        public QuestionnairePage()
        {
            InitializeComponent();
            App app = Application.Current as App;
            _patientController = app.PatientController;
            _doctorController = app.DoctorController;
            _medicalRecordController = app.MedicalRecordController;

            HospitalQuestionnary = _patientController.GetHospitalQuestionnaire().Questions;

            bolnica1.Content = HospitalQuestionnary[0];
            bolnica2.Content = HospitalQuestionnary[1];
            bolnica3.Content = HospitalQuestionnary[2];
            bolnica4.Content = HospitalQuestionnary[3];
            bolnica5.Content = HospitalQuestionnary[4];

            DoctorQuestionnary = _patientController.GetDoctorQuestionnaire().Questions;
            doktor1.Content = DoctorQuestionnary[0];
            doktor2.Content = DoctorQuestionnary[1];
            doktor3.Content = DoctorQuestionnary[2];
            doktor4.Content = DoctorQuestionnary[3];

            DoctorsAvailable = new List<Doctor>();
            MedicalRecord medicalRecord = _medicalRecordController.GetMedicalRecord(Login.loggedId);
            foreach(Report report in medicalRecord.Reports)
            {
                if (!DoctorsAvailable.Contains(_doctorController.GetDoctor(report.DoctorId))) DoctorsAvailable.Add(_doctorController.GetDoctor(report.DoctorId));
            }
            //foreach(String idDoctor in _patientController.GetPatientsDoctors(Login.loggedId))
            //{
            //    DoctorsAvailable.Add(_doctorController.GetDoctor(idDoctor));
            //}

            List<String> names = new List<String>();
            foreach(Doctor doctor in DoctorsAvailable)
            {
                names.Add(doctor.Name + " " + doctor.Surname);
            }
            Doctors.ItemsSource = names;
        }

        private void Add_Answers(object sender, RoutedEventArgs e)
        {
            ErrorMessage.Visibility = Visibility.Hidden;
            List<int> answers = new List<int>();
            if (hospital11.IsChecked == true)
            {
                answers.Add(1);
            }else if(hospital12.IsChecked == true)
            {
                answers.Add(2);
            }else if(hospital13.IsChecked == true)
            {
                answers.Add(3);
            }else if(hospital14.IsChecked == true)
            {
                answers.Add(4);
            }
            else
            {
               answers.Add(5);
            }

            if (hospital21.IsChecked == true)
            {
                answers.Add(1);
            }
            else if (hospital22.IsChecked == true)
            {
                answers.Add(2);
            }
            else if (hospital23.IsChecked == true)
            {
                answers.Add(3);
            }
            else if (hospital24.IsChecked == true)
            {
                answers.Add(4);
            }
            else
            {
                answers.Add(5);
            }

            if (hospital31.IsChecked == true)
            {
                answers.Add(1);
            }
            else if (hospital32.IsChecked == true)
            {
                answers.Add(2);
            }
            else if (hospital33.IsChecked == true)
            {
                answers.Add(3);
            }
            else if (hospital34.IsChecked == true)
            {
                answers.Add(4);
            }
            else
            {
                answers.Add(5);
            }

            if (hospital41.IsChecked == true)
            {
                answers.Add(1);
            }
            else if (hospital42.IsChecked == true)
            {
                answers.Add(2);
            }
            else if (hospital43.IsChecked == true)
            {
                answers.Add(3);
            }
            else if (hospital44.IsChecked == true)
            {
                answers.Add(4);
            }
            else
            {
                answers.Add(5);
            }

            if (hospital51.IsChecked == true)
            {
                answers.Add(1);
            }
            else if (hospital52.IsChecked == true)
            {
                answers.Add(2);
            }
            else if (hospital53.IsChecked == true)
            {
                answers.Add(3);
            }
            else if (hospital54.IsChecked == true)
            {
                answers.Add(4);
            }
            else
            {
                answers.Add(5);
            }

            Answer hospitalAnswer = new Answer("hospital", answers, 0);
            _patientController.AddAnswer(Login.loggedId, hospitalAnswer);
            QuestionnairesAccepted questionnairesAccepted = new QuestionnairesAccepted();
            questionnairesAccepted.ShowDialog();
        }

        private void AddDoctorAnswer(object sender, RoutedEventArgs e)
        {
            if(Doctors.SelectedIndex != -1)
            {
                ErrorMessage.Visibility = Visibility.Hidden;
                List<int> answers = new List<int>();
                if (doctor11.IsChecked == true)
                {
                    answers.Add(1);
                }
                else if (doctor12.IsChecked == true)
                {
                    answers.Add(2);
                }
                else if (doctor13.IsChecked == true)
                {
                    answers.Add(3);
                }
                else if (doctor14.IsChecked == true)
                {
                    answers.Add(4);
                }
                else
                {
                    answers.Add(5);
                }

                if (doctor21.IsChecked == true)
                {
                    answers.Add(1);
                }
                else if (doctor22.IsChecked == true)
                {
                    answers.Add(2);
                }
                else if (doctor23.IsChecked == true)
                {
                    answers.Add(3);
                }
                else if (doctor24.IsChecked == true)
                {
                    answers.Add(4);
                }
                else
                {
                    answers.Add(5);
                }

                if (doctor31.IsChecked == true)
                {
                    answers.Add(1);
                }
                else if (doctor32.IsChecked == true)
                {
                    answers.Add(2);
                }
                else if (doctor33.IsChecked == true)
                {
                    answers.Add(3);
                }
                else if (doctor34.IsChecked == true)
                {
                    answers.Add(4);
                }
                else
                {
                    answers.Add(5);
                }

                if (doctor41.IsChecked == true)
                {
                    answers.Add(1);
                }
                else if (doctor42.IsChecked == true)
                {
                    answers.Add(2);
                }
                else if (doctor43.IsChecked == true)
                {
                    answers.Add(3);
                }
                else if (doctor44.IsChecked == true)
                {
                    answers.Add(4);
                }
                else
                {
                    answers.Add(5);
                }

                int index = Doctors.SelectedIndex;
                Doctor doctor = DoctorsAvailable[index];
                if (_patientController.CheckAnswerAvailable(doctor.Id, _medicalRecordController.GetMedicalRecord(Login.loggedId)))
                {
                    Answer doctorAnswer = new Answer(doctor.Id, answers, 0);
                    _patientController.AddAnswer(Login.loggedId, doctorAnswer);
                    QuestionnairesAccepted questionnairesAccepted = new QuestionnairesAccepted();
                    questionnairesAccepted.ShowDialog();
                }
                else
                {
                    ErrorMessage.Content = "Dodate su sve moguće ocene za ovog lekara";
                    ErrorMessage.Visibility = Visibility.Visible;
                }
                
            }
            else
            {
                ErrorMessage.Content = "Morate izabrati lekara";
                ErrorMessage.Visibility = Visibility.Visible;
            }
            
        }

        private void MenuClick(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new PatientMenu();
        }
    }
}
