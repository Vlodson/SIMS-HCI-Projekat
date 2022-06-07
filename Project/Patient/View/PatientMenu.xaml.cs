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
        private int counter = 0;

        private PatientController _patientController;
        private MedicalRecordController _medicalRecordController;
        private ExamController _examinationController;
        private DoctorController _doctorController;
        private DoctorRepo _doctorRepo;
        private PersonalNotificationController _personalNotificationController;
        private NotificationController _notificationController;
        
        public NavigationService NavService { get; set; }

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
            _notificationController = app.NotificationController;

            NavService = this.Menu.NavigationService;
            
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
            NotificationController notificationController = app.NotificationController;

            Model.Patient patient = patientController.ReadPatient(patientId);
            MedicalRecord patientMedicalRecord = medicalRecordController.GetMedicalRecord(patient.MedicalRecordID);
            
            foreach(Notification notification in notificationController.GetPatientNotifications(patientMedicalRecord))
            {
                if(notification.DateTimeNotification.AddMinutes(10).Minute == DateTime.Now.Minute)
                {
                    //MessageBox.Show(notification.Content);
                }
            }

            List<HospitalMain.Model.PersonalNotification> personalNotificationList = personalNotificationController.GetPatientPersonalNotifications(Login.loggedId);
            foreach(HospitalMain.Model.PersonalNotification personalNotification in personalNotificationList)
            {
                //Console.WriteLine((int)DateTime.Now.DayOfWeek);
                if(personalNotification.Status == true && personalNotification.Days.Contains((int)DateTime.Now.DayOfWeek) && personalNotification.Time.Hour == DateTime.Now.Hour && personalNotification.Time.Minute == DateTime.Now.Minute)
                {
                    MessageBox.Show(personalNotification.Text);
                    personalNotificationController.ChangeNotificationStatus(personalNotification);
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
            //Menu.Content = new ExaminationsList();
            Page examinationsList = new ExaminationsList();
            this.NavService.Navigate(examinationsList);
            
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
            if(date.Date == DateTime.Now.Date)
            {
                button.Background = Brushes.CadetBlue;
            }
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Vertical;
            TextBlock textBlock1 = new TextBlock();
            TextBlock textBlock2 = new TextBlock();
            textBlock1.Text = date.Day.ToString();

            int counterExaminations = 0;
            foreach(Examination examination in Examinations)
            {
                if(date.Date == examination.Date.Date)
                {
                    counterExaminations++;
                }
            }
            if(counterExaminations > 0)
            {
                textBlock2.Text = counterExaminations.ToString();
                textBlock2.Foreground = Brushes.DarkBlue;
            }
            else
            {
                textBlock2.Text = "  ";
            }
            textBlock1.FontSize = 15;
            stackPanel.Children.Add(textBlock1);
            StackPanel stackPanel2 = new StackPanel();
            stackPanel2.Orientation = Orientation.Horizontal;
            TextBlock textBlock3 = new TextBlock();
            textBlock3.Text = "                   ";
            stackPanel2.Children.Add(textBlock3);
            stackPanel2.Children.Add(textBlock2);
            stackPanel.Children.Add(stackPanel2);
            button.Content = stackPanel;
        }

        private void calendarButton_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CalendarDayButton button = (CalendarDayButton)sender;
            DateTime date = (DateTime)button.DataContext;
            HighlightDay(button, date);
        }

        private void ChangeSelected(object sender, SelectionChangedEventArgs e)
        {
            if(counter != 0)
            {
                DateTime selected = (DateTime)MenuCalendar.SelectedDate;
                ObservableCollection<Examination> examinations = _examinationController.ReadPatientExams(Login.loggedId);
                DateTime today = DateTime.Now;

                List<Examination> examinationsForDate = new List<Examination>();
                foreach (Examination exam in examinations)
                {
                    if (exam.Date.Date == selected.Date)
                    {
                        Doctor doctor = _doctorController.GetDoctor(exam.DoctorId);
                        if (doctor.Type == DoctorType.Pulmonology)
                        {
                            exam.DoctorTypeString = "Pulmologija";
                        }
                        else if (doctor.Type == DoctorType.Cardiology)
                        {
                            exam.DoctorTypeString = "Kardiologija";
                        }
                        //ovde dodati neurologiju i dermatologiju
                        else
                        {
                            exam.DoctorTypeString = "Opšta praksa";
                        }
                        examinationsForDate.Add(exam);
                    }
                }
                ShowExaminations showExaminations = new ShowExaminations(examinationsForDate);
                showExaminations.ShowDialog();
            }

            ++counter;
        }

        private void GradingsClick(object sender, RoutedEventArgs e)
        {
            //Menu.Content = new Questionnaires();
            //Menu.Content = new QuestionnairePage();
            Page questionnairePage = new QuestionnairePage();
            this.NavService.Navigate(questionnairePage);

        }

        private void MedicalREcordClick(object sender, RoutedEventArgs e)
        {
            //Menu.Content = new MedicalRecordMVVM();
            Page medicalRecordMVVM = new MedicalRecordMVVM();
            this.NavService.Navigate(medicalRecordMVVM);
        }

        private void MenuClick(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new PatientMenu();
        }

        private void AlarmsClick(object sender, RoutedEventArgs e)
        {
            //Menu.Content = new Alarms();
            Page alarms = new Alarms();
            this.NavService.Navigate(alarms);
        }

        private void ReportClick(object sender, RoutedEventArgs e)
        {
            Page reportPage = new ReportPage();
            this.NavService.Navigate(reportPage);
        }

        private void ProfileClick(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            profile.ShowDialog();
        }
    }
}
