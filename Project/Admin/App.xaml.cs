using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Controller;
using Service;
using Repository;
using Model;

namespace Admin
{
    public partial class App : Application
    {
        public RoomController roomController { get; set; }

        public App()
        {
            List<Room> rooms = new List<Room>();

            var roomRepo = new RoomRepo("empty", rooms);
            var roomService = new RoomService(roomRepo);
            roomController = new RoomController(roomService);
        }

    }
}
