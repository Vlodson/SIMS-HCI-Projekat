using Model;
using Service;
using System;
using System.Collections.Generic;
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
    }
}
