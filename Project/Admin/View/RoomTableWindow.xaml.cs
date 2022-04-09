﻿using System;
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

using Model;
using Controller;

namespace Admin.View
{
    /// <summary>
    /// Interaction logic for RoomTableWindow.xaml
    /// </summary>
    public partial class RoomTableWindow : Window
    {
        private int id = 0;

        private RoomController _room_controller;

        public ObservableCollection<Room> RoomList { get; set; }

        public RoomTableWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            _room_controller = app.roomController;

            _room_controller.CreateRoom("1a", 1, 1, false, "tip1");
            _room_controller.CreateRoom("1b", 2, 2, false, "tip2");
            _room_controller.CreateRoom("1c", 3, 3, false, "tip3");

            RoomList = new ObservableCollection<Room>(_room_controller.ReadAll());
        }

        private void convertEntityToView()
        {
            // stupid slow but i dont really care right now also probably needs a check to see if its null
            RoomList.Clear();
            List<Room> rooms = this._room_controller.ReadAll();
            foreach (Room room in rooms)
                RoomList.Add(room);
        }

        private void addRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            _room_controller.CreateRoom(id.ToString() + "d", id, id, false, "tip" + id.ToString());
            id++;
            convertEntityToView();
        }

        private void editRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            Room r = (Room)roomDataGrid.SelectedItem;
            if (r != null)
            {
                _room_controller.EditRoom(r.Id, r.Equipment, r.Floor, r.RoomNb, true, r.Type);
                convertEntityToView();
            }
        }

        private void removeRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            Room r = (Room)roomDataGrid.SelectedItem;
            if(r != null)
            {
                _room_controller.RemoveRoom(r.Id);
                convertEntityToView();
            }
        }
    }
}