using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class FreeDaysRequestService
    {
        private readonly FreeDaysRequestRepo _requestRepo;
        
        public FreeDaysRequestService(FreeDaysRequestRepo requestRepo)
        {
            _requestRepo = requestRepo;
        }
        public bool NewRequest(FreeDaysRequest request)
        {
            return _requestRepo.NewRequest(request);
        }
    }
}
