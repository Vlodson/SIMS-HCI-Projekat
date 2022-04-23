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
        private readonly TherapyService _therapyService;

        public TherapyController(TherapyService therapyService)
        {
            _therapyService = therapyService;
        }

        public ObservableCollection<Therapy> findById (string examId)
        {
            return _therapyService.findById(examId);
        }

        public void NewTherapy(Therapy therapy)
        {
            _therapyService.NewTherapy(therapy);
        }
    }
}
