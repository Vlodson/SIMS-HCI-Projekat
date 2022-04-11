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
        private List<Model.Doctor> listDoctor;

        public DoctorRepo(string dbPath, List<Model.Doctor> listDoctor)
        {
            this.dbPath = dbPath;
            //listDoctor = new List<Doctor>();
            List<Model.Doctor> doctors = new List<Model.Doctor>();

            List<Examination> examinationsDoctor1 = new List<Examination>();
            DateTime dtDoctor1 = DateTime.Now;
            Model.Doctor doctor1 = new Model.Doctor("idDoctor1", "nameDoctor1", "surnameDoctor1", dtDoctor1, DoctorType.Pulmonology, examinationsDoctor1);

            List<Examination> examinationsDoctor2 = new List<Examination>();
            DateTime dtDoctor2 = DateTime.Now;
            Model.Doctor doctor2 = new Model.Doctor("idDoctor2", "nameDoctor2", "surnameDoctor2", dtDoctor2, DoctorType.Pulmonology, examinationsDoctor2);

            doctors.Add(doctor1);
            doctors.Add(doctor2);

            this.listDoctor = doctors;
           
        }
        public DoctorRepo(string dbPath)
        {
            this.dbPath = dbPath;
            this.listDoctor = new List<Model.Doctor>();

            List<Examination> examinationsDoctor1 = new List<Examination>();
            Model.Doctor doctor1 = new Model.Doctor("111", "nameDoctor1", "surnameDoctor1", new DateTime(2000, 11, 1), DoctorType.Pulmonology, examinationsDoctor1);
            Model.Doctor doctor2 = new Model.Doctor("222", "nameDoctor1", "surnameDoctor1", new DateTime(2000, 11, 1), DoctorType.Pulmonology, examinationsDoctor1);
            this.listDoctor.Add(doctor1);
            this.listDoctor.Add(doctor2);

        }

        public List<Model.Doctor> GetAllDoctors()
        {
            //foreach(Doctor doctor in listDoctor) Console.WriteLine(doctor.Name);
            return listDoctor;
        }

        public Model.Doctor GetDoctor(string id)
        {
            foreach(Model.Doctor doctor in this.listDoctor)
            {
                if (doctor.Id.Equals(id))
                {
                    return doctor;
                }
            }
            return null;
        }


        
    }
}
