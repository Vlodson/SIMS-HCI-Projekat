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
using Utility;

namespace Admin
{
    public partial class App : Application
    {
        public RoomController roomController { get; set; }
        public RoomRepo roomRepo { get; set; }

        public App()
        {
            roomRepo = new RoomRepo(GlobalPaths.RoomsDBPath);
            var roomService = new RoomService(roomRepo);
            roomController = new RoomController(roomService);
        }

    }
}
