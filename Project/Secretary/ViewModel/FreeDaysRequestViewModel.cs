using Controller;
using Enums;
using Model;
using Secretary.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Secretary.ViewModel
{
    public class FreeDaysRequestViewModel : ViewModelBase
    {

        private FreeDaysRequestController _freeDaysRequestController;
        private DoctorController _doctorController;

        //lista zahteva
        private ObservableCollection<FreeRequestViewModel> _freeDaysRequestsList;
        public ObservableCollection<FreeRequestViewModel> FreeDaysRequestsList
        {
            get { return _freeDaysRequestsList; }
            set { _freeDaysRequestsList = value; OnPropertyChanged(nameof(FreeDaysRequestsList)); }
        }

        //selektovani zahtev iz tabele
        private FreeRequestViewModel _freeDaysRequest;
        public FreeRequestViewModel FreeDaysRequest
        {
            get { return _freeDaysRequest; }
            set { _freeDaysRequest = value; OnPropertyChanged(nameof(FreeDaysRequest)); }
        }

        //ID
        private string _id;
        public string ID
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(nameof(ID)); }
        }

        //pocetak odmora
        private DateTime _startDate;
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged(nameof(StartDate)); }
        }

        //kraj odmora
        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; OnPropertyChanged(nameof(EndDate)); }
        }

        //doktorov ID
        private string _doctorID;
        public string DoctorID
        {
            get { return _doctorID; }
            set { _doctorID = value; OnPropertyChanged(nameof(DoctorID)); }
        }

        //razlog odmora
        private FreeDaysReasons _reason;
        public FreeDaysReasons Reason
        {
            get { return _reason; }
            set { _reason = value; OnPropertyChanged(nameof(Reason)); }
        }

        //potencijalni razlog odbijanja
        private string _rejectionReason;
        public string RejectionReason
        {
            get { return _rejectionReason; }
            set { _rejectionReason = value; OnPropertyChanged(nameof(RejectionReason)); }
        }

        public ICommand ApproveCommand { get; }
        public ICommand RejectCommand { get; }
        private MainViewModel _mainViewModel;

        public FreeDaysRequestViewModel(MainViewModel mainViewModel)
        {
            var app = System.Windows.Application.Current as App;
            _freeDaysRequestController = app.FreeDaysController;
            _doctorController = app.DoctorController;

            _freeDaysRequestsList = new ObservableCollection<FreeRequestViewModel>();
            _mainViewModel = mainViewModel;

            //inicijalizacija komandi
            ApproveCommand = new ApproveRequestCommand(this, _freeDaysRequestController, _mainViewModel, _doctorController);
            RejectCommand = new RejectRequestCommand(this, _freeDaysRequestController, _mainViewModel);

            ObservableCollection<FreeDaysRequest> requestsFromBase = _freeDaysRequestController.GetAllPendingRequests();
            foreach (FreeDaysRequest request in requestsFromBase)
            {
                _freeDaysRequestsList.Add(new FreeRequestViewModel(request));
            }

            FreeDaysRequest = null;
        }

    }
}
