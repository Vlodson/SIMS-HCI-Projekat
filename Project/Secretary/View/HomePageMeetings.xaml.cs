using HospitalMain.Controller;
using HospitalMain.Model;
using Syncfusion.UI.Xaml.Scheduler;
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

namespace Secretary.View
{
    /// <summary>
    /// Interaction logic for HomePageMeetings.xaml
    /// </summary>
    public partial class HomePageMeetings : UserControl
    {
        public HomePageMeetings()
        {
            InitializeComponent();
            homeScheduler.DaysViewSettings.TimeInterval = new System.TimeSpan(0, 30, 0);

            var app = System.Windows.Application.Current as App;
            MeetingController meetingController = app.MeetingController;
            ScheduleAppointmentCollection sac = new ScheduleAppointmentCollection();

            foreach(Meeting meeting in meetingController.GetAllMeetings())
            {
                ScheduleAppointment sa = new ScheduleAppointment();

                sa.Subject = meeting.MeetingTopic;
                sa.StartTime = meeting.DateTime;
                sa.EndTime = meeting.DateTime.AddMinutes(30);
                sa.IsAllDay = false;
                sa.AppointmentBackground = new SolidColorBrush(Colors.LightBlue);
                sac.Add(sa);
            }
            homeScheduler.ItemsSource = sac;
        }
    }
}
