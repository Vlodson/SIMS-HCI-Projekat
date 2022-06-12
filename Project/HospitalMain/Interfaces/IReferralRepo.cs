using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    internal interface IReferralRepo
    {
        public bool NewReferral(Referral referral);
        public bool LoadReferral();
        public bool SaveReferral();
    }
}
