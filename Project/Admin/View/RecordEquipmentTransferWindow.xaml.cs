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

namespace Admin.View
{
    /// <summary>
    /// Interaction logic for RecordEquipmentTransferWindow.xaml
    /// </summary>
    public partial class RecordEquipmentTransferWindow : Window
    {
        public EquipmentTransfer equipmentTransfer { get; set; }
        
        private EquipmentTransferController _equipmentTransferController;
        
        public RecordEquipmentTransferWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            _equipmentTransferController = app.equipmentTransferController;

            // last one in list is always the one that needs to be worked here
            equipmentTransfer = _equipmentTransferController.ReadAll().Last();

            equipmentTextBox.Text = equipmentTransfer.Equipment.Type.ToString();
            destinationTextBox.Text = equipmentTransfer.DestinationRoom.Id;
            startDate.Text = equipmentTransfer.StartDate.ToString();
            endDate.Text = equipmentTransfer.EndDate.ToString();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            _equipmentTransferController.RecordTransfer(equipmentTransfer.Id, signatrueTextBox.Text);
            this.Close();
            this.Owner.Show();
        }

        private void discardBtn_Click(object sender, RoutedEventArgs e)
        {
            _equipmentTransferController.DeleteEquipmentTransfer(equipmentTransfer.Id);
            this.Close();
            this.Owner.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _equipmentTransferController.DeleteEquipmentTransfer(equipmentTransfer.Id);
            this.Close();
            this.Owner.Show();
        }
    }
}
