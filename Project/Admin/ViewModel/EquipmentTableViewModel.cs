using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

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
    public class EquipmentTableViewModel: BindableBase
    {
        public ICommandTemplate<String> NavigationCommand { get; private set; }
        public ICommandTemplate RemoveCommand { get; private set; }
        public ICommandTemplate QueryCommand { get; private set; }
        public ICommandTemplate ExportCommand { get; private set; }

        private EquipmentController equipmentController;
        private MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

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

        private ObservableCollection<FriendlyEquipment> equipment;
        public ObservableCollection<FriendlyEquipment> Equipment
        {
            get { return equipment; }
            set
            {
                equipment = value;
                OnPropertyChanged("Equipment");
            }
        }
        private FriendlyEquipment selectedEquipment;
        public FriendlyEquipment SelectedEquipment
        {
            get { return selectedEquipment; }
            set
            {
                if(selectedEquipment != value)
                {
                    selectedEquipment = value;
                    OnPropertyChanged("SelectedEquipment");
                    RemoveCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public EquipmentTableViewModel()
        {
            NavigationCommand = new ICommandTemplate<String>(OnNavigation);
            RemoveCommand = new ICommandTemplate(OnRemove, CanRemove);
            QueryCommand = new ICommandTemplate(OnQuery);
            ExportCommand = new ICommandTemplate(OnExport, CanExport);

            var app = Application.Current as App;
            equipmentController = app.equipmentController;

            ObservableCollection<Equipment> equipment = equipmentController.ReadAll();

            // there has to be a better way lol
            Equipment = new ObservableCollection<FriendlyEquipment>();
            foreach(Equipment equipmentItem in equipment)
                Equipment.Add(new FriendlyEquipment(equipmentItem));

            Search = "Enter Query";
        }

        public void OnExport()
        {
            MessageBox.Show(mainWindow, "PDF Exported");

            //Create a new PDF document.
            PdfDocument pdfDocument = new PdfDocument();
            PdfPage pdfPage = pdfDocument.Pages.Add();

            //Create a new PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();

            //Add three columns.
            pdfGrid.Columns.Add(3);

            //Add header.
            pdfGrid.Headers.Add(1);
            PdfGridRow pdfGridHeader = pdfGrid.Headers[0];
            pdfGridHeader.Cells[0].Value = " ID";
            pdfGridHeader.Cells[1].Value = " Room Number";
            pdfGridHeader.Cells[2].Value = " Type";

            //Add rows.
            foreach (FriendlyEquipment e in Equipment)
            {
                PdfGridRow pdfGridRow = pdfGrid.Rows.Add();
                pdfGridRow.Cells[0].Value = " " + e.Id;
                pdfGridRow.Cells[1].Value = " " + e.RoomNb;
                pdfGridRow.Cells[2].Value = " " + e.Type;
            }

            //Draw the PdfGrid.
            pdfGrid.Draw(pdfPage, PointF.Empty);

            //Save the document.
            pdfDocument.Save(@"../../../../HospitalMain/PDFs/EquipmentTable.pdf");

            //Close the document
            pdfDocument.Close(true);
        }

        public bool CanExport()
        {
            return Equipment.Count > 0;
        }

        public void OnRemove()
        {
            equipmentController.RemoveEquipment(SelectedEquipment.Id, SelectedEquipment.RoomId);
            Equipment.Remove(SelectedEquipment);
        }

        public bool CanRemove()
        {
            return SelectedEquipment is not null;
        }

        public void OnQuery()
        {
            Equipment.Clear();
            if (String.IsNullOrEmpty(Search))
            {
                ObservableCollection<Equipment> equipment = equipmentController.ReadAll();
                foreach (Equipment equipmentItem in equipment)
                    Equipment.Add(new FriendlyEquipment(equipmentItem));
                return;
            }

            ObservableCollection<Equipment> queriedEquipment = equipmentController.QueryEquipment(Search);
            foreach(Equipment queriedItem in queriedEquipment)
                Equipment.Add(new FriendlyEquipment(queriedItem));
            
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
                case "transfers":
                    mainWindow.CurrentView = new EquipmentTransferTableView();
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

    public class FriendlyEquipment
    {
        private RoomController roomController;

        public String Id { get; set; }
        public String RoomId { get; set; }
        public String RoomNb { get; set; }
        public String Type { get; set; }

        public FriendlyEquipment(Equipment equipment)
        {
            var app = Application.Current as App;
            roomController = app.roomController;

            Id = equipment.Id;
            RoomId = equipment.RoomId;
            RoomNb =  roomController.ReadRoom(equipment.RoomId).RoomNb.ToString();
            Type = EquipmentTypeEnumExtensions.ToFriendlyString(equipment.Type);
        }

        public override string ToString()
        {
            return Type;
        }
    }
}
