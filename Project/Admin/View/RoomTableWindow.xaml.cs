using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

using Model;
using Controller;
using HospitalMain.Enums;
using Utility;

namespace Admin.View
{
    /// <summary>
    /// Interaction logic for RoomTableWindow.xaml
    /// </summary>
    public partial class RoomTableWindow : Window
    {
        private int id = 0;

        private RoomController _room_controller;
        private EquipmentController _equipmentController;

        public ObservableCollection<Room> RoomsList { get; set; }

        public RoomTableWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            _room_controller = app.roomController;
            _equipmentController = app.equipmentController;

            // has to go before room loading
            if (File.Exists(GlobalPaths.EquipmentDBPath))
                _equipmentController.LoadEquipment();

            if (File.Exists(GlobalPaths.RoomsDBPath))
                _room_controller.LoadRoom();

            RoomsList = _room_controller.ReadAll();
        }

        private void addRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            _room_controller.CreateRoom(id.ToString() + "d", id, id, false, (RoomTypeEnum)(id%4));
            id++;
        }

        private void editRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            Room r = (Room)roomDataGrid.SelectedItem;
            if (r != null)
            {
                _room_controller.EditRoom(r.Id, r.Equipment, r.Floor, r.RoomNb, true, r.Type);
            }
        }

        private void removeRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            Room r = (Room)roomDataGrid.SelectedItem;
            if(r != null)
            {
                _room_controller.RemoveRoom(r.Id);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _equipmentController.SaveEquipment();
            _room_controller.SaveRoom();
        }

        private void addEquipmentBtn_Click(object sender, RoutedEventArgs e)
        {
            Room r = (Room)roomDataGrid.SelectedItem;
            if(r != null)
            {
                _equipmentController.CreateEquipment("e" + id.ToString(), r.Id, (EquipmentTypeEnum)(id % 10));
                _room_controller.AddEquipment("0d", new Equipment("e" + id.ToString(), r.Id, (EquipmentTypeEnum)(id % 10)));
                id++;
            }
            

        }

        private void removeEquipmentBtn_Click(object sender, RoutedEventArgs e)
        {
            Room r = (Room)roomDataGrid.SelectedItem;
            if(r != null)
            {
                _equipmentController.RemoveEquipment(r.Equipment[0].Id);
                _room_controller.RemoveEquipment(r.Id, r.Equipment[0].Id);
            }

        }
    }
}
