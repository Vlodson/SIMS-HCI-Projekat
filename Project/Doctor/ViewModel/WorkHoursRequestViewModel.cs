using Commands;
using Doctor;
using Doctor.View;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class WorkHoursRequestViewModel: ViewModelBase
    {
        private string startDate;
        private string endDate;
        private ObservableCollection<string> daysInWeek;
        private bool isSelectedMon;
        private bool isSelectedTue;
        private bool isSelectedWed;
        private bool isSelectedThu;
        private bool isSelectedFri;
        private bool isSelectedSat;
        private bool isSelectedSun;
        public MyICommand SendCommand { get; set; }
        public MyICommand EquipmentCommand { get; set; }
        public MyICommand FreeDaysCommand { get; set; }
        ObservableCollection<string> DaysInWeek { get; set; }
        public string EndDate
        {
            get { return endDate; }
            set
            {
                if (endDate != value)
                {
                    endDate = value;
                    OnPropertyChanged("EndDate");
                }
            }
        }
        public bool IsSelectedMon
        {
            get { return isSelectedMon; }
            set
            {
                if (isSelectedMon != value)
                {
                    isSelectedMon = value;
                    OnPropertyChanged("IsSelectedMon");
                }
            }
        }
        public bool IsSelectedTue
        {
            get { return isSelectedTue; }
            set
            {
                if (isSelectedTue != value)
                {
                    isSelectedTue = value;
                    OnPropertyChanged("IsSelectedTue");
                }
            }
        }
        public bool IsSelectedWed
        {
            get { return isSelectedWed; }
            set
            {
                if (isSelectedWed != value)
                {
                    isSelectedWed = value;
                    OnPropertyChanged("IsSelectedWed");
                }
            }
        }
        public bool IsSelectedThu
        {
            get { return isSelectedThu; }
            set
            {
                if (isSelectedThu != value)
                {
                    isSelectedThu = value;
                    OnPropertyChanged("IsSelectedThu");
                }
            }
        }
        public bool IsSelectedFri
        {
            get { return isSelectedFri; }
            set
            {
                if (isSelectedFri != value)
                {
                    isSelectedFri = value;
                    OnPropertyChanged("IsSelectedFri");
                }
            }
        }
        public bool IsSelectedSat
        {
            get { return isSelectedSat; }
            set
            {
                if (isSelectedSat != value)
                {
                    isSelectedSat = value;
                    OnPropertyChanged("IsSelectedSat");
                }
            }
        }
        public bool IsSelectedSun
        {
            get { return isSelectedSun; }
            set
            {
                if (isSelectedSun != value)
                {
                    isSelectedSun = value;
                    OnPropertyChanged("IsSelectedSun");
                }
            }
        }

        public string StartDate
        {
            get { return startDate; }
            set
            {
                if (startDate != value)
                {
                    startDate = value;
                    OnPropertyChanged("StartDate");
                }
            }
        }
        public WorkHoursRequestViewModel()
        {
            SendCommand = new MyICommand(OnSend);
            EquipmentCommand = new MyICommand(OnEquipment);
            FreeDaysCommand = new MyICommand(OnFreeDays);
        }
        public void OnSend()
        {
            daysInWeek = new ObservableCollection<string>();
            Dictionary< string, bool> selecteds = new Dictionary<string, bool>();
            selecteds.Add("sunday", IsSelectedSun);
            selecteds.Add( "saturday", IsSelectedSat);
            selecteds.Add( "friday", IsSelectedFri);
            selecteds.Add( "thursday", IsSelectedThu);
            selecteds.Add( "monday", IsSelectedMon);
            selecteds.Add( "tuesday", IsSelectedTue);
            selecteds.Add( "wednesday", IsSelectedWed);
            foreach (var selected in selecteds)
            {
                if (selected.Value)
                {
                    daysInWeek.Add(selected.Key);
                }
            }
            Model.WorkHoursRequest request = new Model.WorkHoursRequest(DateTime.Parse(startDate), DateTime.Parse(endDate), daysInWeek, MainWindow._uid);
        }
        public void OnFreeDays()
        {
            FreeDaysRequestPage requestPage = new FreeDaysRequestPage();
            DoctorNavBar.navigation.Navigate(requestPage);
        }
        public void OnEquipment()
        {
            EquipmentRequestPage requestPage = new EquipmentRequestPage();
            DoctorNavBar.navigation.Navigate(requestPage);
        }
    }
}
