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
            List<Patient> patients = new List<Patient>();
            List<Room> rooms = new List<Room>();

            string dbPathExams = "..\\..\\..\\..\\HospitalMain\\Database\\Examinations.json";
            string dbPathRooms = "..\\..\\..\\..\\HospitalMain\\Database\\Rooms.json";

            ExaminationRepo = new ExaminationRepo(dbPathExams);
            RoomRepo = new RoomRepo(dbPathRooms, rooms);
            var patientRepository = new PatientRepo("...", patients);
            var doctorRepository = new DoctorRepo("...");

            var patientService = new PatientService(patientRepository, ExaminationRepo);
            var doctorService = new DoctorService(doctorRepository, ExaminationRepo);
            var roomService = new RoomService(RoomRepo);

            ExamController = new ExamController(doctorService);
            DoctorController = new DoctorController(doctorService);
            PatientController = new PatientController(patientService);
            RoomController = new RoomController(roomService);
        }
    }
}
