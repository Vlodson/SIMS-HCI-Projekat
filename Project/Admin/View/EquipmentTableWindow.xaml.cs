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
    public partial class EquipmentTableWindow : Window
    {

        private EquipmentController _equipmentController;
        
        // the name will likely change to tableview and for each button press it will do (1)
        public ObservableCollection<Equipment> EquipmentPerRoomList { get; set; }

        public EquipmentTableWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            _equipmentController = app.equipmentController;

            if (File.Exists(GlobalPaths.EquipmentDBPath))
                _equipmentController.LoadEquipment();

            // this is (1), since it doesnt do crud stuff on it directly just change reference on view button row press
            // and add new or something like that
            EquipmentPerRoomList = _equipmentController.ReadAll();
            
        }
    }
}
