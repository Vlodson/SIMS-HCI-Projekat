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
using Controller;
using Model;
using Syncfusion.UI.Xaml.Scheduler;

namespace Secretary.View
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : UserControl
    {
        public HomePage()
        {
            InitializeComponent();
            homeScheduler.DaysViewSettings.TimeInterval = new System.TimeSpan(0, 30, 0);

            var app = System.Windows.Application.Current as App;
            ExamController examController = app.ExamController;
            DoctorController doctorController = app.DoctorController;
            PatientController patientController = app.PatientController;
            RoomController roomController = app.RoomController;
            ScheduleAppointmentCollection sac = new ScheduleAppointmentCollection();

            foreach (Examination exam in examController.GetExaminations())
            {
                ScheduleAppointment sa = new ScheduleAppointment();

                Patient currentPatient = patientController.ReadPatient(exam.PatientId);
                Doctor currentDoctor = doctorController.GetDoctor(exam.DoctorId);
                Room currentRoom = roomController.ReadRoom(exam.ExamRoomId);
                sa.Subject = currentRoom.RoomNb + " " + currentPatient.NameSurname + " " + currentDoctor.NameSurname;

                sa.StartTime = exam.Date;
                sa.EndTime = exam.Date.AddMinutes(30);
                sa.IsAllDay = false;

                sa.AppointmentBackground = new SolidColorBrush(Colors.LightBlue);
                sac.Add(sa);
            }
            homeScheduler.ItemsSource = sac;
        }
    }
}
