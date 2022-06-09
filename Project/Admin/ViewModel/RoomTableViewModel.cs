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
    public class RoomTableViewModel: BindableBase
    {
        public ICommandTemplate<String> NavigationCommand { get; private set; }
        public ICommandTemplate RemoveCommand { get; private set; }
        public ICommandTemplate QueryCommand { get; private set; }
        public ICommandTemplate ExportCommand { get; private set; }

        private RoomController roomController;
        private MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

        private String search;
        private ObservableCollection<FriendlyRoom> rooms;

        public String Search
        {
            get { return search; }
            set
            {
                if(search != value)
                {
                    search = value;
                    OnPropertyChanged("Search");
                }
            }
        }
        public ObservableCollection<FriendlyRoom> Rooms
        {
            get { return rooms; }
            set
            {
                if(rooms != value)
                {
                    rooms = value;
                    OnPropertyChanged("Rooms");
                }
            }
        }

        public RoomTableViewModel()
        {
            NavigationCommand = new ICommandTemplate<String>(OnNavigation);
            RemoveCommand = new ICommandTemplate(OnRemove, CanRemove);
            QueryCommand = new ICommandTemplate(OnQuery);
            ExportCommand = new ICommandTemplate(OnExport, CanExport);

            var app = Application.Current as App;
            roomController = app.roomController;

            ObservableCollection<Room> rooms = roomController.ReadAll();

            Rooms = new ObservableCollection<FriendlyRoom>();
            foreach(Room room in rooms)
                Rooms.Add(new FriendlyRoom(room));

            Search = "Enter Query";
        }

        public void OnExport()
        {
            MessageBox.Show(mainWindow, "PDF Exported");

            //Create a new PDF document.
            PdfDocument pdfDocument = new PdfDocument();
            PdfPage pdfPage = pdfDocument.Pages.Add();
            pdfDocument.DocumentInformation.Title = "Rooms Table";
            pdfDocument.DocumentInformation.Author = "Hospital administration";
            pdfDocument.DocumentInformation.CreationDate = DateTime.Now;

            //Create a text element
            string text = "Rooms Table";
            PdfTextElement element = new PdfTextElement(text);
            element.Brush = new PdfSolidBrush(Color.Black);
            element.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
            element.StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Top);
            //Set the properties to paginate the text
            PdfLayoutFormat layoutFormat = new PdfLayoutFormat();
            layoutFormat.Break = PdfLayoutBreakType.FitPage;
            //Set bounds to draw multiline text
            RectangleF bounds = new RectangleF(PointF.Empty, pdfPage.Graphics.ClientSize);
            //Draw the text element with the properties and formats set
            element.Draw(pdfPage, bounds, layoutFormat);


            //Create a new PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();

            //Add three columns.
            pdfGrid.Columns.Add(4);

            //Add header.
            pdfGrid.Headers.Add(1);
            PdfGridRow pdfGridHeader = pdfGrid.Headers[0];
            pdfGridHeader.Cells[0].Value = " Number";
            pdfGridHeader.Cells[1].Value = " Floor";
            pdfGridHeader.Cells[2].Value = " Occupancy";
            pdfGridHeader.Cells[3].Value = " Type";

            //Add rows.
            foreach (FriendlyRoom r in Rooms)
            {
                PdfGridRow pdfGridRow = pdfGrid.Rows.Add();
                pdfGridRow.Cells[0].Value = " " + r.RoomNb;
                pdfGridRow.Cells[1].Value = " " + r.Floor;
                pdfGridRow.Cells[2].Value = " " + r.Occupancy;
                pdfGridRow.Cells[3].Value = " " + r.Type;
            }

            //Draw the PdfGrid.
            pdfGrid.Draw(pdfPage, PointF.Empty);

            //Save the document.
            pdfDocument.Save(@"../../../../HospitalMain/PDFs/RoomTable.pdf");

            //Close the document
            pdfDocument.Close(true);
        }

        public bool CanExport()
        {
            return Rooms.Count > 0;
        }

        public void OnRemove()
        {
            return;
        }

        public bool CanRemove()
        {
            return false;
        }

        public void OnQuery()
        {
            Rooms.Clear();
            if (String.IsNullOrEmpty(Search))
            {
                ObservableCollection<Room> rooms = roomController.ReadAll();
                foreach (Room roomItem in rooms)
                    Rooms.Add(new FriendlyRoom(roomItem));
                return;
            }

            ObservableCollection<Room> queriedRooms = roomController.QueryRooms(Search);
            foreach (Room room in queriedRooms)
                Rooms.Add(new FriendlyRoom(room));
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
                case "equipment":
                    mainWindow.CurrentView = new EquipmentTableView();
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

    public class FriendlyRoom
    {
        public String Id { get; set; }
        public int Floor { get; set; }
        public int RoomNb { get; set; }
        public bool Occupancy { get; set; }
        public String Type { get; set; }

        public FriendlyRoom(Room room)
        {
            Id = room.Id;
            Floor = room.Floor;
            RoomNb = room.RoomNb;
            Occupancy = room.Occupancy;
            Type = RoomTypeEnumExtensions.ToFriendlyString(room.Type);
        }
    }
}
