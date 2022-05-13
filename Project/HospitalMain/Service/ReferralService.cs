using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ReferralService
    {
        private readonly ReferralRepo _referralRepo;

        public ReferralService(ReferralRepo referralRepo)
        {
            _referralRepo = referralRepo;
        }
        public bool NewReferral(Referral referral)
        {
            return _referralRepo.NewReferral(referral);
        }
    }
}
