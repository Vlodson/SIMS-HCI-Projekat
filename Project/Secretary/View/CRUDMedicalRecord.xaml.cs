using System;
using System.Windows;
using Controller;
using Model;
using System.Collections.ObjectModel;
using Repository;
using System.IO;
using HospitalMain.Enums;
using System.Collections.Generic;
using System.Linq;
using HospitalMain.Model;
using Secretary.ComboBoxTemplate;
using Secretary.ViewModel;

namespace Secretary.View
{
    /// <summary>
    /// Interaction logic for CRUDMedicalRecord.xaml
    /// </summary>
    public partial class CRUDMedicalRecord : Window
    {
        private MedicalRecordRepo medicalRecordRepo;

        public CRUDMedicalRecord()
        {
            InitializeComponent();
            this.DataContext = new CRUDMedicalRecordViewModel();

            App app = Application.Current as App;
            medicalRecordRepo = app.MedicalRecordRepo;         
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            medicalRecordRepo.SaveMedicalRecord();
        }

    }
}
