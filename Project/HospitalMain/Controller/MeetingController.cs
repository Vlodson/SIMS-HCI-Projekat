using HospitalMain.Model;
using HospitalMain.Service;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMain.Controller
{
    public class MeetingController
    {
        private MeetingsService _meetingsService;

        public MeetingController(MeetingsService meetingsService)
        {
            _meetingsService = meetingsService;
        }

        public int generateID()
        {
            return _meetingsService.generateID();
        }
        public bool BookNewMeeting(Meeting meeting)
        {
            return _meetingsService.BookNewMeeting(meeting);
        }

        public void EditMeeting(Meeting meeting)
        {
            _meetingsService.EditMeeting(meeting);
        }

        public bool DeleteMeeting(String meetingID)
        {
            return _meetingsService.DeleteMeeting(meetingID);
        }

        public Meeting GetMeetingByID(String meetingID)
        {
            return _meetingsService.GetMeetingByID(meetingID);
        }

        public ObservableCollection<Meeting> GetAllMeetings()
        {
            return _meetingsService.GetAllMeetings();
        }

        public ObservableCollection<Room> GetFreeRooms(DateTime dateTime)
        {
            return _meetingsService.GetFreeRooms(dateTime);
        }

        public ObservableCollection<Doctor> GetFreeDoctors(DateTime dateTime)
        {
            return _meetingsService.GetFreeDoctors(dateTime);
        }
        public bool CheckRoomTransferEquipment(Room room, DateTime dateTime)
        {
            return _meetingsService.CheckRoomTransferEquipment(room, dateTime);
        }
        public bool CheckIfTheRoomIsBeingRenovated(Room room, DateTime dateTime)
        {
            return _meetingsService.CheckIfTheRoomIsBeingRenovated(room, dateTime);
        }
    }
}
