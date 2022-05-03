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
        public ExaminationRepo examRepo { get; set; }
        public RoomRepo roomRepo { get; set; }
        public PatientRepo patientRepo { get; set; }
        public TherapyRepo therapyRepo { get; set; }
        public DoctorRepo doctorRepo { get; set; }
        public ReportRepo reportRepo { get; set; } 
        public EquipmentRepo equipmentRepo { get; set; }
        public MedicalRecordRepo medicalRecordRepo { get; set; }

        public UserAccountController userAccountController { get; set; }
        public UserAccountRepo userAccountRepo { get; set; }



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
            

            var patientService = new PatientService(patientRepo, examRepo, doctorRepo, roomRepo);
            var therapyService = new TherapyService(therapyRepo);
            var doctorService = new DoctorService(doctorRepo, examRepo, roomRepo);
            var roomService = new RoomService(roomRepo);
            var patientAccountService = new PatientAccountService(patientRepo);
            var reportService = new ReportService(reportRepo);
            var medicalRecordService = new MedicalRecordService(medicalRecordRepo);
            var userAccountService = new UserAccountService(userAccountRepo);

            examController = new ExamController(patientService, doctorService);
            doctorController = new DoctorController(doctorService);
            patientController = new PatientController(patientService, patientAccountService);
            therapyController = new TherapyController(therapyService);
            roomController = new RoomController(roomService);
            reportController = new ReportController(reportService); 
            medicalRecordController = new MedicalRecordController(medicalRecordService);
            userAccountController = new UserAccountController(userAccountService);
        }
    }
}
