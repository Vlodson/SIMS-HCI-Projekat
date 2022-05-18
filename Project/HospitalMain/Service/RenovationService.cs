using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


using Repository;
using Model;
using HospitalMain.Enums;

namespace Service
{
    public class RenovationService
    {
        private readonly RenovationRepo _renovationRepo;
        private readonly RoomRepo _roomRepo;
        private readonly ExaminationRepo _examinationRepo;

        public RenovationService(RenovationRepo renovationRepo, RoomRepo roomRepo, ExaminationRepo examinationRepo)
        {
            _renovationRepo = renovationRepo;
            _roomRepo = roomRepo;
            _examinationRepo = examinationRepo;
        }

        public bool ScheduleRenovation(String id, String originRoomId, String destinationRoomId, RenovationTypeEnum type, DateOnly startDate, DateOnly endDate)
        {
            Room originRoom = _roomRepo.GetRoom(originRoomId);
            Room destinationRoom = _roomRepo.GetRoom(destinationRoomId);
            Renovation renovation = new Renovation(id, originRoom, destinationRoom, type, startDate, endDate);
            _renovationRepo.NewRenovation(renovation);
            return true;
        }

        public bool RecordRenovation(String renovationId)
        {
            Renovation renovation = _renovationRepo.GetRenovation(renovationId);
            Room originRoom = renovation.OriginRoom;
            
            _roomRepo.SetRoom(originRoom.Id, originRoom.Equipment, originRoom.Floor, originRoom.RoomNb, originRoom.Occupancy, RoomTypeEnum.Inoperative, originRoom.PreviousType);
            _renovationRepo.SetRenovation(renovation.Id, renovation.OriginRoom, renovation.DestinationRoom, renovation.Type, renovation.StartDate, renovation.EndDate);
            
            return true;
        }

        public bool OccupiedAtTheTime(Renovation renovation)
        {
            foreach (Examination examination in _examinationRepo.examinationList)
            {
                if (renovation.OriginRoom.Id == examination.ExamRoomId) // destination room missing here. to add after merge/split
                    if (renovation.StartDate >= DateOnly.Parse(examination.Date.ToString()) && renovation.EndDate <= DateOnly.Parse(examination.Date.AddMinutes(examination.Duration).ToString()))
                        return false;

            }

            return true;
        }

        public void FinishRenovation()
        {
            foreach(Renovation renovation in _renovationRepo.Renovations)
            {
                if (renovation.EndDate >= DateOnly.Parse(DateTime.Now.ToString()))
                    _roomRepo.SetRoom(renovation.OriginRoom.Id, renovation.OriginRoom.Equipment, renovation.OriginRoom.Floor, renovation.OriginRoom.RoomNb, renovation.OriginRoom.Occupancy, renovation.OriginRoom.PreviousType, renovation.OriginRoom.PreviousType);
            }
        }

        public void MergeRooms(Renovation renovation)
        {
            // generate new params for room
            List<Room> roomList = new List<Room>(_roomRepo.Rooms);
            int id = roomList.Max(r => int.Parse(r.Id.ToString())) + 1;
            int number = roomList.Where(r => r.Floor == renovation.OriginRoom.Floor).Max(r1 => r1.RoomNb) + 1;

            // make new room
            Room newRoom = new Room(id.ToString(), renovation.OriginRoom.Floor, number, false, RoomTypeEnum.Inoperative, renovation.OriginRoom.Type);
            _roomRepo.NewRoom(newRoom);

            // add room equipment
            ObservableCollection<Equipment> originEquipment = renovation.OriginRoom.Equipment;
            ObservableCollection<Equipment> destinationEquipment = renovation.DestinationRoom.Equipment;

            foreach (Equipment equipment in originEquipment)
                _roomRepo.AddEquipment(newRoom.Id, equipment);

            foreach (Equipment equipment in destinationEquipment)
                _roomRepo.AddEquipment(newRoom.Id, equipment);

            // delete rooms
            _roomRepo.DeleteRoom(renovation.OriginRoom.Id);
            _roomRepo.DeleteRoom(renovation.DestinationRoom.Id);
        }

        public void SplitRoom(Renovation renovation)
        {
            // generate new params for room
            List<Room> roomList = new List<Room>(_roomRepo.Rooms);
            int id = roomList.Max(r => int.Parse(r.Id.ToString())) + 1;
            int number = roomList.Where(r => r.Floor == renovation.OriginRoom.Floor).Max(r1 => r1.RoomNb) + 1;

            // change origin status
            Room originRoom = renovation.OriginRoom;
            _roomRepo.SetRoom(originRoom.Id, originRoom.Equipment, originRoom.Floor, originRoom.RoomNb, originRoom.Occupancy, RoomTypeEnum.Inoperative, originRoom.Type);

            // generate new room and add it to the repo
            Room newRoom = new Room(id.ToString(), originRoom.Floor, number, false, RoomTypeEnum.Inoperative, originRoom.Type);
            _roomRepo.NewRoom(newRoom);
        }

        public bool DeleteRenovation(String renovationId)
        {
            return _renovationRepo.DeleteRenovation(renovationId);
        }

        public bool SetClipboardRenovation(Renovation renovation)
        {
            return _renovationRepo.SetClipboardRenovation(renovation);
        }

        public Renovation GetClipboardRenovation()
        {
            return _renovationRepo.GetClipboardRenovation();
        }

        public ObservableCollection<Renovation> ReadAll()
        {
            return _renovationRepo.Renovations;
        }

        public bool LoadRenovation()
        {
            return _renovationRepo.LoadRenovation();
        }

        public bool SaveRenovation()
        {
            return _renovationRepo.SaveRenovation();
        }
    }
}
