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
        public PatientController PatientController { get; set; }
        public RoomController RoomController { get; set; }
        public ExaminationRepo ExaminationRepo { get; set; }
        public RoomRepo RoomRepo { get; set; }



        public App()
        {
            ObservableCollection<Patient> patients = new ObservableCollection<Patient>();
            List<Room> rooms = new List<Room>();

            ExaminationRepo = new ExaminationRepo(GlobalPaths.ExamsDBPath);
            RoomRepo = new RoomRepo(GlobalPaths.RoomsDBPath, rooms);
            var patientRepository = new PatientRepo("...", patients);
            var doctorRepository = new DoctorRepo("...");

            PatientService patientService = new PatientService(patientRepository, ExaminationRepo);
            DoctorService doctorService = new DoctorService(doctorRepository, ExaminationRepo);
            RoomService roomService = new RoomService(RoomRepo);
            PatientAccountService patientAccountService = new PatientAccountService(patientRepository);

            ExamController = new ExamController(doctorService);
            DoctorController = new DoctorController(doctorService);
            PatientController = new PatientController(patientService, patientAccountService);
            RoomController = new RoomController(roomService);
        }
    }
}
