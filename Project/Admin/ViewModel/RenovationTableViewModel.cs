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
    public class RenovationTableViewModel: BindableBase
    {
        public ICommandTemplate<String> NavigationCommand { get; private set; }
        public ICommandTemplate RemoveCommand { get; private set; }
        public ICommandTemplate QueryCommand { get; private set; }
        public ICommandTemplate ExportCommand { get; private set; }

        private MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
        private RenovationController renovationController;

        private String search;
        private ObservableCollection<FriendlyRenovation> renovations;
        private FriendlyRenovation selectedRenovation;

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

        public ObservableCollection<FriendlyRenovation> Renovations
        {
            get { return renovations; }
            set
            {
                if(renovations != value)
                {
                    renovations = value;
                    OnPropertyChanged("Renovations");
                }
            }
        }
        public FriendlyRenovation SelectedRenovation
        {
            get { return selectedRenovation; }
            set
            {
                if (selectedRenovation != value)
                {
                    selectedRenovation = value;
                    OnPropertyChanged("SelectedRenovation");
                    RemoveCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public RenovationTableViewModel()
        {
            NavigationCommand = new ICommandTemplate<String>(OnNavigation);
            RemoveCommand = new ICommandTemplate(OnRemove, CanRemove);
            QueryCommand = new ICommandTemplate(OnQuery);
            ExportCommand = new ICommandTemplate(OnExport, CanExport);

            var app = Application.Current as App;
            renovationController = app.renovationController;

            ObservableCollection<Renovation> renovations = renovationController.ReadAll();

            Renovations = new ObservableCollection<FriendlyRenovation>();
            foreach (Renovation renovation in renovations)
                Renovations.Add(new FriendlyRenovation(renovation));

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
            pdfGridHeader.Cells[0].Value = " Origin ROom";
            pdfGridHeader.Cells[1].Value = " Type";
            pdfGridHeader.Cells[2].Value = " End Date";

            //Add rows.
            foreach (FriendlyRenovation r in Renovations)
            {
                PdfGridRow pdfGridRow = pdfGrid.Rows.Add();
                pdfGridRow.Cells[0].Value = " " + r.OriginRoom;
                pdfGridRow.Cells[1].Value = " " + r.Type;
                pdfGridRow.Cells[2].Value = " " + r.EndDate;
            }

            //Draw the PdfGrid.
            pdfGrid.Draw(pdfPage, PointF.Empty);

            //Save the document.
            pdfDocument.Save(@"../../../../HospitalMain/PDFs/RenovationTable.pdf");

            //Close the document
            pdfDocument.Close(true);
        }

        public bool CanExport()
        {
            return Renovations.Count > 0;
        }

        public void OnRemove()
        {
            renovationController.DeleteRenovation(SelectedRenovation.Id);
            Renovations.Remove(SelectedRenovation);
        }

        public bool CanRemove()
        {
            return SelectedRenovation is not null;
        }

        public void OnQuery()
        {
            Renovations.Clear();
            if (String.IsNullOrEmpty(Search))
            {
                ObservableCollection<Renovation> renovations = renovationController.ReadAll();
                foreach (Renovation renovationItem in renovations)
                    Renovations.Add(new FriendlyRenovation(renovationItem));
                return;
            }

            ObservableCollection<Renovation> queriedRenovations = renovationController.QueryRenovations(Search);
            foreach (Renovation queriedItem in queriedRenovations)
                Renovations.Add(new FriendlyRenovation(queriedItem));
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
                case "equipment":
                    mainWindow.CurrentView = new EquipmentTableView();
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

    public class FriendlyRenovation
    {
        public String Id { get; set; }
        public int OriginRoom { get; set; }
        public String Type { get; set; }
        public String EndDate { get; set; }

        public FriendlyRenovation(Renovation renovation)
        {
            Id = renovation.Id;
            OriginRoom = renovation.OriginRoom.RoomNb;
            Type = renovation.Type.ToString();
            EndDate = renovation.EndDate.ToString();
        }
    }
}
