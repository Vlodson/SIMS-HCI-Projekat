using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class WorkHoursRequest : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private DateTime startDate;
        private DateTime endDate;
        private ObservableCollection<string> daysInWeek;
        private string id;

        public WorkHoursRequest(DateTime startDate, DateTime endDate, ObservableCollection<string> daysInWeek, string doctorId)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            this.daysInWeek = daysInWeek;
            this.id = doctorId;
        }
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                if (value != startDate)
                {
                    startDate = value;
                    OnPropertyChanged("StartDate");
                }
            }
        }
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                if (value != endDate)
                {
                    endDate = value;
                    OnPropertyChanged("EndDate");
                }
            }
        }
        public string ID
        {
            get { return id; }
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged("ID");
                }
            }
        }
        public ObservableCollection<string> DaysInWeek
        {
            get { return daysInWeek; }
            set
            {
                if (value != daysInWeek)
                {
                    daysInWeek = value;
                    OnPropertyChanged("DaysInWeek");
                }
            }
        }
    }
}
