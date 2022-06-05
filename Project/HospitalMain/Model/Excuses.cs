using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Excuses : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string examId;
        private DateTime from;
        private DateTime to;
        public Excuses(string examId, DateTime from, DateTime to)
        {
            this.examId = examId;
            this.from = from;
            this.to = to;
        }
        public string ExamId
        {
            get
            {
                return examId;
            }
            set
            {
                examId = value;
                OnPropertyChanged("ExamId");
            }
        }
        public DateTime From
        {
            get
            {
                return from;
            }
            set
            {
                from = value;
                OnPropertyChanged("From");
            }
        }
        public DateTime To
        {
            get
            {
                return to;
            }
            set
            {
                to = value;
                OnPropertyChanged("To");
            }
        }

    }
}
