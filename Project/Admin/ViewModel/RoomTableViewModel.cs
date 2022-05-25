using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Model;
using Controller;
using Utility;
using HospitalMain.Enums;

namespace Admin.ViewModel
{
    public class RoomTableViewModel: BindableBase
    {
        private RoomController roomController;
        public ObservableCollection<FriendlyRoom> Rooms { get; set; }

        public RoomTableViewModel()
        {
            var app = Application.Current as App;
            roomController = app.roomController;

            ObservableCollection<Room> rooms = roomController.ReadAll();

            Rooms = new ObservableCollection<FriendlyRoom>();
            foreach(Room room in rooms)
                Rooms.Add(new FriendlyRoom(room));
        }
    }

    public class FriendlyRoom
    {
        public String Id { get; set; }
        public int Floor { get; set; }
        public int RoomNb { get; set; }
        public bool Occupancy { get; set; }
        public String Type { get; set; }

        public FriendlyRoom(Room room)
        {
            Id = room.Id;
            Floor = room.Floor;
            RoomNb = room.RoomNb;
            Occupancy = room.Occupancy;
            Type = RoomTypeEnumExtensions.ToFriendlyString(room.Type);
        }
    }
}
