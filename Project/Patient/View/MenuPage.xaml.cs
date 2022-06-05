using Controller;
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
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        private PatientController _patientController;
        private MedicalRecordController _medicalRecordController;
        private ExamController _examinationController;
        private DoctorController _doctorController;
        private DoctorRepo _doctorRepo;


        public static ObservableCollection<Examination> Examinations
        {
            get;
            set;
        }

        public static List<Examination> ExaminationsForDate
        {
            get;
            set;
        }

        public static List<DateOnly> DatesExaminations
        {
            get;
            set;
        }
        public MenuPage()
        {
            InitializeComponent();
            App app = Application.Current as App;
            _patientController = app.PatientController;
            _medicalRecordController = app.MedicalRecordController;
            _examinationController = app.ExamController;
            _doctorController = app.DoctorController;
            _doctorRepo = app.DoctorRepo;

            ObservableCollection<Examination> examinations = _examinationController.ReadPatientExams(Login.loggedId);
            foreach (Examination exam in examinations)
            {
                exam.DoctorNameSurname = _doctorController.GetDoctor(exam.DoctorId).NameSurname;
            }
            Examinations = examinations;

            DateTime today = DateTime.Now;
            ExaminationsForDate = new List<Examination>();
            DatesExaminations = new List<DateOnly>();
            MenuCalendar.SelectedDate = DateTime.Now;
            foreach (Examination exam in examinations)
            {
                if (exam.Date.Date == today.Date)
                {
                    if (exam.DoctorType == DoctorType.Pulmonology)
                    {
                        exam.DoctorTypeString = "Pulmologija";
                    }
                    else if (exam.DoctorType == DoctorType.Cardiology)
                    {
                        exam.DoctorTypeString = "Kardiologija";
                    }
                    else
                    {
                        exam.DoctorTypeString = "Opšta praksa";
                    }
                    ExaminationsForDate.Add(exam);
                }
                if (!DatesExaminations.Contains(DateOnly.FromDateTime(exam.Date)))
                {
                    DatesExaminations.Add(DateOnly.FromDateTime(exam.Date));

                }
            }
            MenuCalendar.DataContext = DatesExaminations;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Window.GetWindow(this).Content = new ExaminationsList();
            //Menu.Content = new ExaminationsList();

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new Login();
        }

        private void calendarButton_Loaded(object sender, EventArgs e)
        {
            CalendarDayButton button = (CalendarDayButton)sender;
            DateTime date = (DateTime)button.DataContext;
            HighlightDay(button, date);
            button.DataContextChanged += new DependencyPropertyChangedEventHandler(calendarButton_DataContextChanged);
        }

        private void HighlightDay(CalendarDayButton button, DateTime date)
        {
            if (DatesExaminations.Contains(DateOnly.FromDateTime(date)))
                button.Background = Brushes.LightBlue;
            else
                button.Background = Brushes.White;
        }

        private void calendarButton_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CalendarDayButton button = (CalendarDayButton)sender;
            DateTime date = (DateTime)button.DataContext;
            HighlightDay(button, date);
        }

        private void GradingsClick(object sender, RoutedEventArgs e)
        {
            //Menu.Content = new Questionnaires();
            //Menu.Content = new QuestionnairePage();
        }

        private void MedicalREcordClick(object sender, RoutedEventArgs e)
        {
           //Menu.Content = new MedicalRecordMVVM();
        }

        private void MenuClick(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new PatientMenu();
        }

        private void ReportClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
