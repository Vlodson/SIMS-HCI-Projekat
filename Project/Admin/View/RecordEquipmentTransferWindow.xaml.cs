using System;
using System.Collections.Generic;
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
using HospitalMain.Enums;

namespace Admin.View
{
    /// <summary>
    /// Interaction logic for RecordEquipmentTransferWindow.xaml
    /// </summary>
    public partial class RecordEquipmentTransferWindow : Window
    {
        public EquipmentTransfer equipmentTransfer { get; set; }
        
        private EquipmentTransferController _equipmentTransferController;
        private RoomController _roomController;

        public RecordEquipmentTransferWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            _equipmentTransferController = app.equipmentTransferController;
            _roomController = app.roomController;

            Room OriginRoom = _roomController.GetClipboardRoom();
            Title.TextWrapping = TextWrapping.Wrap;
            Title.Text = "Record trasnfer for room\n" + OriginRoom.RoomNb;

            // last one in list is always the one that needs to be worked here
            equipmentTransfer = _equipmentTransferController.ReadAll().Last();

            equipmentTextBox.Text = EquipmentTypeEnumExtensions.ToFriendlyString(equipmentTransfer.Equipment.Type);
            destinationTextBox.Text = equipmentTransfer.DestinationRoom.Id;
            startDate.Text = equipmentTransfer.StartDate.ToString();
            endDate.Text = equipmentTransfer.EndDate.ToString();
        }

        private void Execute_Save(object sender, ExecutedRoutedEventArgs e)
        {
            _equipmentTransferController.RecordTransfer(equipmentTransfer.Id);
            this.Close();
            this.Owner.Show();
        }

        private void CanExecute_Save(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void discardBtn_Click(object sender, RoutedEventArgs e)
        {
            _equipmentTransferController.DeleteEquipmentTransfer(equipmentTransfer.Id);
            this.Close();
            this.Owner.Show();
        }
    }
}
