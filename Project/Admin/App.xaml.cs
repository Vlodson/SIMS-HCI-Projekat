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
        public EquipmentController equipmentController { get; set; }

        public App()
        {
            var roomRepo = new RoomRepo(GlobalPaths.RoomsDBPath);
            var roomService = new RoomService(roomRepo);
            roomController = new RoomController(roomService);

            var EquipmentRepo = new EquipmentRepo(GlobalPaths.EquipmentDBPath);
            var EquipmentService = new EquipmentService(EquipmentRepo);
            equipmentController = new EquipmentController(EquipmentService);
        }

    }
}
