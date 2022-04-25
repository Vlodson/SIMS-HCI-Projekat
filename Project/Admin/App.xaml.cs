﻿using System;
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
using HospitalMain.Enums;

namespace Admin
{
    public partial class App : Application
    {
        public RoomController roomController { get; set; }
        public EquipmentController equipmentController { get; set; }
        public EquipmentTransferController equipmentTransferController { get; set; }
        public RenovationController renovationController { get; set; }

        public App()
        {
            var equipmentRepo = new EquipmentRepo(GlobalPaths.EquipmentDBPath);
            var roomRepo = new RoomRepo(GlobalPaths.RoomsDBPath, equipmentRepo);
            var equipmentTransferRepo = new EquipmentTransferRepo(GlobalPaths.EquipmentTransfersDBPath, roomRepo, equipmentRepo);
            var renovationRepo = new RenovationRepo(GlobalPaths.RenovationDBPath, roomRepo);
            
            var roomService = new RoomService(roomRepo);
            var equipmentService = new EquipmentService(equipmentRepo, roomRepo);
            var equipmentTransferService = new EquipmentTransferService(equipmentTransferRepo, roomRepo, equipmentRepo);
            var renovationService = new RenovationService(renovationRepo, roomRepo);
            
            roomController = new RoomController(roomService);
            equipmentController = new EquipmentController(equipmentService);
            equipmentTransferController = new EquipmentTransferController(equipmentTransferService);
            renovationController = new RenovationController(renovationService);

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
                equipmentTransferController.ScheduleTransfer(i.ToString(), i.ToString(), ((i + 1) % 20).ToString(), i.ToString(), new DateOnly(2022, 10, 10), new DateOnly(2022, 11, 10));
                equipmentTransferController.RecordTransfer(i.ToString(), "Pera");
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
