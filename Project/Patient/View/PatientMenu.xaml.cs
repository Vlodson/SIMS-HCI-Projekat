using Controller;
using HospitalMain.Controller;
using HospitalMain.Model;
using HospitalMain.Repository;
using Model;
using Patient.Views;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for PatientMenu.xaml
    /// </summary>
    public partial class PatientMenu : Page
    {
        private PatientController _patientController;
        private MedicalRecordController _medicalRecordController;
        private ExamController _examinationController;
        private DoctorController _doctorController;
        private DoctorRepo _doctorRepo;
        private PersonalNotificationController _personalNotificationController;
        

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
        public PatientMenu()
        {
            InitializeComponent();
            App app = Application.Current as App;
            _patientController = app.PatientController;
            _medicalRecordController = app.MedicalRecordController;
            _examinationController = app.ExamController;
            _doctorController = app.DoctorController;
            _doctorRepo = app.DoctorRepo;
            _personalNotificationController = app.personalNotificationController;
            
            
            _doctorRepo.SaveDoctor();

            
            //TimerCallback timerDelegate = new TimerCallback(CheckStatus);

            //Timer t = new Timer(timerDelegate, null, 0, 0);

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
            //dataGridExaminations.ItemsSource = ExaminationsForDate;


                //MyMethod();
                //List<HospitalMain.Model.PersonalNotification> personalNotificationList = _personalNotificationController.GetPatientPersonalNotifications(Login.loggedId);
                //foreach (HospitalMain.Model.PersonalNotification personalNotification in personalNotificationList)
                //{
                //    if (personalNotification.Status == true && personalNotification.Days.Contains((int)DateTime.Now.DayOfWeek) && personalNotification.Time.Hour == DateTime.Now.Hour && personalNotification.Time.Minute < DateTime.Now.Minute)
                //    {
                //        MessageBox.Show(personalNotification.Text);
                //        personalNotification.Status = false;
                //    }
                //}
            Thread thread = new Thread(CheckStatus);
            thread.Start();

        }

        //private static void TimerCallback(Object o)
        private static void CheckStatus()
        {
            //ovde isto kao za obavestenja
            String patientId = Login.loggedId;
            App app = Application.Current as App;
            PatientController patientController = app.PatientController;
            MedicalRecordController medicalRecordController = app.MedicalRecordController;
            PersonalNotificationController personalNotificationController = app.personalNotificationController;

            Model.Patient patient = patientController.ReadPatient(patientId);
            MedicalRecord patientMedicalRecord = medicalRecordController.GetMedicalRecord(patient.MedicalRecordID);
            
            foreach(Notification notification in medicalRecordController.GetNotificationTimes(patientMedicalRecord))
            {
                if(notification.DateTimeNotification.AddMinutes(10).Minute == DateTime.Now.Minute)
                {
                    //MessageBox.Show(notification.Content);
                }
            }

            List<HospitalMain.Model.PersonalNotification> personalNotificationList = personalNotificationController.GetPatientPersonalNotifications(Login.loggedId);
            foreach(HospitalMain.Model.PersonalNotification personalNotification in personalNotificationList)
            {
                if(personalNotification.Status == true && personalNotification.Days.Contains((int)DateTime.Now.DayOfWeek) && personalNotification.Time.Hour <= DateTime.Now.Hour && personalNotification.Time.Minute <= DateTime.Now.Minute)
                {
                    MessageBox.Show(personalNotification.Text);
                    personalNotificationController.SetNotificationRead(personalNotification);
                }
            }
            Thread.Sleep(100);


        }

        async Task RunPeriodically(Action action, TimeSpan interval, CancellationToken token)
        {

            while (true)
            {
                
                action();
                await Task.Delay(interval, token);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Window.GetWindow(this).Content = new ExaminationsList();
            Menu.Content = new ExaminationsList();
            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new Login();
        }

        private void Notifications_Click(object sender, RoutedEventArgs e)
        {
            //Notifications notifications = new Notifications();
            //notifications.ShowDialog();
            NotificationsMVVM notificationsMVVM = new NotificationsMVVM();
            notificationsMVVM.ShowDialog();
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
            Menu.Content = new QuestionnairePage();
        }

        private void MedicalREcordClick(object sender, RoutedEventArgs e)
        {
            Menu.Content = new MedicalRecordMVVM();
        }

        private void MenuClick(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new PatientMenu();
        }

        private void AlarmsClick(object sender, RoutedEventArgs e)
        {
            Menu.Content = new Alarms();
        }
    }
}
