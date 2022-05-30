using Controller;
using Doctor;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ShowAllRequestsViewModel: ViewModelBase
    {
        public ObservableCollection<FreeDaysRequest> Requests { get; set; }
        private FreeDaysRequestController _requestController;
        public ShowAllRequestsViewModel ()
        {
            var app = System.Windows.Application.Current as App;
            _requestController = app.requestController;

            Requests = _requestController.ReadAllByDoctorId(MainWindow._uid);
        }
    }
}
