using Controller;
using Model;
using Repository;
using Syncfusion.UI.Xaml.Scheduler;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Doctor.View
{
    /// <summary>
    /// Interaction logic for ExaminationSchedule.xaml
    /// </summary>
    public partial class ExaminationSchedule : Page
    {
        private ExamController _examController;
        private ExaminationRepo _examRepo;
        public static Examination SelectedItem
        {
            get;
            set;
        }
        public ExaminationSchedule()
        {
            InitializeComponent();
            this.DataContext = this;

            App app = Application.Current as App;
            _examController = app.examController;
            _examRepo = app.examRepo;

            Scheduler.DaysViewSettings.TimeInterval = new System.TimeSpan(0, 30, 0);
            ScheduleAppointmentCollection sac = new ScheduleAppointmentCollection();
            if (File.Exists(_examRepo.DBPath))
                _examRepo.LoadExamination();

            foreach(Examination exam in _examController.ReadDoctorExams(MainWindow._uid))
            {
                ScheduleAppointment sa = new ScheduleAppointment();

                sa.Subject = exam.NameSurnamePatient + " | Soba: " + exam.ExamRoomId;
                sa.StartTime = exam.Date;
                sa.EndTime = exam.Date.AddMinutes(exam.Duration);
                sa.IsAllDay = false;
                sa.AppointmentBackground = new SolidColorBrush(Colors.LightBlue);
                sac.Add(sa);
            }
            Scheduler.ItemsSource = sac;
        }
        private void add_Click(object sender, RoutedEventArgs e)
        {
            AddExamination addExamination = new AddExamination(this);
            NavigationService.Navigate(addExamination);
            _examRepo.SaveExamination();
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            //SelectedItem = (Examination)dataGridExaminations.SelectedItem;
            //UpdateExamination updateExamination = new UpdateExamination(SelectedItem, this);
            UpdateExamination updateExamination = new UpdateExamination();
            NavigationService.Navigate(updateExamination);
        }


        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            //Examination selectedItem = _examController.GetExamByTime((DateTime)Scheduler.SelectedDate);
            //if (selectedItem != null)
            //{
            //    _examController.DoctorRemoveExam(selectedItem);
            //    dataGridExaminations.ItemsSource = _examController.ReadDoctorExams(MainWindow._uid);
            //    _examRepo.SaveExamination();
            //}

        }
    }
}
