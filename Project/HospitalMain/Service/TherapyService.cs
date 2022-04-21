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
    
    public class TherapyService
    {
        private readonly TherapyRepo _therapyRepo;
        public TherapyService(TherapyRepo therapyRepo)
        {
            _therapyRepo = therapyRepo;

        }

        public ObservableCollection<Therapy> findById(string id)
        {
            return _therapyRepo.findById(id);
        }
    }
}
