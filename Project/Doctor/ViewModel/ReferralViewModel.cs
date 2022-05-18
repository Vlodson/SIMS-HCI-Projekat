using Commands;
using Controller;
using Doctor;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ReferralViewModel: ViewModelBase
    {
        private string selectedDoctor;
        private DoctorType selectedSpec;
        private readonly DoctorController _doctorController;
        private readonly ReferralController _referralController;
        private readonly PatientController _patientController;
        private Examination selectedExam;
        private DateTime dateBind;
        private string nameSurnameBind;
        private ObservableCollection<string> doctors;
        public MyICommand ReferralCommand { get; set; }
        public DoctorType SelectedSpec
        {
            get 
            {
                Doctors = _doctorController.GetDoctorsBySpecialization(selectedSpec);
                return selectedSpec;
            }
            set 
            {
                selectedSpec = value;
                Doctors = _doctorController.GetDoctorsBySpecialization(selectedSpec);
            }
        }
        public ObservableCollection<string> Doctors 
        {
            get { return doctors; }
            set
            {
                if (doctors != value)
                {
                    doctors = value;
                    OnPropertyChanged("Doctors");
                }
            }
        }
 
        public List<DoctorType> Specializations
        {
            get
            {
                return filterDoctorTypes();
            }
        }
        public string SelectedDoctor
        {
            get { return selectedDoctor; }
            set
            {
                if (selectedDoctor != value)
                {
                    selectedDoctor = value;
                    OnPropertyChanged("SelectedDoctor");
                    ReferralCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public DateTime DateBind { get => dateBind; set => dateBind = value; }
        public string NameSurnameBind { get => nameSurnameBind; set => nameSurnameBind = value; }

        public ReferralViewModel(Examination exam)
        {
            var app = System.Windows.Application.Current as App;
            _doctorController = app.doctorController;
            _referralController = app.referralController;
            _patientController = app.patientController;
            ReferralCommand = new MyICommand(OnReferral, CanReferral);
            selectedExam = exam;
            NameSurnameBind = _patientController.ReadPatient(exam.PatientId).NameSurname;
            DateBind = exam.Date;


        }
        public List<DoctorType> filterDoctorTypes()
        {
            var spec = Enum.GetValues(typeof(DoctorType)).Cast<DoctorType>().ToList();
            for (int i = 0; i<spec.Count; i++)
            {
                if (spec.ElementAt(i).ToString().Equals("General")||spec.ElementAt(i).ToString().Equals("None"))
                {
                    spec.RemoveAt(i);
                    i--;
                }


            }
            /*List<string> specSerbian = new List<string>();
            foreach (var i in spec)
            {
                switch (i.ToString())
                {
                    case "Neurology":
                        specSerbian.Add("Neurologija");
                        break;
                    case "Dermatology":
                        specSerbian.Add("Dermatologija");
                        break;
                    case "Pulmonology":
                        specSerbian.Add("Pulmologija");
                        break;
                    default:
                        specSerbian.Add("Kardiologija");
                        break;
                }
            }*/
             return spec;
        }
        public bool CanReferral()
        {
            return !string.IsNullOrEmpty(selectedDoctor);
        }
        public void OnReferral()
        {
            Referral referral = new Referral(selectedDoctor, selectedExam.PatientId, (new Random()).Next(10000).ToString(), selectedSpec, selectedExam.Date);
            _referralController.NewReferral(referral);
        }

    }
}
