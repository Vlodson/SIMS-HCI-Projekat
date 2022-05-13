using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class ReferralController
    {
        private readonly ReferralService _referralService;

        public ReferralController(ReferralService referralService)
        {
            _referralService = referralService;
        }
        public bool NewReferral(Referral referral)
        {
            return _referralService.NewReferral(referral);
        } 
    }
}
