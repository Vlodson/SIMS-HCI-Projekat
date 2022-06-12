using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    internal interface IFreeDaysRequestRepo
    {
        public bool NewRequest(FreeDaysRequest request);
        public void EditRequestStatus(FreeDaysRequest request);
        public bool LoadRequest();
        public bool SaveRequest();
    }
}
