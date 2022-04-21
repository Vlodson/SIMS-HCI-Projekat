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
    public class TherapyController
    {
        private TherapyService therapyService;

        public TherapyController(TherapyService therapyService)
        {
            this.therapyService = therapyService;
        }

        public ObservableCollection<Therapy> findById (string examId)
        {
            return therapyService.findById(examId);
        }
    }
}
