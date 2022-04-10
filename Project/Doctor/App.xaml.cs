﻿using Controller;
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

        public App()
        {
            List<Examination> exams = new List<Examination>();
            List<Patient> patients = new List<Patient>();
            List<Room> rooms = new List<Room>();

            var examinationRepository = new ExaminationRepo("...", exams);
            var patientRepository = new PatientRepo("...", patients);
            var doctorRepository = new DoctorRepo("...");
            var roomRepository = new RoomRepo("...", rooms);

            var patientService = new PatientService(patientRepository, examinationRepository);
            var doctorService = new DoctorService(doctorRepository, examinationRepository);
            var roomService = new RoomService(roomRepository);

            ExamController = new ExamController(doctorService);
            DoctorController = new DoctorController(doctorService);
            PatientController = new PatientController(patientService);
            RoomController = new RoomController(roomService);
        }
    }
}