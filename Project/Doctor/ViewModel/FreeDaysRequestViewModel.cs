using Commands;
using Controller;
using Doctor;
using Enums;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class FreeDaysRequestViewModel: ViewModelBase
    {
        public IEnumerable<FreeDaysReasons> Reasons
        {
            get
            {
                return Enum.GetValues(typeof(FreeDaysReasons)).Cast<FreeDaysReasons>();
            }
        }

        private  string freeDaysLeft;
        private string startDate;
        private string endDate;
        private FreeDaysReasons reason;
        private FreeDaysReasons selectedItem;

        private readonly DoctorController _doctorController;
        private readonly FreeDaysRequestController _freeDaysRequestController;
        public MyICommand SendRequestCommand { get; set; }

        public FreeDaysReasons Reason
        {
            get { return reason; }
            set
            {
                reason = value;
                OnPropertyChanged("Reason");
            }
        }
        public FreeDaysReasons SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }


        public FreeDaysRequestViewModel()
        {
            var app = System.Windows.Application.Current as App;
            _doctorController = app.doctorController;
            _freeDaysRequestController = app.requestController;

            Model.Doctor doctor = _doctorController.GetDoctor(MainWindow._uid);
            FreeDaysLeft = doctor.FreeDaysLeft.ToString();

            SendRequestCommand = new MyICommand(OnSend);
        }
        public string FreeDaysLeft
        {
            get { return freeDaysLeft; }
            set
            {
                if (freeDaysLeft != value)
                {
                    freeDaysLeft = value;
                    OnPropertyChanged("FreeDaysLeft");
                }
            }
        }
        public string EndDate
        {
            get { return endDate; }
            set
            {
                if (endDate != value)
                {
                    endDate = value;
                    OnPropertyChanged("EndDate");
                }
            }
        }
        public string StartDate
        {
            get { return startDate; }
            set
            {
                if (startDate != value)
                {
                    startDate = value;
                    OnPropertyChanged("StartDate");
                }
            }
        }
        private void OnSend()
        {
           
            FreeDaysRequest request = new FreeDaysRequest(Int32.Parse(freeDaysLeft), _doctorController.GetDoctor(MainWindow._uid).Id, DateTime.Parse(startDate), DateTime.Parse(endDate), selectedItem);
            _freeDaysRequestController.NewRequest(request);
        }
    }
}
