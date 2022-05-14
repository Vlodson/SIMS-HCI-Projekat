using System;
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
using HospitalMain.Enums;

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
            Title.TextWrapping = TextWrapping.Wrap;
            Title.Text = "Schedule transfer for room\n" + OriginRoom.RoomNb;

            // mental gymnastics to get the combo box to sho friendly looking strings from enums :)
            // im going to create an anonymous class with fields text and value for each combo box item so i bind the paths to that
            equipmentComboBox.Items.Clear();
            equipmentComboBox.DisplayMemberPath = "Text";
            equipmentComboBox.SelectedValuePath = "Value";
            foreach (Equipment equipment in OriginRoom.Equipment)
            {
                // here for each item i create the anon class with the fields i defined above to ensure porper binding
                // this now means that when i reference the combo box to get the item i want ill need to do combobox.SelectedValue since the Item itself is useless/nothing
                equipmentComboBox.Items.Add(new { Text = EquipmentTypeEnumExtensions.ToFriendlyString(equipment.Type), Value = equipment });
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
            {
                if (DestinationRoom.Id.Equals(OriginRoom.Id))
                {
                    DestinationRoom = null;
                    _roomController.RemoveSelectedRoom();
                    destinationTextBox.Text = "Same room selected";
                    return;
                }

                destinationTextBox.Text = DestinationRoom.RoomNb.ToString();
            }
        }
        
        private void Execute_Record(object sender, ExecutedRoutedEventArgs e)
        {
            int id = 0;
            if (_equipmentTransferController.ReadAll().Count > 0)
                id = _equipmentTransferController.ReadAll().Max(eq => int.Parse(eq.Id)) + 1;
            DateOnly start = DateOnly.FromDateTime(startDate.SelectedDate.Value);
            DateOnly end = DateOnly.FromDateTime(endDate.SelectedDate.Value);
            Equipment equipment = equipmentComboBox.SelectedValue as Equipment;

            EquipmentTransfer equipmentTransfer = new EquipmentTransfer(id.ToString(), OriginRoom, DestinationRoom, equipment, start, end);
            _equipmentTransferController.ScheduleTransfer(equipmentTransfer);
            this.Close();
            RecordEquipmentTransferWindow recordEquipmentTransferWindow = new RecordEquipmentTransferWindow();
            recordEquipmentTransferWindow.Owner = App.Current.MainWindow;
            recordEquipmentTransferWindow.Show();
        }

        private void CanExecute_Record(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !(equipmentComboBox.SelectedItem is null || DestinationRoom is null || startDate.SelectedDate is null || endDate.SelectedDate is null || endDate.SelectedDate < startDate.SelectedDate);
        }
        private void Execute_Save(object sender, ExecutedRoutedEventArgs e)
        {
            int id = 0;
            if (_equipmentTransferController.ReadAll().Count > 0)
                id = _equipmentTransferController.ReadAll().Max(eq => int.Parse(eq.Id)) + 1;
            DateOnly start = DateOnly.FromDateTime(startDate.SelectedDate.Value);
            DateOnly end = DateOnly.FromDateTime(endDate.SelectedDate.Value);
            Equipment equipment = equipmentComboBox.SelectedItem as Equipment;

            _equipmentTransferController.SetClipboardEquipmentTransfer(new EquipmentTransfer(id.ToString(), OriginRoom, DestinationRoom, equipment, start, end));
        }

        private void CanExecute_Save(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !(equipmentComboBox.SelectedItem is null || DestinationRoom is null || startDate.SelectedDate is null || endDate.SelectedDate is null || endDate.SelectedDate < startDate.SelectedDate);
        }

        private void discardBtn_Click(object sender, RoutedEventArgs e)
        {
            _roomController.RemoveSelectedRoom();
            this.Close();
            this.Owner.Show();
        }
    }
}
