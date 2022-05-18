using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMain.Model
{
    public class Answer : INotifyPropertyChanged
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
        private List<int> grades;
        private int counterGrades;

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
        public List<int> Grades
        {
            get
            {
                return grades;
            }
            set
            {
                grades = value;
                OnPropertyChanged("Grades");
            }
        }

        public int CounterGrades
        {
            get
            {
                return counterGrades;
            }
            set
            {
                counterGrades = value;
                OnPropertyChanged("CounterGrades");
            }
        }

        public Answer(string idDoctor, List<int> grades, int counterGrades)
        {
            this.idDoctor = idDoctor;
            this.grades = grades;
            this.counterGrades = counterGrades;
        }

        public Answer()
        {
        }
    }
}
