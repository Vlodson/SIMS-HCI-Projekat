using Controller;
using Enums;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.ViewModel
{
    public class FreeRequestViewModel : ViewModelBase
    {

        private FreeDaysRequest _freeDaysRequest;
        private DoctorController _doctorController;

        public string ID => _freeDaysRequest.ID;
        public StatusEnum Status => _freeDaysRequest.Status;
        public string DoctorID => _freeDaysRequest.DoctorId;
        public DateTime StartDate
        {
            get => _freeDaysRequest.StartDate;
            set
            {
                _freeDaysRequest.StartDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        public DateTime EndDate
        {
            get => _freeDaysRequest.EndDate;
            set
            {
                _freeDaysRequest.EndDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }
        public FreeDaysReasons Reason
        {
            get => _freeDaysRequest.Reason;
            set
            {
                _freeDaysRequest.Reason = value;
                OnPropertyChanged(nameof(Reason));
            }
        }
        public string RejectionReason
        {
            get => _freeDaysRequest.RejectionReason;
            set
            {
                _freeDaysRequest.RejectionReason = value;
                OnPropertyChanged(nameof(RejectionReason));
            }
        }
        public String DoctorName { get; set; }
        public String DoctorSurname { get; set; }
        public DoctorType Specialization { get; set; }
        public double FreeDaysLeft { get; set; }

        public FreeRequestViewModel(FreeDaysRequest freeDaysRequest)
        {
            var app = System.Windows.Application.Current as App;
            _doctorController = app.DoctorController;

            _freeDaysRequest = freeDaysRequest;

            Doctor doctor = _doctorController.GetDoctor(_freeDaysRequest.DoctorId);

            DoctorName = doctor.Name;
            DoctorSurname = doctor.Surname;
            Specialization = doctor.Type;
            FreeDaysLeft = doctor.FreeDaysLeft;
        }
    }
}
