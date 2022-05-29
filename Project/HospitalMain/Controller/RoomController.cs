using System;
using System.Collections.ObjectModel;

using Service;
using Model;
using HospitalMain.Enums;

namespace Controller
{
   public class RoomController
   {
      
        private readonly RoomService _roomService;
      
        public RoomController(RoomService _roomService)
        {
            this._roomService = _roomService;
        }

        public bool CreateRoom(Room room)
        {
            return _roomService.CreateRoom(room);
        }
      
        public bool RemoveRoom(String roomId)
        {
            return _roomService.RemoveRoom(roomId);
        }
      
        public void EditRoom(Room newRoom)
        {
            _roomService.EditRoom(newRoom);
        }
      
        public Room ReadRoom(String roomId)
        {
            return _roomService.ReadRoom(roomId);
        }
      
        public ObservableCollection<Room> ReadAll()
        {
            return _roomService.ReadAll();
        }

        public bool AddEquipment(String roomId, Equipment equipment)
        {
            return _roomService.AddEquipment(roomId, equipment);
        }

        public bool RemoveEquipment(String roomId, String equipmentId)
        {
            return _roomService.RemoveEquipment(roomId, equipmentId);
        }

        public bool SetClipboardRoom(Room room)
        {
            return _roomService.SetClipboardRoom(room);
        }

        public Room GetClipboardRoom()
        {
            return _roomService.GetClipboardRoom();
        }

        public bool SetSelectedRoom(Room room)
        {
            return _roomService.SetSelectedRoom(room);
        }

        public Room GetSelectedRoom()
        {
            return _roomService.GetSelectedRoom();
        }

        public void RemoveSelectedRoom()
        {
            _roomService.RemoveSelectedRoom();
        }

        public bool LoadRoom()
        {
            return _roomService.LoadRoom();
        }

        public bool SaveRoom()
        {
            return _roomService.SaveRoom();
        }
   }
}