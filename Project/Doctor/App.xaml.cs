using Controller;
using Model;
using Repository;
using Service;
using Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;

namespace Doctor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ExamController ExamController { get; set; }
        public DoctorController DoctorController { get; set; }
        public PatientController patientController { get; set; }
        public RoomController RoomController { get; set; }
        public ExaminationRepo ExaminationRepo { get; set; }
        public RoomRepo RoomRepo { get; set; }
        public PatientRepo patientRepo { get; set; }



        public App()
        {
            ExaminationRepo = new ExaminationRepo(GlobalPaths.ExamsDBPath);
            patientRepo = new PatientRepo(GlobalPaths.PatientsDBPath);
            RoomRepo = new RoomRepo(GlobalPaths.RoomsDBPath);

            var examRepository = new ExaminationRepo(GlobalPaths.ExamsDBPath);
            var patientRepository = new PatientRepo(GlobalPaths.PatientsDBPath);
            var doctorRepository = new DoctorRepo(GlobalPaths.DoctorsDBPath);
            

            PatientService patientService = new PatientService(patientRepository, examRepository);
            DoctorService doctorService = new DoctorService(doctorRepository, examRepository);
            RoomService roomService = new RoomService(RoomRepo);
            PatientAccountService patientAccountService = new PatientAccountService(patientRepository);

            ExamController = new ExamController(patientService, doctorService);
            DoctorController = new DoctorController(doctorService);
            patientController = new PatientController(patientService, patientAccountService);
            RoomController = new RoomController(roomService);
        }
    }
}
