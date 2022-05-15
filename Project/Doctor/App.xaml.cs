﻿using Controller;
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
using HospitalMain.Enums;

namespace Doctor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ExamController examController { get; set; }
        public DoctorController doctorController { get; set; }
        public PatientController patientController { get; set; }
        public TherapyController therapyController { get; set;  }
        public RoomController roomController { get; set; }
        public ReportController reportController { get; set; }
        public MedicalRecordController medicalRecordController { get; set; }
        public FreeDaysRequestController requestController { get; set; }
        public ReferralController referralController { get; set; }
        public MedicineController medicineController { get; set; }
        public ExaminationRepo examRepo { get; set; }
        public RoomRepo roomRepo { get; set; }
        public PatientRepo patientRepo { get; set; }
        public TherapyRepo therapyRepo { get; set; }
        public DoctorRepo doctorRepo { get; set; }
        public ReportRepo reportRepo { get; set; } 
        public EquipmentRepo equipmentRepo { get; set; }
        public MedicalRecordRepo medicalRecordRepo { get; set; }
        public FreeDaysRequestRepo requestRepo { get; set; }
        public ReferralRepo referralRepo { get; set; }
        public MedicineRepo medicineRepo { get; set; }
        public QuestionnaireRepo questionnaireRepo { get; set; }
        public UserAccountController userAccountController { get; set; }
        public UserAccountRepo userAccountRepo { get; set; }
        public EquipmentController equipmentController { get; set; }

        public EquipmentTransferController equipmentTransferController { get; set; }
        public EquipmentTransferRepo transferRepo { get; set; }




        public App()
        {

            examRepo = new ExaminationRepo(GlobalPaths.ExamsDBPath);
            patientRepo = new PatientRepo(GlobalPaths.PatientsDBPath);
            doctorRepo = new DoctorRepo(GlobalPaths.DoctorsDBPath);
            therapyRepo = new TherapyRepo(GlobalPaths.TherapyDBPath);
            equipmentRepo = new EquipmentRepo(GlobalPaths.EquipmentDBPath);
            roomRepo = new RoomRepo(GlobalPaths.RoomsDBPath, equipmentRepo);
            reportRepo = new ReportRepo(GlobalPaths.ReportDBPath);
            medicalRecordRepo = new MedicalRecordRepo(GlobalPaths.MedicalRecordDBPath);
            userAccountRepo = new UserAccountRepo(GlobalPaths.UserDBPath);
            transferRepo = new EquipmentTransferRepo(GlobalPaths.EquipmentTransfersDBPath, roomRepo, equipmentRepo);
            requestRepo = new FreeDaysRequestRepo(GlobalPaths.RequestDBPath);
            referralRepo = new ReferralRepo(GlobalPaths.ReferralDBPath);
            medicineRepo = new MedicineRepo(GlobalPaths.MedicineDBPath);
            

            var patientService = new PatientService(patientRepo, examRepo, doctorRepo, roomRepo, questionnaireRepo);
            var therapyService = new TherapyService(therapyRepo);
            var doctorService = new DoctorService(doctorRepo, examRepo, roomRepo);
            var roomService = new RoomService(roomRepo);
            var patientAccountService = new PatientAccountService(patientRepo);
            var reportService = new ReportService(reportRepo);
            var medicalRecordService = new MedicalRecordService(medicalRecordRepo);
            var userAccountService = new UserAccountService(userAccountRepo);
            var equipmentService = new EquipmentService(equipmentRepo, roomRepo);
            var equipmentTransferService = new EquipmentTransferService(transferRepo, roomRepo, equipmentRepo, examRepo);
            var requestService = new FreeDaysRequestService(requestRepo);
            var referralService = new ReferralService(referralRepo);
            var medicineService = new MedicineService(medicineRepo);

            examController = new ExamController(patientService, doctorService);
            doctorController = new DoctorController(doctorService);
            patientController = new PatientController(patientService, patientAccountService);
            therapyController = new TherapyController(therapyService);
            roomController = new RoomController(roomService);
            reportController = new ReportController(reportService); 
            medicalRecordController = new MedicalRecordController(medicalRecordService);
            userAccountController = new UserAccountController(userAccountService);
            equipmentController = new EquipmentController(equipmentService);
            equipmentTransferController = new EquipmentTransferController(equipmentTransferService);
            requestController = new FreeDaysRequestController(requestService);
            referralController = new ReferralController(referralService);
            medicineController = new MedicineController(medicineService);
        }
    }
}
