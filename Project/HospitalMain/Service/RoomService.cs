using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using Model;
using Repository;
using Utility;
using HospitalMain.Enums;

namespace Service
{
   public class RoomService
   {
      
        private readonly RoomRepo _repo;

        public RoomService(RoomRepo roomRepo)
        {
            _repo = roomRepo;
        }

        public bool CreateRoom(Room room)
        {
            // logic for failed addition needed
            return _repo.NewRoom(room);
        }
      
        public bool RemoveRoom(String roomId)
        {
            return _repo.DeleteRoom(roomId);
        }
      
        public void EditRoom(Room newRoom)
        {
            _repo.SetRoom(newRoom);
        }

        public ObservableCollection<Room> GetAllRoomsByExamType(ExaminationTypeEnum examinationTypeEnum)
        {
            if(examinationTypeEnum == ExaminationTypeEnum.OrdinaryExamination)
            {
                return GetAllPatientRooms();
            } else if (examinationTypeEnum == ExaminationTypeEnum.Surgery)
            {
                return GetAllOperationRooms();
            }
            return null;
        }

        public ObservableCollection<Room> GetAllOperationRooms()
        {
            ObservableCollection<Room> operationRooms = new ObservableCollection<Room>();
            foreach(Room room in _repo.Rooms)
            {
                if(room.Type == RoomTypeEnum.Operation_Room)
                {
                    operationRooms.Add(room);
                }
            }

            return operationRooms;
        }
      
        public ObservableCollection<Room> GetAllPatientRooms()
        {
            ObservableCollection<Room> patientRooms = new ObservableCollection<Room>();
            foreach(Room room in _repo.Rooms)
            {
                if(room.Type == RoomTypeEnum.Patient_Room)
                {
                    patientRooms.Add(room);
                }
            }

            return patientRooms;
        }

        public Room ReadRoom(String roomId)
        {
            return _repo.GetRoom(roomId);
        }
      
        public ObservableCollection<Room> ReadAll()
        {
            return _repo.Rooms;
        }

        public bool AddEquipment(String roomId, Equipment equipment)
        {
            return _repo.AddEquipment(roomId, equipment);
        }

        public bool RemoveEquipment(String roomId, String equipmentId)
        {
            return _repo.RemoveEquipment(roomId, equipmentId);
        }
   
        public bool SetClipboardRoom(Room room)
        {
            return _repo.SetClipboardRoom(room);
        }

        public Room GetClipboardRoom()
        {
            return _repo.GetClipboardRoom();
        }

        public bool SetSelectedRoom(Room room)
        {
            return _repo.SetSelectedRoom(room);
        }

        public Room GetSelectedRoom()
        {
            return _repo.GetSelectedRoom();
        }

        public void RemoveSelectedRoom()
        {
            _repo.RemoveSelectedRoom();
        }

        public String GenerateID()
        {
            return _repo.GenerateID();
        }

        public ObservableCollection<Room> QueryRooms(String query)
        {
            List<Room> roomList = new List<Room>(_repo.Rooms);

            ObservableCollection<Room> queriedRooms = new ObservableCollection<Room>(QueryUtility.DoQuery<Room>(roomList, query));

            return queriedRooms;
        }

        public bool LoadRoom()
        {
            return _repo.LoadRoom();
        }

        public bool SaveRoom()
        {
            return _repo.SaveRoom();
        }
   }
}