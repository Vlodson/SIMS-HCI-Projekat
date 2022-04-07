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

        public App()
        {
            //ovo obrisati pa zamneiti iz fajla kad do toga dodjem
            List<Examination> exams = new List<Examination>();


            var examinationRepository = new ExaminationRepo("...", exams);
            var patientRepository = new PatientRepo("...");
            var patientService = new PatientService(patientRepository, examinationRepository);

            ExamController = new ExamController(patientService);
        }

    }

    
}
