using HospitalMain.Controller;
using Model;
using Secretary.ComboBoxTemplate;
using Secretary.Commands;
using Secretary.ViewUtils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Secretary.ViewModel
{
    public class EditMeetingViewModel : ViewModelBase
    {
        private MeetingController _meetingCotroller;

        private String _meetingTopic;
        public String MeetingTopic
        {
            get { return _meetingTopic; }
            set { _meetingTopic = value; OnPropertyChanged(nameof(MeetingTopic)); }
        }

        private DateTime _dateTime = DateTime.Now;
        public DateTime DateTime
        {
            get { return _dateTime; }
            set
            {
                _dateTime = value;
                OnPropertyChanged(nameof(DateTime));
                FillDoctorComboBoxData();
                FillRoomComboBoxData();
            }
        }

        private List<ComboBoxData<Room>> roomComboBox = new List<ComboBoxData<Room>>();
        public List<ComboBoxData<Room>> RoomComboBox
        {
            get { return roomComboBox; }
            set { roomComboBox = value; OnPropertyChanged(nameof(RoomComboBox)); }
        }

        //selected
        private Room _room;
        public Room Room
        {
            get { return _room; }
            set { _room = value; OnPropertyChanged(nameof(Room)); }
        }

        private void FillRoomComboBoxData()
        {
            roomComboBox.Clear();
            ObservableCollection<Room> rooms = _meetingCotroller.GetFreeRooms(DateTime);
            foreach (Room room in rooms)
            {
                roomComboBox.Add(new ComboBoxData<Room> { Name = room.RoomNb.ToString(), Value = room });
            }
            Room = rooms.First();
        }

        private ObservableCollection<SelectableItemWrapper<Doctor>> doctorListBox = new ObservableCollection<SelectableItemWrapper<Doctor>>();
        public ObservableCollection<SelectableItemWrapper<Doctor>> DoctorListBox
        {
            get { return doctorListBox; }
            set { doctorListBox = value; OnPropertyChanged(nameof(DoctorListBox)); }
        }

        //selected
        private ObservableCollection<Doctor> _doctors;
        public ObservableCollection<Doctor> Doctors
        {
            get { return _doctors; }
            set { _doctors = value; OnPropertyChanged(nameof(Doctors)); }
        }

        private void FillDoctorComboBoxData()
        {
            doctorListBox.Clear();
            foreach (Doctor doctor in _meetingCotroller.GetFreeDoctors(DateTime))
            {
                doctorListBox.Add(new SelectableItemWrapper<Doctor> { IsSelected = false, Item = doctor });
            }
        }

        public ICommand EditMeetingCommand { get; }

        public EditMeetingViewModel(HomeViewModel homeViewModel, HomePageMeetingsViewModel homePageMeetingsViewModel)
        {
            var app = System.Windows.Application.Current as App;
            _meetingCotroller = app.MeetingController;

            Doctors = new ObservableCollection<Doctor>();

            FillDoctorComboBoxData();
            FillRoomComboBoxData();

            EditMeetingCommand = new EditMeetingCommand(_meetingCotroller, this, homeViewModel, homePageMeetingsViewModel);
        }
    }
}
