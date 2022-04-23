using System;
using System.Windows;
using Controller;
using Model;
using System.Collections.ObjectModel;
using Repository;
using System.IO;
using HospitalMain.Enums;

namespace Secretary.View
{
    /// <summary>
    /// Interaction logic for CRUDMedicalRecord.xaml
    /// </summary>
    public partial class CRUDMedicalRecord : Window
    {
        private MedicalRecordController medicalRecordController;
        private MedicalRecordRepo medicalRecordRepo;
        private int id = 3;

        public static ObservableCollection<MedicalRecord> medicalRecords
        {
            get;
            set;
        }

        public CRUDMedicalRecord()
        {
            InitializeComponent();
            this.DataContext = this;

            App app = Application.Current as App;
            medicalRecordController = app.MedicalRecordController;
            medicalRecordRepo = app.MedicalRecordRepo;

            //if (File.Exists(medicalRecordRepo.DBPath))
            //    medicalRecordRepo.LoadMedicalRecord();
            medicalRecords = medicalRecordController.GetAllMedicalRecords();
        }

        private void addMedRecordButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void editMedRecordButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteMedRecordButton_Click(object sender, RoutedEventArgs e)
        {
            MedicalRecord selectedMedicalRecord = (MedicalRecord)dataGridMedicalRecords.SelectedItem;
            if(selectedMedicalRecord != null)
            {
                medicalRecordController.DeleteMedicalRecord(selectedMedicalRecord.ID);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            medicalRecordRepo.SaveMedicalRecord();
        }
    }
}
