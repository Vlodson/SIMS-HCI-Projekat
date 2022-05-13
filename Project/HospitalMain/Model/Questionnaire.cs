using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMain.Model
{
    public class Questionnaire : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private String idDoctor;
        private List<String> questions;

        public String IdDoctor
        {
            get
            {
                return idDoctor;
            }
            set
            {
                idDoctor = value;
                OnPropertyChanged("IdDoctor");
            }
        }

        public List<String> Questions
        {
            get
            {
                return questions;
            }
            set
            {
                questions = value;
                OnPropertyChanged("Questions");
            }
        }

        public Questionnaire(string idDoctor, List<string> questions)
        {
            this.idDoctor = idDoctor;
            this.questions = questions;
        }

        public Questionnaire()
        {
        }
    }
}
