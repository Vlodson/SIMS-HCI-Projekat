using HospitalMain.Enums;
using HospitalMain.Model;
using HospitalMain.Repository;
using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMain.Service
{
    public class MeetingsService
    {
        private MeetingsRepo _meetingsRepo;
        private DoctorRepo _doctorRepo;
        private RoomRepo _roomRepo;
        private DoctorService _doctorService;
        private RenovationRepo _renovationRepo;
        private EquipmentTransferRepo _equipmentTransferRepo;

        public MeetingsService(MeetingsRepo meetingsRepo, DoctorRepo doctorRepo, RoomRepo roomRepo, DoctorService doctorService, RenovationRepo renovationRepo, EquipmentTransferRepo equipmentTransferRepo)
        {
            _meetingsRepo = meetingsRepo;
            _doctorRepo = doctorRepo;
            _roomRepo = roomRepo;
            _doctorService = doctorService;
            _renovationRepo = renovationRepo;
            _equipmentTransferRepo = equipmentTransferRepo;
        }

        public bool BookNewMeeting(Meeting newMeeting)
        {
            return _meetingsRepo.BookNewMeeting(newMeeting);
        }

        public void EditMeeting(Meeting meeting)
        {
            _meetingsRepo.EditMeeting(meeting);
        }

        public bool DeleteMeeting(String meetingID)
        {
            return _meetingsRepo.DeleteMeeting(meetingID);
        }

        public ObservableCollection<Doctor> GetFreeDoctors(DateTime dateTime)
        {
            ObservableCollection<Doctor> doctors = _doctorRepo.DoctorList;
            foreach(Doctor doctor in doctors)
            {
                foreach(Examination exam in _doctorService.ExaminationsForDoctor(doctor.Id))
                {
                    if(DateTime.Compare(exam.Date, dateTime) == 0)
                    {
                        doctors.Remove(doctor);
                        break;
                    }
                }
            }
            return doctors;
        }

        public bool CheckRoomTransferEquipment(Room room, DateTime dateTime)
        {
            foreach(EquipmentTransfer equipmentTransfer in _equipmentTransferRepo.equipmentTransfers)
            {
                if((room.Id.Equals(equipmentTransfer.DestinationRoom.Id) || room.Id.Equals(equipmentTransfer.OriginRoom.Id)) && dateTime >= equipmentTransfer.StartDate && dateTime <= equipmentTransfer.EndDate)
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckIfTheRoomIsBeingRenovated(Room room, DateTime dateTime)
        {
            foreach(Renovation renovation in _renovationRepo.Renovations)
            {
                if((room.Id.Equals(renovation.DestinationRoom.Id) || room.Id.Equals(renovation.OriginRoom.Id)) && DateOnly.Parse(dateTime.ToShortDateString()) >= renovation.StartDate && DateOnly.Parse(dateTime.ToShortDateString()) <= renovation.EndDate)
                {
                    return false;
                }
            }
            return true;
        }
        
        public ObservableCollection<Room> GetFreeRooms(DateTime dateTime)
        {
            ObservableCollection<Room> freeMeetingRooms = GetAllMeetingRooms();
            foreach(Room room in freeMeetingRooms)
            {
                CheckIfRoomIsFree(room, dateTime, freeMeetingRooms);
            }
            return freeMeetingRooms;
        }

        public void CheckIfRoomIsFree(Room room, DateTime dateTime, ObservableCollection<Room> freeMeetingRooms)
        {
            ObservableCollection<Meeting> meetings = _meetingsRepo.MeetingsList;
            foreach(Meeting meeting in meetings)
            {
                if (meeting.RoomID.Equals(room.Id) && dateTime < meeting.DateTime.AddMinutes(30) && dateTime > meeting.DateTime.AddMinutes(-30))
                {
                    freeMeetingRooms.Remove(room);
                    break;
                }
            }
        }

        private ObservableCollection<Room> GetAllMeetingRooms()
        {
            ObservableCollection<Room> rooms = _roomRepo.Rooms;
            foreach(Room room in rooms)
            {
                if(room.Type != RoomTypeEnum.Meeting_Room)
                {
                    rooms.Remove(room);
                }
            }
            return rooms;
        }
    }
}
