using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
     public class FreeDaysRequestController
    {
        private readonly FreeDaysRequestService _requestService;
        public FreeDaysRequestController(FreeDaysRequestService requestService)
        {
            _requestService = requestService;
        }
        public bool NewRequest(FreeDaysRequest request)
        {
            return _requestService.NewRequest(request);
        }

        public ObservableCollection<FreeDaysRequest> GetAllPendingRequests()
        {
            return _requestService.GetAllPendingRequests();
        }

        public void EditRequestStatus(FreeDaysRequest request)
        {
            _requestService.EditRequestStatus(request);
        }

        public bool CheckIfDoctorHasFreeDays(string doctorID, int days)
        {
            return _requestService.CheckIfDoctorHasFreeDays(doctorID, days);
        }

        public int GenerateID()
        {
            return _requestService.GenerateID();
        }

    }
}
