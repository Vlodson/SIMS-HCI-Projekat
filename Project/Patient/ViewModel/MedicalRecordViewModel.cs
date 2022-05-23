using Controller;
using HospitalMain.Enums;
using Model;
using Patient.View;
using Patient.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Patient.ViewModel
{
    public class MedicalRecordViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private MedicalRecordController _medicalRecordController;
        private DoctorController _doctorController;

        private String name;
        private String surname;
        private String jmbg;
        private DateTime dateBirth;
        private String mail;
        private String phone;
        private String bloodType;
        private String gender;
        private String allergens;
        private List<Report> reports;
        private Report selectedReport;

        public MyICommand MenuBack { get; set; }

        private Page thisPage;

        public String Name { get { return name; } set { name = value; } }
        public String Surname { get { return surname; } set { surname = value;} }
        public String Jmbg { get { return jmbg; } set { jmbg = value; } }
        public DateTime DateBirth { get { return dateBirth; } set { dateBirth = value; } } 
        public String Mail { get { return mail; } set { mail = value; } } 
        public String Phone { get { return phone; } set { phone = value; } }
        public String BloodType { get { return bloodType; } set { bloodType = value;} }
        public String Gender { get { return gender; } set { gender = value; } }
        public String Allergens { get { return allergens; } set { allergens = value; } }
        public List<Report> Reports { get { return reports; } }
        public Report SelectedReport { get { return selectedReport; } set { selectedReport = value; OnPropertyChanged("SelectedReport"); ViewReportCommand.RaiseCanExecuteChanged(); } }

        public MyICommand ChangeMedicalRecordCommand { get; set; }
        public MyICommand ViewReportCommand { get; set; }

        public MedicalRecordViewModel(Page page)
        {
            App app = Application.Current as App;
            _medicalRecordController = app.MedicalRecordController;
            _doctorController = app.DoctorController;

            MenuBack = new MyICommand(OnMenuBack);

            thisPage = page;

            ChangeMedicalRecordCommand = new MyICommand(OnChangeMedicalRecord);
            ViewReportCommand = new MyICommand(OnViewReport, CanViewReport);

            MedicalRecord medicalRecord = _medicalRecordController.GetMedicalRecord(Login.loggedId);
            Name = medicalRecord.Name;
            Surname = medicalRecord.Surname;
            Jmbg = medicalRecord.UCIN;
            DateBirth = medicalRecord.DoB;
            Mail = medicalRecord.Mail;
            Phone = medicalRecord.PhoneNumber;
            switch (medicalRecord.BloodType)
            {
                case HospitalMain.Enums.BloodType.A_positive:
                    BloodType = "A+";
                    break;
                case HospitalMain.Enums.BloodType.B_positive:
                    BloodType = "B+";
                    break;
                case HospitalMain.Enums.BloodType.AB_positive:
                    BloodType = "B+";
                    break;
                case HospitalMain.Enums.BloodType.O_positive:
                    BloodType = "0+";
                    break;
                case HospitalMain.Enums.BloodType.A_negative:
                    BloodType = "A-";
                    break;
                case HospitalMain.Enums.BloodType.B_negative:
                    BloodType = "B-";
                    break;
                case HospitalMain.Enums.BloodType.AB_negative:
                    BloodType = "B-";
                    break;
                case HospitalMain.Enums.BloodType.O_negative:
                    BloodType = "0-";
                    break;

            }
            switch (medicalRecord.Gender)
            {
                case HospitalMain.Enums.Gender.Ženski:
                    Gender = "Ž";
                    break;
                case HospitalMain.Enums.Gender.Muški:
                    Gender = "M";
                    break;
            }

            List<Allergens> allergens = medicalRecord.Allergens.ToList();
            foreach(Allergens allergen in allergens)
            {
                switch (allergen)
                {
                    case HospitalMain.Enums.Allergens.Grinje:
                        Allergens = Allergens + "grinje";
                        break;
                    case HospitalMain.Enums.Allergens.Prašina:
                        Allergens = Allergens + "prašina";
                        break;
                    case HospitalMain.Enums.Allergens.Polen:
                        Allergens = Allergens + "polen";
                        break;
                    case HospitalMain.Enums.Allergens.Lešnici:
                        Allergens = Allergens + "lešnici";
                        break;
                    case HospitalMain.Enums.Allergens.Orasi:
                        Allergens = Allergens + "orasi";
                        break;
                    case HospitalMain.Enums.Allergens.Ambrozija:
                        Allergens = Allergens + "ambrozija";
                        break;
                    case HospitalMain.Enums.Allergens.Perje:
                        Allergens = Allergens + "perje";
                        break;
                    case HospitalMain.Enums.Allergens.Bademi:
                        Allergens = Allergens + "bademi";
                        break;
                    case HospitalMain.Enums.Allergens.Kopriva:
                        Allergens = Allergens + "kopriva";
                        break;

                }
                Allergens = Allergens + " ";
                foreach(Report report in medicalRecord.Reports.ToList())
                {
                    Doctor doctor = _doctorController.GetDoctor(report.DoctorId);
                    report.DoctorNameSurname = doctor.NameSurname;
                    switch (doctor.Type)
                    {
                        case DoctorType.Pulmonology:
                            report.DoctorType = "pulmologija";
                            break;
                        case DoctorType.Cardiology:
                            report.DoctorType = "kardiologija";
                            break;
                        case DoctorType.Dermatology:
                            report.DoctorType = "dermatologija";
                            break;
                        case DoctorType.Neurology:
                            report.DoctorType = "neurologija";
                            break;
                        case DoctorType.General:
                            report.DoctorType = "opšta praksa";
                            break;
                    }
                }
                reports = medicalRecord.Reports.ToList();
            }
        }

        public void OnMenuBack()
        {
            //Window.GetWindow(thisWindow).Content = new PatientMenu();
            thisPage.Visibility = Visibility.Collapsed;
        }

        public void OnChangeMedicalRecord()
        {
            ChangeMedicalRecord changeMedicalRecord = new ChangeMedicalRecord();
            changeMedicalRecord.ShowDialog();
        }

        public bool CanViewReport()
        {
            return SelectedReport != null;
        }
        public void OnViewReport()
        {
            ReportWindow reportWindow = new ReportWindow(selectedReport);
            reportWindow.ShowDialog();
        }
    }
}
