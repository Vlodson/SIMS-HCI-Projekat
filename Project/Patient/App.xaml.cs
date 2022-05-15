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
using HospitalMain.Enums;
using System.IO;
using HospitalMain.Repository;

namespace Patient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 


    
    public partial class App : Application
    {
        public ExamController ExamController { get; set; }
        public DoctorController DoctorController { get; set; }
        public PatientController PatientController { get; set; }

        public ExaminationRepo ExaminationRepo { get; set; }
        public DoctorRepo DoctorRepo { get; set; }
        public PatientRepo PatientRepo { get; set; }
        public QuestionnaireRepo QuestionnaireRepo { get; set; }
        public RoomController RoomController { get; set; }
        public MedicalRecordController MedicalRecordController { get; set; }
        public EquipmentController EquipmentController { get; set; }

        public UserAccountController UserAccountController { get; set; }

        public App()
        {
            //ovo obrisati pa zamneiti iz fajla kad do toga dodjem
            List<Examination> exams = new List<Examination>();
            List<Doctor> doctors = new List<Doctor>();
            List<Model.Patient> patients = new List<Model.Patient>();

            //ExaminationRepo examinationRepository = new ExaminationRepo(dbPathExams);
            ExaminationRepo examinationRepo = new ExaminationRepo(GlobalPaths.ExamsDBPath);
            ExaminationRepo = examinationRepo;
            PatientRepo patientRepository = new PatientRepo(GlobalPaths.PatientsDBPath);
            PatientRepo = patientRepository;
            //DoctorRepo doctorRepository = new DoctorRepo("...", doctors);
            EquipmentRepo equipmentRepo = new EquipmentRepo(GlobalPaths.EquipmentDBPath);
            RoomRepo roomRepo = new RoomRepo(GlobalPaths.RoomsDBPath, equipmentRepo);
            MedicalRecordRepo medicalRecordRepo = new MedicalRecordRepo(GlobalPaths.MedicalRecordDBPath);
            UserAccountRepo userAccountRepo = new UserAccountRepo(GlobalPaths.UserDBPath);
            QuestionnaireRepo questionnaireRepo = new QuestionnaireRepo(GlobalPaths.QuestionnaireDBPath);
            QuestionnaireRepo = questionnaireRepo;
            
            DoctorRepo doctorRepository = new DoctorRepo(GlobalPaths.DoctorsDBPath);
            DoctorRepo = doctorRepository;

            PatientService patientService = new PatientService(patientRepository, examinationRepo, doctorRepository, roomRepo, questionnaireRepo);
            PatientAccountService patientAccountService = new PatientAccountService(patientRepository);
            DoctorService doctorService = new DoctorService(doctorRepository, examinationRepo, roomRepo);
            RoomService roomService = new RoomService(roomRepo);
            MedicalRecordService medicalRecordService = new MedicalRecordService(medicalRecordRepo);
            EquipmentService equipmentService = new EquipmentService(equipmentRepo, roomRepo);
            UserAccountService userAccountService = new UserAccountService(userAccountRepo);

            ExamController = new ExamController(patientService, doctorService);
            DoctorController = new DoctorController(doctorService);
            PatientController = new PatientController(patientService, patientAccountService);
            RoomController = new RoomController(roomService);
            MedicalRecordController = new MedicalRecordController(medicalRecordService);
            EquipmentController = new EquipmentController(equipmentService);
            UserAccountController = new UserAccountController(userAccountService);

            /*for (int i = 0; i < 20; i++)
            {
                int floor = 1;
                if (i > 10)
                    floor = 2;

                RoomController.CreateRoom(i.ToString(), floor, i % 11 + 10 * (floor - 1), false, (RoomTypeEnum)(i % 5), (RoomTypeEnum)(i % 5));
                EquipmentController.CreateEquipment(i.ToString(), i.ToString(), (EquipmentTypeEnum)(i % 10));
                RoomController.AddEquipment(i.ToString(), EquipmentController.ReadEquipment(i.ToString()));
            }

            if (File.Exists(ExaminationRepo.dbPath))
                ExaminationRepo.LoadExamination();*/

        }
    }
}
