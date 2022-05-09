using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

using Controller;
using Service;
using Repository;
using Utility;
using Model;
using HospitalMain.Enums;

namespace Admin
{
    public partial class App : Application
    {
        public RoomController roomController { get; set; }
        public EquipmentController equipmentController { get; set; }
        public EquipmentTransferController equipmentTransferController { get; set; }
        public RenovationController renovationController { get; set; }
        public UserAccountController userAccountController { get; set; }

        public App()
        {
            var equipmentRepo = new EquipmentRepo(GlobalPaths.EquipmentDBPath);
            var roomRepo = new RoomRepo(GlobalPaths.RoomsDBPath, equipmentRepo);
            var equipmentTransferRepo = new EquipmentTransferRepo(GlobalPaths.EquipmentTransfersDBPath, roomRepo, equipmentRepo);
            var renovationRepo = new RenovationRepo(GlobalPaths.RenovationDBPath, roomRepo);
            var userAccountRepo = new UserAccountRepo(GlobalPaths.UserDBPath);
            
            var roomService = new RoomService(roomRepo);
            var equipmentService = new EquipmentService(equipmentRepo, roomRepo);
            var equipmentTransferService = new EquipmentTransferService(equipmentTransferRepo, roomRepo, equipmentRepo);
            var renovationService = new RenovationService(renovationRepo, roomRepo);
            var userAccountService = new UserAccountService(userAccountRepo);
            
            roomController = new RoomController(roomService);
            equipmentController = new EquipmentController(equipmentService);
            equipmentTransferController = new EquipmentTransferController(equipmentTransferService);
            renovationController = new RenovationController(renovationService);
            userAccountController = new UserAccountController(userAccountService);

            if(File.Exists(GlobalPaths.EquipmentDBPath))
                equipmentController.LoadEquipment();
            
            if (File.Exists(GlobalPaths.RoomsDBPath))
                roomController.LoadRoom();

            if (File.Exists(GlobalPaths.EquipmentTransfersDBPath))
                equipmentTransferController.LoadEquipmentTransfer();

            if(File.Exists(GlobalPaths.RenovationDBPath))
                renovationController.LoadRenovation();

            for (int i = 0; i < 20; i++)
            {
                int floor = 1;
                if (i > 10)
                    floor = 2;

                roomController.CreateRoom(i.ToString(), floor, i % 11 + 10 * (floor - 1), false, (RoomTypeEnum)(i % 5));
                equipmentController.CreateEquipment(i.ToString(), i.ToString(), (EquipmentTypeEnum)(i % 10));
                roomController.AddEquipment(i.ToString(), equipmentController.ReadEquipment(i.ToString()));
            }

            for (int i = 0; i < 20; i++)
            {
                Room OriginRoom = roomController.ReadRoom(i.ToString());
                Room DestinationRoom = roomController.ReadRoom(((i + 1) % 20).ToString());
                Equipment equipment = equipmentController.ReadEquipment(i.ToString());
                EquipmentTransfer equipmentTransfer = new EquipmentTransfer(i.ToString(), OriginRoom, DestinationRoom, equipment, new DateOnly(2022, 10, 10), new DateOnly(2022, 11, 10));
                equipmentTransferController.ScheduleTransfer(equipmentTransfer);
                equipmentTransferController.RecordTransfer(i.ToString());
            }

        }

        public static Window GetSpecificWindowType<T>()
        {
            // return first instance of type T of window
            foreach(Window window in App.Current.Windows)
                if(window is T)
                    return window;
            return null;
        }

        public static void HideAllWindows()
        {
            foreach (Window window in App.Current.Windows)
                window.Hide();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            //equipmentController.SaveEquipment();
            //roomController.SaveRoom();
            //equipmentTransferController.SaveEquipmentTransfer();
            //renovationController.SaveRenovation();
        }
    }
}
