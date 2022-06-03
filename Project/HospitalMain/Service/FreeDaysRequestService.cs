using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class FreeDaysRequestService
    {
        private readonly FreeDaysRequestRepo _requestRepo;
        private readonly DoctorRepo _doctorRepo;
        
        public FreeDaysRequestService(FreeDaysRequestRepo requestRepo, DoctorRepo doctorRepo)
        {
            _requestRepo = requestRepo;
            _doctorRepo = doctorRepo;
        }
        public bool NewRequest(FreeDaysRequest request)
        {
            return _requestRepo.NewRequest(request);
        }

        public ObservableCollection<FreeDaysRequest> GetAllPendingRequests()
        {
            ObservableCollection<FreeDaysRequest> pendingRequests = new ObservableCollection<FreeDaysRequest>();
            foreach(FreeDaysRequest request in _requestRepo.Requests)
            {
                if(request.Status == Enums.StatusEnum.Pending)
                {
                    pendingRequests.Add(request);
                }
            }

            return pendingRequests;
        }

        public ObservableCollection<FreeDaysRequest> GetAllAcceptedRequests()
        {
            ObservableCollection<FreeDaysRequest> acceptedRequests = new ObservableCollection<FreeDaysRequest>();
            foreach(FreeDaysRequest request in _requestRepo.Requests)
            {
                if(request.Status == Enums.StatusEnum.Approved)
                {
                    acceptedRequests.Add(request);
                }
            }

            return acceptedRequests;
        }

        public void EditRequestStatus(FreeDaysRequest request)
        {
            _requestRepo.EditRequestStatus(request);
        }

        public bool CheckIfDoctorHasFreeDays(string doctorID, int days)
        {
            ObservableCollection<Doctor> doctorsFromBase = _doctorRepo.DoctorList;
            foreach(Doctor doctor in doctorsFromBase)
            {
                if (doctor.Id.Equals(doctorID))
                {
                    return HasFreeDays(doctor, days);
                }
            }
            return false;
        }

        private bool HasFreeDays(Doctor doctor, int days)
        {
            if(doctor.FreeDaysLeft - days >= 0)
            {
                return true;
            }
            return false;
        }
    }
}
