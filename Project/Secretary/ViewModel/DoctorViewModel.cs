using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.ViewModel
{
    public class DoctorViewModel : ViewModelBase
    {
        private Doctor _doctor;

        public String ID => _doctor.Id;
        public String DoctorName => _doctor.Name;
        public String DoctorSurname => _doctor.Surname;
        public DoctorType DoctorType => _doctor.Type;
        public String DateOfBirth => _doctor.DoB.ToString("g");
        public double FreeDaysLeft => _doctor.FreeDaysLeft;

        public DoctorViewModel(Doctor doctor)
        {
            _doctor = doctor;
        }

    }
}
