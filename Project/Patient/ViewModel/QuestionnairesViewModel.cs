using Controller;
using HospitalMain.Controller;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Patient.ViewModel
{
    public class QuestionnairesViewModel : BindableBase
    {
        public List<String> HospitalQuestionnary { get; set; }
        public PatientController _patientController;
        public QuestionnaireController _questionnaireController;
        public String SelectedAnswer { get; set; }

        private String groupName;
        private bool checked1;
        private bool checked2;
        private bool checked3;
        private bool checked4;
        private bool checked5;
        
        public bool Checked1 
        {
            get
            {
                return checked1;
            }
            set
            {
                if(checked1 != value)
                {
                    checked1 = value;
                    OnPropertyChanged("Checked1");
                }
            }
        }

        public String GroupName
        {
            get
            {
                return groupName;
            }
            set
            {
                value = groupName;
                OnPropertyChanged("GroupName");
            }
        }
        public bool Checked2 
        {
            get
            {
                return checked2;
            }
            set
            {
                if (checked2 != value)
                {
                    checked2 = value;
                    OnPropertyChanged("Checked2");
                }
            }
        }
        public bool Checked3 
        {
            get
            {
                return checked3;
            }
            set
            {
                if (checked3 != value)
                {
                    checked3 = value;
                    OnPropertyChanged("Checked3");
                }
            }
        }
        public bool Checked4 
        {
            get
            {
                return checked4;
            }
            set
            {
                if (checked4 != value)
                {
                    checked4 = value;
                    OnPropertyChanged("Checked4");
                }
            }
        }

        public bool Checked5 
        {
            get
            {
                return checked5;
            }
            set
            {
                if (checked5 != value)
                {
                    checked5 = value;
                    OnPropertyChanged("Checked5");
                }
            }
        }

        public String selectedQuestion { get; set; }
        public QuestionnairesViewModel()
        {
            App app = Application.Current as App;
            _patientController = app.PatientController;
            _questionnaireController = app.QuestionnaireController;

            HospitalQuestionnary = _questionnaireController.GetHospitalQuestionnaire().Questions;
            
        }

        
    }
}
