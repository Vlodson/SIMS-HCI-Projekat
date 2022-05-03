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
    public class DoctorController
    {
        private readonly DoctorService _doctorService;

        public DoctorController(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public ObservableCollection<Doctor> GetAll()
        {
            return _doctorService.GetDoctors();
        }

        public Doctor GetDoctor(string id)
        {
            return _doctorService.GetDoctor(id);
        }

        public List<Examination> GetFreeGetFreeExaminations(Doctor doctor, DateTime startDate, DateTime endDate, bool priority)
        {
            return _doctorService.GetFreeExaminations(doctor, startDate, endDate, priority);
        }

        public List<Examination> MoveExaminations(Examination examination)
        {
            return _doctorService.MoveExaminations(examination);
        }
    }
}
