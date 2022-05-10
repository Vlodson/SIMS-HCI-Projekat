using Controller;
using HospitalMain.Model;
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
        public PatientController _patientController;

        public QuestionnairePage()
        {
            InitializeComponent();
            App app = Application.Current as App;
            _patientController = app.PatientController;
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
            doktor5.Content = DoctorQuestionnary[4];
        }

        private void Add_Answers(object sender, RoutedEventArgs e)
        {
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

            Answer hospitalAnswer = new Answer("hospital", answers);
            _patientController.AddAnswer(Login.loggedId, hospitalAnswer);
        }

        private void AddDoctorAnswer(object sender, RoutedEventArgs e)
        {

        }
    }
}
