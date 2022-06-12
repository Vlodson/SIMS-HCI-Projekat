using Controller;
using HospitalMain.Controller;
using HospitalMain.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using Secretary.Commands;

namespace Secretary.ViewModel
{
    public class RoomOccupancyReportViewModel : ViewModelBase
    {

        private MeetingController _meetingController;
        private ExamController _examController;
        private RoomController _roomController;

        private ObservableCollection<Meeting> _meetingsInWeek;
        private ObservableCollection<Examination> _examsInWeek;

        public ObservableCollection<Meeting> MeetingsInWeek
        {
            get { return _meetingsInWeek; }
            set { _meetingsInWeek = value; OnPropertyChanged(nameof(MeetingsInWeek)); }
        }

        public ObservableCollection<Examination> ExamsInWeek
        {
            get { return _examsInWeek; }
            set { _examsInWeek = value; OnPropertyChanged(nameof(ExamsInWeek)); }
        }

        private DateTime dateTime = DateTime.Now;
        public DateTime DateTime
        {
            get { return dateTime; }
            set { dateTime = value; OnPropertyChanged(nameof(DateTime)); }
        }

        public ICommand ExportPdfCommand { get; }

        public RoomOccupancyReportViewModel(MainViewModel mainViewModel)
        {
            var app = System.Windows.Application.Current as App;
            _meetingController = app.MeetingController;
            _examController = app.ExamController;
            _roomController = app.RoomController;

            MeetingsInWeek = new ObservableCollection<Meeting>();
            ExamsInWeek = new ObservableCollection<Examination>();

            ExportPdfCommand = new ExportPdfCommand(this, _meetingController, _examController, _roomController, mainViewModel);
        }
    }
}
