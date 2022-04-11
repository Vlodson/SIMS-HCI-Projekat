using Model;
using Service;
using System;
using System.Collections.Generic;
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

        public List<Model.Doctor> GetAll()
        {
            return _doctorService.GetDoctors();
        }

        public Model.Doctor GetDoctor(string id)
        {
            return _doctorService.GetDoctor(id);
        }

        public List<DateTime> GetFreeGetFreeExaminations(Model.Doctor doctor)
        {
            return _doctorService.GetFreeExaminations(doctor);
        }
    }
}
