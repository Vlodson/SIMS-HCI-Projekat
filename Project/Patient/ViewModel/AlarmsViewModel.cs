using Patient.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Patient.ViewModel
{
    public class AlarmsViewModel : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public MyICommand AddPersonalNotificationCommand { get; set; }
        public AlarmsViewModel()
        {
            App app = Application.Current as App;

            AddPersonalNotificationCommand = new MyICommand(OnAddPersonalNotification);
        }

        private void OnAddPersonalNotification()
        {
            NEwAlarm newAlarm = new NEwAlarm();
            newAlarm.ShowDialog();
        }

    }
}
