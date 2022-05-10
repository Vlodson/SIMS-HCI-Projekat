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

        public Answer(string idDoctor, List<int> grades)
        {
            this.idDoctor = idDoctor;
            this.grades = grades;
        }

        public Answer()
        {
        }
    }
}
