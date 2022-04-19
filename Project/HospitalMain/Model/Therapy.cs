using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMain.Model
{
    public class Therapy: INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private string id;
        private string medicine; //zamjeniti sa klasom Medicine(Lek) kad se implementira
        private int duration;
        private int perDay;
        private bool prescription; //da li se izdaje recept za ovaj lek ili ne

        public Therapy(string id, string medicine, int duration, int perDay, bool prescription )
        {
            this.id = id;
            this.medicine = medicine;
            this.duration = duration;
            this.perDay = perDay;
            this.prescription = prescription;
        }

        public string Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    // OnPropertyChanged("Id");
                }
            }
        }

        public int Duration
        {
            get { return duration; }
            set
            {
                if (duration != value)
                {
                    duration = value;
                    OnPropertyChanged("Duration");
                }
            }
        }

        public int PerDay
        {
            get { return perDay; }
            set
            {
                if (perDay != value)
                {
                    perDay = value;
                    OnPropertyChanged("PerDay");
                }
            }
        }

        public string Medicine 
        {
            get { return medicine; }
            set
            {
                if (medicine != value)
                {
                    medicine = value;
                    OnPropertyChanged("Medicine");
                }
            }
        }
    }
}
