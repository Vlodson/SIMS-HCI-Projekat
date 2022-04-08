using ClassDijagramV1._0.Controller;
using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ClassDijagramV1._0
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 

    //public ExamController ExamController { get; set; }

    
    public partial class App : Application
    {
        //ExamController = new ExamController();
        public ExamController ExamController { get; set; }
        public DoctorController DoctorController { get; set; }
        public PatientController PatientController { get; set; }

        public App()
        {
            //ovo obrisati pa zamneiti iz fajla kad do toga dodjem
            List<Examination> exams = new List<Examination>();
            List<Doctor> doctors = new List<Doctor>();
            List<Patient> patients = new List<Patient>();


            var examinationRepository = new ExaminationRepo("...", exams);
            var patientRepository = new PatientRepo("...");
            var patientService = new PatientService(patientRepository, examinationRepository);
            var doctorRepository = new DoctorRepo("...", doctors);
            var doctorService = new DoctorService(doctorRepository, examinationRepository);

            ExamController = new ExamController(patientService);
            DoctorController = new DoctorController(doctorService);
            PatientController = new PatientController(patientService);
        }

    }

    
}
