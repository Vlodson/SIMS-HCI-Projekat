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

namespace Secretary.View
{
    /// <summary>
    /// Interaction logic for CRUDMedicalRecord.xaml
    /// </summary>
    public partial class CRUDMedicalRecord : Window
    {
        private MedicalRecordController medicalRecordController;
        private MedicalRecordRepo medicalRecordRepo;
        private DoctorController doctorController;
        private int id = 3;

        public static ObservableCollection<MedicalRecord> MedicalRecords
        {
            get;
            set;
        }

        public static ObservableCollection<Allergen> Allergens
        {
            get;
            set;
        }

        public static ObservableCollection<Report> Reports
        {
            get;
            set;
        }

        public static ObservableCollection<Doctor> Doctors
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
            doctorController = app.DoctorController;

            //MedicalRecords = new ObservableCollection<MedicalRecord>();
            //if (File.Exists(medicalRecordRepo.DBPath))
            //    medicalRecordRepo.LoadMedicalRecord();
            MedicalRecords = medicalRecordController.GetAllMedicalRecords();
            Doctors = doctorController.GetAll();

            
        }

        private void addMedRecordButton_Click(object sender, RoutedEventArgs e)
        {
            String name = textNameMedRecord.Text;
            String surname = textSurnameMedRecord.Text;
            String ucin = textUCINMedRecord.Text;
            String phoneNum = textPhoneNumMedRecord.Text;
            String adress = textAdressMedRecord.Text;
            String mail = textMailMedRecord.Text;
            DateTime dob = Convert.ToDateTime(textDoBMedRecord.Text);
            Enum.TryParse(textGenderMedRecord.Text, out Gender gender);
            String bloodType = textBloodTypeMedRecord.Text;
            id++;

            //izvestaji
            ObservableCollection<Report> reports = new ObservableCollection<Report>();

            //novi alergeni
            String[] stringAllergens = textAllergensMedicalRecord.Text.Split(", ");
            ObservableCollection<Allergen> allergens = new ObservableCollection<Allergen>();
            foreach (string al in stringAllergens)
            {
                Allergen allergen = new Allergen(al);
                allergens.Add(allergen);
            }

            medicalRecordController.CreateMedicalRecord(id.ToString(), ucin, name, surname, phoneNum, mail, adress, gender, dob, bloodType, reports, allergens);
            
        }

        private void editMedRecordButton_Click(object sender, RoutedEventArgs e)
        {
            MedicalRecord selectedMedicalRecord = (MedicalRecord)dataGridMedicalRecords.SelectedItem;

            if(selectedMedicalRecord != null)
            {
                selectedMedicalRecord.Name = textNameMedRecord.Text;
                selectedMedicalRecord.Surname = textSurnameMedRecord.Text;
                selectedMedicalRecord.UCIN = textUCINMedRecord.Text;
                selectedMedicalRecord.PhoneNumber = textPhoneNumMedRecord.Text;
                selectedMedicalRecord.Adress = textAdressMedRecord.Text;
                selectedMedicalRecord.Mail = textMailMedRecord.Text;

                selectedMedicalRecord.DoB = Convert.ToDateTime(textDoBMedRecord.Text);

                Enum.TryParse(textGenderMedRecord.Text, out Gender gender);
                selectedMedicalRecord.Gender = gender;
                selectedMedicalRecord.BloodType = textBloodTypeMedRecord.Text;

                //List<string> list = new List<string>();
                //list.AddRange(listOfAllergensMedicalRecord.Items.OfType<string>().ToList());
                //selectedMedicalRecord.Allergens = new ObservableCollection<string>(list);

                //alergeni parsiranje
                string[] stringAllergens = textAllergensMedicalRecord.Text.Split(", ");
                ObservableCollection<Allergen> allergens = new ObservableCollection<Allergen>();
                foreach (string al in stringAllergens)
                {
                    Allergen allergen = new Allergen(al);
                    allergens.Add(allergen);
                }
                selectedMedicalRecord.Allergens = allergens;

                medicalRecordController.EditMedicalRecord(selectedMedicalRecord.ID, selectedMedicalRecord.UCIN, selectedMedicalRecord.Name, selectedMedicalRecord.Surname, selectedMedicalRecord.PhoneNumber, selectedMedicalRecord.Mail, selectedMedicalRecord.Adress, selectedMedicalRecord.Gender, selectedMedicalRecord.DoB, selectedMedicalRecord.BloodType, selectedMedicalRecord.Reports, selectedMedicalRecord.Allergens); ;
                textAllergensMedicalRecord.Text = "";
            }

            dataGridMedicalRecords.UnselectAll();
        }

        private void deleteMedRecordButton_Click(object sender, RoutedEventArgs e)
        {
            MedicalRecord selectedMedicalRecord = (MedicalRecord)dataGridMedicalRecords.SelectedItem;
            if(selectedMedicalRecord != null)
            {
                if (medicalRecordController.DeleteMedicalRecord(selectedMedicalRecord.ID))
                {
                    textAllergensMedicalRecord.Text = "";
                }

            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            medicalRecordRepo.SaveMedicalRecord();
        }

        private void dataGridMedicalRecords_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(dataGridMedicalRecords.SelectedItem != null)
            {
                MedicalRecord selectedMedicalRecord = (MedicalRecord)dataGridMedicalRecords.SelectedItem;
                String allergens = "";

                foreach (Allergen allergen in selectedMedicalRecord.Allergens)
                {
                    if (allergen.Equals(selectedMedicalRecord.Allergens.Last()))
                    {
                        allergens += allergen.Name;
                        break;
                    }
                    allergens += allergen.Name + ", ";
                }

                textAllergensMedicalRecord.Text = allergens;


                Reports = selectedMedicalRecord.Reports;
            }
            
        }

    }
}
