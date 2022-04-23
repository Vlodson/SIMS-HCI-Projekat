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
using System.IO;

using Model;
using Utility;
using Controller;
using HospitalMain.Enums;

namespace Admin.View
{
    /// <summary>
    /// Interaction logic for EquipmentTableWindow.xaml
    /// </summary>
    public partial class TableWindow : Window
    {

        private EquipmentController _equipmentController;
        private RoomController _roomController;

        public ObservableCollection<Equipment> EquipmentPerRoomList { get; set; }
        public ObservableCollection<Room> RoomsList { get; set; }

        public TableWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            _equipmentController = app.equipmentController;
            _roomController = app.roomController;
            
            EquipmentPerRoomList = _equipmentController.ReadAll();
            RoomsList = _roomController.ReadAll();
        }

        private void equipmentBtn_Click(object sender, RoutedEventArgs e)
        {
            // since TableGrid is shared, you need to clear it otherwise it will get more and more columns every time you press the buttons
            TableGrid.Columns.Clear();

            // create data grid programmatically
            TableGrid.ItemsSource = EquipmentPerRoomList;

            DataGridTextColumn id_col = new DataGridTextColumn()
            {
                Header = "ID",
                Width = 100,
                Binding = new Binding("Id")
            };


            DataGridTextColumn room_id_col = new DataGridTextColumn()
            {
                Header = "Room ID",
                Width = 100,
                Binding = new Binding("RoomId")
            };

            DataGridTextColumn type_col = new DataGridTextColumn()
            {
                Header = "Type",
                Width = new DataGridLength(1, DataGridLengthUnitType.Star), // for some reason the constructor has value and unit type but value doenst do anything if its star
                Binding = new Binding("Type")
            };

            TableGrid.Columns.Add(id_col);
            TableGrid.Columns.Add(room_id_col);
            TableGrid.Columns.Add(type_col);
        }

        private void roomsBtn_Click(object sender, RoutedEventArgs e)
        {
            TableGrid.Columns.Clear();

            TableGrid.ItemsSource = RoomsList;

            DataGridTextColumn id_col = new DataGridTextColumn()
            {
                Header = "ID",
                Binding = new Binding("Id")
            };

            DataGridTextColumn floor_col = new DataGridTextColumn()
            {
                Header = "Floor",
                Binding = new Binding("Floor")
            };

            DataGridTextColumn roomnb_col = new DataGridTextColumn()
            {
                Header = "RoomNB",
                Binding = new Binding("RoomNb")
            };

            DataGridTextColumn occupancy_col = new DataGridTextColumn()
            {
                Header = "Occupancy",
                Binding = new Binding("Occupancy")
            };

            DataGridTextColumn type_col = new DataGridTextColumn()
            {
                Header = "Type",
                Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                Binding = new Binding("Type")
            };

            TableGrid.Columns.Add(id_col);
            TableGrid.Columns.Add(floor_col);
            TableGrid.Columns.Add(roomnb_col);
            TableGrid.Columns.Add(occupancy_col);
            TableGrid.Columns.Add(type_col);
        }
    }
}
