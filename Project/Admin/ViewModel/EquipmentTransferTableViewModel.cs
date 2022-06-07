using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.ComponentModel;
using System.Drawing;
using Syncfusion.Pdf.Grid;

using Model;
using Controller;
using Utility;
using HospitalMain.Enums;

using Admin.Views;

namespace Admin.ViewModel
{
    public class EquipmentTransferTableViewModel: BindableBase
    {
        public ICommandTemplate<String> NavigationCommand { get; private set; }
        public ICommandTemplate RemoveCommand { get; private set; }
        public ICommandTemplate QueryCommand { get; private set; }
        public ICommandTemplate ExportCommand { get; private set; }

        private EquipmentTransferController equipmentTransferController;
        private MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

        private ObservableCollection<FriendlyEquipmentTransfer> equipmentTransfers;
        public ObservableCollection<FriendlyEquipmentTransfer> EquipmentTransfers
        {
            get { return equipmentTransfers; }
            set
            {
                if(equipmentTransfers != value)
                {
                    equipmentTransfers = value;
                    OnPropertyChanged("EquipmentTransfers");
                }
            }
        }

        private FriendlyEquipmentTransfer selectedEquipment;
        public FriendlyEquipmentTransfer SelectedEquipmentTransfer
        {
            get { return selectedEquipment; }
            set
            {
                if(selectedEquipment != value)
                {
                    selectedEquipment = value;
                    OnPropertyChanged("SelectedEquipmentTransfer");
                    RemoveCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private String search;
        public String Search
        {
            get { return search; }
            set
            {
                if (search != value)
                {
                    search = value;
                    OnPropertyChanged("Search");
                }
            }
        }

        public EquipmentTransferTableViewModel()
        {
            NavigationCommand = new ICommandTemplate<String>(OnNavigation);
            RemoveCommand = new ICommandTemplate(OnRemove, CanRemove);
            QueryCommand = new ICommandTemplate(OnQuery);
            ExportCommand = new ICommandTemplate(OnExport, CanExport);

            var app = Application.Current as App;
            equipmentTransferController = app.equipmentTransferController;

            ObservableCollection<EquipmentTransfer> equipmentTransfers = equipmentTransferController.ReadAll();

            EquipmentTransfers = new ObservableCollection<FriendlyEquipmentTransfer>();
            foreach (EquipmentTransfer equipmentTransfer in equipmentTransfers)
                EquipmentTransfers.Add(new FriendlyEquipmentTransfer(equipmentTransfer));

            Search = "Enter Query";
        }

        public void OnExport()
        {
            MessageBox.Show("PDF Exported");

            //Create a new PDF document.
            PdfDocument pdfDocument = new PdfDocument();
            PdfPage pdfPage = pdfDocument.Pages.Add();

            //Create a new PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();

            //Add three columns.
            pdfGrid.Columns.Add(4);

            //Add header.
            pdfGrid.Headers.Add(1);
            PdfGridRow pdfGridHeader = pdfGrid.Headers[0];
            pdfGridHeader.Cells[0].Value = " Origin";
            pdfGridHeader.Cells[1].Value = " Destination";
            pdfGridHeader.Cells[2].Value = " Equipment";
            pdfGridHeader.Cells[3].Value = " End Date";

            //Add rows.
            foreach (FriendlyEquipmentTransfer e in EquipmentTransfers)
            {
                PdfGridRow pdfGridRow = pdfGrid.Rows.Add();
                pdfGridRow.Cells[0].Value = " " + e.OriginRoom;
                pdfGridRow.Cells[1].Value = " " + e.DestinationRoom;
                pdfGridRow.Cells[2].Value = " " + e.Equipment;
                pdfGridRow.Cells[2].Value = " " + e.EndDate;
            }

            //Draw the PdfGrid.
            pdfGrid.Draw(pdfPage, PointF.Empty);

            //Save the document.
            pdfDocument.Save(@"../../../../HospitalMain/PDFs/EquipmentTransferTable.pdf");

            //Close the document
            pdfDocument.Close(true);
        }

        public bool CanExport()
        {
            return EquipmentTransfers.Count > 0;
        }

        public void OnRemove()
        {
            equipmentTransferController.DeleteEquipmentTransfer(SelectedEquipmentTransfer.Id);
            EquipmentTransfers.Remove(SelectedEquipmentTransfer);
        }
        public bool CanRemove()
        {
            return SelectedEquipmentTransfer is not null;
        }
        public void OnQuery()
        {
            EquipmentTransfers.Clear();
            if (String.IsNullOrEmpty(Search))
            {
                ObservableCollection<EquipmentTransfer> equipmentTransfers = equipmentTransferController.ReadAll();
                foreach (EquipmentTransfer equipmentTransferItem in equipmentTransfers)
                    EquipmentTransfers.Add(new FriendlyEquipmentTransfer(equipmentTransferItem));
                return;
            }

            ObservableCollection<EquipmentTransfer> queriedEquipmentTransfers = equipmentTransferController.QueryEquipmentTransfers(Search);
            foreach (EquipmentTransfer equipmentTransferItem in queriedEquipmentTransfers)
                EquipmentTransfers.Add(new FriendlyEquipmentTransfer(equipmentTransferItem));

        }
        public void OnNavigation(String view)
        {
            switch (view)
            {
                case "back":
                    mainWindow.Width = 750;
                    mainWindow.Height = 430;
                    mainWindow.CurrentView = new MainMenuView();
                    break;
                case "logout":
                    break;
                case "rooms":
                    mainWindow.CurrentView = new RoomTableView();
                    break;
                case "medicine":
                    mainWindow.CurrentView = new MedicineTableView();
                    break;
                case "equipment":
                    mainWindow.CurrentView = new EquipmentTableView();
                    break;
                case "renovations":
                    mainWindow.CurrentView = new RenovationTableView();
                    break;
                case "answers":
                    mainWindow.CurrentView = new RatingsView();
                    break;
                case "help":
                    mainWindow.CurrentView = new QueryHelpView(mainWindow.CurrentView);
                    break;
            }
        }

    }

    public class FriendlyEquipmentTransfer
    {
        public String Id { get; set; }
        public int OriginRoom { get; set; }
        public int DestinationRoom { get; set; }
        public String Equipment { get; set; }
        public DateTime EndDate { get; set; }

        public FriendlyEquipmentTransfer(EquipmentTransfer equipmentTransfer)
        {
            Id = equipmentTransfer.Id;
            OriginRoom = equipmentTransfer.OriginRoom.RoomNb;
            DestinationRoom = equipmentTransfer.DestinationRoom.RoomNb;
            Equipment = EquipmentTypeEnumExtensions.ToFriendlyString(equipmentTransfer.Equipment.Type);
            EndDate = equipmentTransfer.EndDate;
        }
    }
}
