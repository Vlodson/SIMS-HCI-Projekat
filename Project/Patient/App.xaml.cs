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
using HospitalMain.Service;

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
        public EquipmentTransferController equipmentTransferController { get; set; }
        public RenovationController renovationController { get; set; }
        public MedicineController medicineController { get; set; }

        public ReferralController ReferralController { get; set; }

        public HospitalMain.Controller.PersonalNotificationController personalNotificationController { get; set; }


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
            var equipmentTransferRepo = new EquipmentTransferRepo(GlobalPaths.EquipmentTransfersDBPath, roomRepo, equipmentRepo);
            var renovationRepo = new RenovationRepo(GlobalPaths.RenovationDBPath, roomRepo);
            var medicineRepo = new MedicineRepo(GlobalPaths.MedicineDBPath);
            ReferralRepo referralRepo = new ReferralRepo(GlobalPaths.ReferralDBPath);
            PersonalNotificationRepo personalNotificationRepo = new PersonalNotificationRepo(GlobalPaths.PersonalNotificationDBPath);

            DoctorRepo doctorRepository = new DoctorRepo(GlobalPaths.DoctorsDBPath);
            DoctorRepo = doctorRepository;

            PatientService patientService = new PatientService(patientRepository, examinationRepo, doctorRepository, roomRepo, questionnaireRepo);
            PatientAccountService patientAccountService = new PatientAccountService(patientRepository);
            DoctorService doctorService = new DoctorService(doctorRepository, examinationRepo, roomRepo, patientRepository);
            RoomService roomService = new RoomService(roomRepo);
            MedicalRecordService medicalRecordService = new MedicalRecordService(medicalRecordRepo);
            EquipmentService equipmentService = new EquipmentService(equipmentRepo, roomRepo);
            UserAccountService userAccountService = new UserAccountService(userAccountRepo);
            var equipmentTransferService = new EquipmentTransferService(equipmentTransferRepo, roomRepo, equipmentRepo, examinationRepo);
            var renovationService = new RenovationService(renovationRepo, roomRepo, examinationRepo);
            var medicineService = new MedicineService(medicineRepo);
            ReferralService referralService = new ReferralService(referralRepo);

            PersonalNotificationService personalNotificationService = new PersonalNotificationService(personalNotificationRepo);    

            EmergencyService emergencyService = new EmergencyService(examinationRepo, DoctorRepo);


            ExamController = new ExamController(patientService, doctorService);
            DoctorController = new DoctorController(doctorService, emergencyService);
            PatientController = new PatientController(patientService, patientAccountService);
            RoomController = new RoomController(roomService);
            MedicalRecordController = new MedicalRecordController(medicalRecordService);
            EquipmentController = new EquipmentController(equipmentService);
            UserAccountController = new UserAccountController(userAccountService);
            equipmentTransferController = new EquipmentTransferController(equipmentTransferService);
            renovationController = new RenovationController(renovationService);
            medicineController = new MedicineController(medicineService);
            ReferralController = new ReferralController(referralService);
            personalNotificationController = new HospitalMain.Controller.PersonalNotificationController(personalNotificationService);

            if (File.Exists(GlobalPaths.EquipmentDBPath))
                EquipmentController.LoadEquipment();

            if (File.Exists(GlobalPaths.RoomsDBPath))
                RoomController.LoadRoom();

            if (File.Exists(GlobalPaths.EquipmentTransfersDBPath))
                equipmentTransferController.LoadEquipmentTransfer();

            if (File.Exists(GlobalPaths.RenovationDBPath))
                renovationController.LoadRenovation();

            if (File.Exists(GlobalPaths.MedicineDBPath))
                medicineController.LoadMedicine();

            for (int i = 0; i < 20; i++)
            {
                int floor = 1;
                if (i > 10)
                    floor = 2;

                RoomController.CreateRoom(i.ToString(), floor, i % 11 + 10 * (floor - 1), false, (RoomTypeEnum)(i % 5), (RoomTypeEnum)(i % 5));
                EquipmentController.CreateEquipment(i.ToString(), i.ToString(), (EquipmentTypeEnum)(i % 10));
                RoomController.AddEquipment(i.ToString(), EquipmentController.ReadEquipment(i.ToString()));
            }

            for (int i = 0; i < 20; i++)
            {
                Room OriginRoom = RoomController.ReadRoom(i.ToString());
                Room DestinationRoom = RoomController.ReadRoom(((i + 1) % 20).ToString());
                Equipment equipment = EquipmentController.ReadEquipment(i.ToString());
                EquipmentTransfer equipmentTransfer = new EquipmentTransfer(i.ToString(), OriginRoom, DestinationRoom, equipment, new DateTime(2022, 10, 10, 12, 0, 0), new DateTime(2022, 10, 10, 13, 0, 0));
                equipmentTransferController.ScheduleTransfer(equipmentTransfer);
                equipmentTransferController.RecordTransfer(i.ToString());
            }

            
            //for (int i = 0; i < 20; i++)
            //{
            //    int floor = 1;
            //    if (i > 10)
            //        floor = 2;

            //    RoomController.CreateRoom(i.ToString(), floor, i % 11 + 10 * (floor - 1), false, (RoomTypeEnum)(i % 5), (RoomTypeEnum)(i % 5));
            //    EquipmentController.CreateEquipment(i.ToString(), i.ToString(), (EquipmentTypeEnum)(i % 10));
            //    RoomController.AddEquipment(i.ToString(), EquipmentController.ReadEquipment(i.ToString()));
            //}

            //if (File.Exists(ExaminationRepo.dbPath))
            //    ExaminationRepo.LoadExamination();

            //EquipmentController.SaveEquipment();
            //RoomController.SaveRoom();
            //equipmentTransferController.SaveEquipmentTransfer();
            //renovationController.SaveRenovation();

        }

    }
}
