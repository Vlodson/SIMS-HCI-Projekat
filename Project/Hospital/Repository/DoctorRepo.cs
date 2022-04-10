using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DoctorRepo
    {
        private String dbPath;
        private List<Doctor> listDoctor;

        public DoctorRepo(string dbPath, List<Doctor> listDoctor)
        {
            this.dbPath = dbPath;
            //listDoctor = new List<Doctor>();
            List<Doctor> doctors = new List<Doctor>();

            List<Examination> examinationsDoctor1 = new List<Examination>();
            DateTime dtDoctor1 = DateTime.Now;
            Doctor doctor1 = new Doctor("idDoctor1", "nameDoctor1", "surnameDoctor1", dtDoctor1, DoctorType.Pulmonology, examinationsDoctor1);

            List<Examination> examinationsDoctor2 = new List<Examination>();
            DateTime dtDoctor2 = DateTime.Now;
            Doctor doctor2 = new Doctor("idDoctor2", "nameDoctor2", "surnameDoctor2", dtDoctor2, DoctorType.Pulmonology, examinationsDoctor2);

            doctors.Add(doctor1);
            doctors.Add(doctor2);

            this.listDoctor = doctors;
           
        }

        public List<Doctor> GetAllDoctors()
        {
            //foreach(Doctor doctor in listDoctor) Console.WriteLine(doctor.Name);
            return listDoctor;
        }

        public Doctor GetDoctor(string id)
        {
            foreach(Doctor doctor in this.listDoctor)
            {
                if (doctor.getId().Equals(id))
                {
                    return doctor;
                }
            }
            return null;
        }

        
    }
}
