﻿using System;
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
    /// Interaction logic for ScheduleEquipmentTransferWindow.xaml
    /// </summary>
    public partial class ScheduleEquipmentTransferWindow : Window
    {

        public Room OriginRoom { get; set; }
        public Room DestinationRoom { get; set; }
        public Equipment Equipment { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        
        private EquipmentTransferController _equipmentTransferController;
        private RoomController _roomController;

        public ScheduleEquipmentTransferWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            _equipmentTransferController = app.equipmentTransferController;
            _roomController = app.roomController;

            OriginRoom = _roomController.GetClipboardRoom();
            foreach(Equipment equipment in OriginRoom.Equipment)
            {
                equipmentComboBox.Items.Clear();
                equipmentComboBox.Items.Add(equipment);
                equipmentComboBox.SelectedValuePath = "Id";
                equipmentComboBox.DisplayMemberPath = "Type";
            }
        }

        // wpf is stupid sometimes so some events dont happen because they are made to be custom, but the PreviewX event always happens
        private void destinationTextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            HospitalLayoutSubmenuWindow hospitalLayoutSubmenuWindow = new HospitalLayoutSubmenuWindow();
            hospitalLayoutSubmenuWindow.Hide();
            hospitalLayoutSubmenuWindow.ShowDialog();
            this.Show();
            DestinationRoom = _roomController.GetSelectedRoom();
            if(DestinationRoom != null)
                destinationTextBox.Text = DestinationRoom.RoomNb.ToString();
        }

        private void recordBtn_Click(object sender, RoutedEventArgs e)
        {
            int id = _equipmentTransferController.ReadAll().Max(eq => int.Parse(eq.Id))+1;
            DateOnly start = DateOnly.FromDateTime(startDate.SelectedDate.Value);
            DateOnly end = DateOnly.FromDateTime(endDate.SelectedDate.Value);
            Equipment eq = equipmentComboBox.SelectedItem as Equipment;

            _equipmentTransferController.ScheduleTransfer(id.ToString(), OriginRoom.Id, DestinationRoom.Id, eq.Id, start, end);
            this.Close();
            RecordEquipmentTransferWindow recordEquipmentTransferWindow = new RecordEquipmentTransferWindow();
            recordEquipmentTransferWindow.Show();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            int id = _equipmentTransferController.ReadAll().Max(eq => int.Parse(eq.Id))+1;
            DateOnly start = DateOnly.FromDateTime(startDate.SelectedDate.Value);
            DateOnly end = DateOnly.FromDateTime(endDate.SelectedDate.Value);
            Equipment eq = equipmentComboBox.SelectedItem as Equipment;

            _equipmentTransferController.SetClipboardEquipmentTransfer(new EquipmentTransfer(id.ToString(), OriginRoom, DestinationRoom, Equipment, StartDate, EndDate, ""));
        }

        private void discardBtn_Click(object sender, RoutedEventArgs e)
        {
            _roomController.RemoveSelectedRoom();
            this.Close();
            this.Owner.Show();
        }
    }
}
