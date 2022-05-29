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

        public bool ScheduleRenovation(Renovation renovation)
        {
            _renovationRepo.NewRenovation(renovation);
            return true;
        }

        public bool RecordRenovation(String renovationId)
        {
            Renovation renovation = _renovationRepo.GetRenovation(renovationId);
            Room originRoom = renovation.OriginRoom;
            
            _roomRepo.SetRoom(originRoom);
            _renovationRepo.SetRenovation(renovation);
            
            return true;
        }

        public bool OccupiedAtTheTime(Renovation renovation)
        {
            foreach (Examination examination in _examinationRepo.Examinations)
            {
                if (renovation.OriginRoom.Id == examination.ExamRoomId) // destination room missing here. to add after merge/split
                    if (renovation.StartDate >= DateOnly.Parse(examination.Date.ToShortDateString()) && renovation.EndDate <= DateOnly.Parse(examination.Date.AddMinutes(examination.Duration).ToShortDateString()))
                        return false;

            }

            return true;
        }

        public void FinishRenovation()
        {
            foreach(Renovation renovation in _renovationRepo.Renovations)
            {
                if (renovation.EndDate >= DateOnly.Parse(DateTime.Now.ToShortDateString()))
                    _roomRepo.SetRoom(renovation.OriginRoom);
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
            TransferAllRoomEquipment(renovation.OriginRoom.Equipment, newRoom);
            TransferAllRoomEquipment(renovation.DestinationRoom.Equipment, newRoom);

            // delete rooms
            _roomRepo.DeleteRoom(renovation.OriginRoom.Id);
            _roomRepo.DeleteRoom(renovation.DestinationRoom.Id);
        }

        private void TransferAllRoomEquipment(ObservableCollection<Equipment> equipment, Room destination)
        {
            ObservableCollection<Equipment> equipmentCopy = new ObservableCollection<Equipment>(equipment);

            foreach(Equipment eq in equipmentCopy)
            {
                _roomRepo.AddEquipment(destination.Id, eq);
                eq.RoomId = destination.Id;
            }
        }

        public void SplitRoom(Renovation renovation)
        {
            // generate new params for room
            List<Room> roomList = new List<Room>(_roomRepo.Rooms);
            int id = roomList.Max(r => int.Parse(r.Id.ToString())) + 1;
            int number = roomList.Where(r => r.Floor == renovation.OriginRoom.Floor).Max(r1 => r1.RoomNb) + 1;

            // change origin status
            _roomRepo.SetRoom(renovation.OriginRoom);

            // generate new room and add it to the repo
            Room newRoom = new Room(id.ToString(), renovation.OriginRoom.Floor, number, false, RoomTypeEnum.Inoperative, renovation.OriginRoom.Type);
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
