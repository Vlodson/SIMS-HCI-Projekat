using Commands;
using Controller;
using Doctor;
using Doctor.View;
using HospitalMain.Enums;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ViewModel
{
    public class EquipmentRequestViewModel: ViewModelBase
    {
        public IEnumerable<EquipmentTypeEnum> EquipmentBind
        {
            get
            {
                return Enum.GetValues(typeof(EquipmentTypeEnum)).Cast<EquipmentTypeEnum>();
            }
        }
        private ObservableCollection<Room> roomBind;
        private readonly RoomController _roomController;
        private EquipmentTypeEnum selectedEquipment;
        private Room selectedRoom;
        private int amount;
        public MyICommand SendCommand { get; set; }
        public MyICommand WorkHoursCommand { get; set; }
        public MyICommand FreeDaysCommand { get; set;}
        public ObservableCollection<Room> RoomBind { get => roomBind; set => roomBind = value; }
        public Room SelectedRoom
        {
            get
            {
                return selectedRoom;
            }
            set
            {
                selectedRoom = value;
                OnPropertyChanged("SelectedRoom");
                SendCommand.RaiseCanExecuteChanged();
            }
        }
        public EquipmentTypeEnum SelectedEquipment
        {
            get
            {
                return selectedEquipment;
            }
            set
            {
                selectedEquipment = value;
                OnPropertyChanged("SelectedEquipment");
                SendCommand.RaiseCanExecuteChanged();
            }
        }
        public int Amount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = value;
                OnPropertyChanged("Amount");
                SendCommand.RaiseCanExecuteChanged();
            }
        }

        public EquipmentRequestViewModel()
        {
            var app = System.Windows.Application.Current as App;
            _roomController = app.roomController;

            RoomBind = _roomController.ReadAll();

            SendCommand = new MyICommand(OnSend, CanSend);
            FreeDaysCommand = new MyICommand(OnFreeDays);
            WorkHoursCommand = new MyICommand(OnWorkHours);
        }
        public void OnFreeDays()
        {
            FreeDaysRequestPage requestPage = new FreeDaysRequestPage();
            DoctorNavBar.navigation.Navigate(requestPage);
        }
        public void OnWorkHours()
        {
            Doctor.View.WorkHoursRequest requestPage = new Doctor.View.WorkHoursRequest();
            DoctorNavBar.navigation.Navigate(requestPage);
        }
        public bool CanSend()
        {
            return SelectedRoom != null && Amount > 0;
        }
        public void OnSend()
        {
            EquipmentRequest request = new EquipmentRequest(selectedEquipment, amount, selectedRoom, MainWindow._uid, DateTime.Now);
            MessageBox.Show("Uspesno ste poslali zahtev za opremu!");
            EquipmentRequestPage requestPage = new EquipmentRequestPage();
            DoctorNavBar.navigation.Navigate(requestPage);
        }
    }
}
