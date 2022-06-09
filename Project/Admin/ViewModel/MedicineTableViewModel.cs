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
using Enums;
using HospitalMain.Enums;

using Admin.Views;

namespace Admin.ViewModel
{
    public class MedicineTableViewModel: BindableBase
    {
        public ICommandTemplate<String> NavigationCommand { get; private set; }
        public ICommandTemplate RemoveCommand { get; private set; }
        public ICommandTemplate QueryCommand { get; private set; }
        public ICommandTemplate ExportCommand { get; private set; }

        private MedicineController medicineController;
        private MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

        private String search;
        private ObservableCollection<FriendlyMedicine> medicines;
        private FriendlyMedicine selectedMedicine;

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

        public ObservableCollection<FriendlyMedicine> Medicines
        {
            get { return medicines; }
            set
            {
                if(medicines != value)
                {
                    medicines = value;
                    OnPropertyChanged("Medicines");
                }
            }
        }

        public FriendlyMedicine SelectedMedicine
        {
            get { return selectedMedicine; }
            set
            {
                if(selectedMedicine != value)
                {
                    selectedMedicine = value;
                    OnPropertyChanged("SelectedMedicine");
                    RemoveCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public MedicineTableViewModel()
        {
            NavigationCommand = new ICommandTemplate<String>(OnNavigation);
            RemoveCommand = new ICommandTemplate(OnRemove, CanRemove);
            QueryCommand = new ICommandTemplate(OnQuery);
            ExportCommand = new ICommandTemplate(OnExport, CanExport);

            var app = Application.Current as App;
            medicineController = app.medicineController;

            ObservableCollection<Medicine> medicines = medicineController.ReadAll();

            Medicines = new ObservableCollection<FriendlyMedicine>();
            foreach (Medicine medicine in medicines)
                Medicines.Add(new FriendlyMedicine(medicine));

            Search = "Enter Query";
        }

        public void OnExport()
        {
            MessageBox.Show(mainWindow, "PDF Exported");

            //Create a new PDF document.
            PdfDocument pdfDocument = new PdfDocument();
            PdfPage pdfPage = pdfDocument.Pages.Add();
            pdfDocument.DocumentInformation.Title = "Medicines Table";
            pdfDocument.DocumentInformation.Author = "Hospital administration";
            pdfDocument.DocumentInformation.CreationDate = DateTime.Now;

            //Create a text element
            string text = "Medicines Table";
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
            pdfGrid.Columns.Add(3);

            //Add header.
            pdfGrid.Headers.Add(1);
            PdfGridRow pdfGridHeader = pdfGrid.Headers[0];
            pdfGridHeader.Cells[0].Value = " Name";
            pdfGridHeader.Cells[1].Value = " Type";
            pdfGridHeader.Cells[2].Value = " Status";

            //Add rows.
            foreach (FriendlyMedicine m in Medicines)
            {
                PdfGridRow pdfGridRow = pdfGrid.Rows.Add();
                pdfGridRow.Cells[0].Value = " " + m.Name;
                pdfGridRow.Cells[1].Value = " " + m.Type;
                pdfGridRow.Cells[2].Value = " " + m.Status;
            }

            //Draw the PdfGrid.
            pdfGrid.Draw(pdfPage, PointF.Empty);

            //Save the document.
            pdfDocument.Save(@"../../../../HospitalMain/PDFs/MedicineTable.pdf");

            //Close the document
            pdfDocument.Close(true);
        }

        public bool CanExport()
        {
            return Medicines.Count > 0;
        }

        public void OnRemove()
        {
            medicineController.DeleteMedicine(SelectedMedicine.Id);
            Medicines.Remove(SelectedMedicine);
        }

        public bool CanRemove()
        {
            return selectedMedicine is not null;
        }

        public void OnQuery()
        {
            Medicines.Clear();
            if (String.IsNullOrEmpty(Search))
            {
                ObservableCollection<Medicine> medicines = medicineController.ReadAll();
                foreach (Medicine medicineItem in medicines)
                    Medicines.Add(new FriendlyMedicine(medicineItem));
                return;
            }

            ObservableCollection<Medicine> queriedMedicine = medicineController.QueryMedicine(Search);
            foreach (Medicine queriedMedicineItem in queriedMedicine)
                Medicines.Add(new FriendlyMedicine(queriedMedicineItem));
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
                case "equipment":
                    mainWindow.CurrentView = new EquipmentTableView();
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

    public class FriendlyMedicine
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public MedicineTypeEnum Type { get; set; }
        public StatusEnum Status { get; set; }

        public FriendlyMedicine(Medicine medicine)
        {
            Id = medicine.Id;
            Name = medicine.Name;
            Type = medicine.Type;
            Status = medicine.Status;
        }
    }
}
