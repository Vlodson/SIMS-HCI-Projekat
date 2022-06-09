using Controller;
using HospitalMain.Controller;
using Secretary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using System.Drawing;
using Model;
using HospitalMain.Model;
using System.ComponentModel;
using Syncfusion.Pdf.Graphics;

namespace Secretary.Commands
{
    public class ExportPdfCommand : CommandBase
    {
        private MeetingController _meetingController;
        private ExamController _examController;
        private RoomController _roomController;
        private RoomOccupancyReportViewModel _roomOccupancyReportViewModel;
        private MainViewModel _mainViewModel;

        public ExportPdfCommand(RoomOccupancyReportViewModel roomOccupancyReportViewModel, MeetingController meetingController, ExamController examController, RoomController roomController, MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _roomOccupancyReportViewModel = roomOccupancyReportViewModel;
            _meetingController = meetingController;
            _examController = examController;
            _roomController = roomController;

            _roomOccupancyReportViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object? parameter)
        {

            //Create a new PDF document.
            PdfDocument pdfDocument = new PdfDocument();
            pdfDocument.PageSettings.Margins.All = 30;

            PdfPage pdfPage = pdfDocument.Pages.Add();

            RectangleF bounds = new RectangleF(0, 0, pdfDocument.Pages[0].GetClientSize().Width, 200);

            PdfPageTemplateElement header = new PdfPageTemplateElement(bounds);

            PdfImage image = new PdfBitmap(@"../../../../Secretary/Images/logo.png");

            //Set the standard font

            PdfTrueTypeFont font = new PdfTrueTypeFont(new Font("TimesRoman", 24), true);
            PdfTrueTypeFont font1 = new PdfTrueTypeFont(new Font("TimesRoman", 12), true);
            //Draw the text
            header.Graphics.DrawString("Nedeljni izveštaj zauzetosti prostorija bolnice\n", font, PdfBrushes.Black, new PointF(20,140));
            header.Graphics.DrawString("Zdravstvena ustanova 'Medical' \n" +
                "Karađorđeva 4\n" +
                "21000 Novi Sad\n" +
                "Telefon: 021/487-212\n", font1, PdfBrushes.Black, new PointF(0, 20));

            //Draw the image in the header.

            header.Graphics.DrawImage(image, new PointF(430, 0), new SizeF(120, 120));

            //Add the header at the top.

            pdfDocument.Template.Top = header;

            //Create a new PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();

            //Add three columns.
            pdfGrid.Columns.Add(5);

            //Add header.
            pdfGrid.Headers.Add(1);
            PdfGridRow pdfGridHeader = pdfGrid.Headers[0];
            pdfGridHeader.Cells[0].Value = " Tip pregleda";
            pdfGridHeader.Cells[1].Value = " Datum i vreme";
            pdfGridHeader.Cells[2].Value = " Broj prostorije";
            pdfGridHeader.Cells[3].Value = " Sprat";
            pdfGridHeader.Cells[4].Value = " Tip prostorije";

            _roomOccupancyReportViewModel.ExamsInWeek = _examController.GetAllExamsInWeek(_roomOccupancyReportViewModel.DateTime);
            _roomOccupancyReportViewModel.MeetingsInWeek = _meetingController.GetAllMeetingsInWeek(_roomOccupancyReportViewModel.DateTime);

            //stilizovanje celija
            PdfGridCellStyle cellStyle = new PdfGridCellStyle();
            cellStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 10f);

            //stilizovanje naslova tabele
            PdfGridCellStyle headerStyle = new PdfGridCellStyle();
            headerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor());
            headerStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f, PdfFontStyle.Regular);

            for(int i = 0; i < pdfGridHeader.Cells.Count; i++)
            {
                pdfGridHeader.Cells [i].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
            }

            pdfGridHeader.ApplyStyle(headerStyle);

            foreach (Examination exam in _roomOccupancyReportViewModel.ExamsInWeek)
            {
                PdfGridRow row = pdfGrid.Rows.Add();
                row.ApplyStyle(cellStyle);
                if(exam.EType == HospitalMain.Enums.ExaminationTypeEnum.OrdinaryExamination)
                {
                    row.Cells[0].Value = " Standardan pregled";
                } else
                {
                    row.Cells[0].Value = " Operacija";
                }
                row.Cells[1].Value = " " + exam.Date;

                Room room = _roomController.ReadRoom(exam.ExamRoomId);
                row.Cells[2].Value = " " + room.RoomNb;
                row.Cells[3].Value = " " + room.Floor;
                if(room.Type == HospitalMain.Enums.RoomTypeEnum.Operation_Room)
                {
                    row.Cells[4].Value = " Soba za operacije";
                } else if (room.Type == HospitalMain.Enums.RoomTypeEnum.Patient_Room)
                {
                    row.Cells[4].Value = " Soba za preglede";
                }

            }

            foreach (Meeting meeting in _roomOccupancyReportViewModel.MeetingsInWeek)
            {
                PdfGridRow row = pdfGrid.Rows.Add();
                row.ApplyStyle(cellStyle);
                row.Cells[0].Value = " Sastanak";
                row.Cells[1].Value = " " + meeting.DateTime;

                Room room = _roomController.ReadRoom(meeting.RoomID);
                row.Cells[2].Value = " " + room.RoomNb;
                row.Cells[3].Value = " " + room.Floor;
                row.Cells[4].Value = " Soba za sastanke";
            }

            //Draw the PdfGrid.
            pdfGrid.Draw(pdfPage, PointF.Empty);

            //Draw the fouter text. (Date and time)
            RectangleF bounds1 = new RectangleF(0, 200, pdfDocument.Pages[0].GetClientSize().Width, 50);

            PdfPageTemplateElement fouter = new PdfPageTemplateElement(bounds1);

            fouter.Graphics.DrawString("Datum i vreme: " + _roomOccupancyReportViewModel.DateTime.ToString("dd.MM.yyyy HH:mm"), font1, PdfBrushes.Black, new PointF(0,0));
            pdfDocument.Template.Bottom = fouter;

            //Save the document.
            pdfDocument.Save(@"../../../../HospitalMain/PDFs/WeeklyOccupancyRoomReport.pdf");

            //Close the document
            pdfDocument.Close(true);

            if(parameter.ToString() == "ExportPDF")
            {
                _mainViewModel.CurrentViewModel = new HomeViewModel();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            bool flag = false;
            if(_roomOccupancyReportViewModel.DateTime < DateTime.Now)
            {
                flag = false;
            } else
            {
                flag = true;
            }
            return flag && base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(RoomOccupancyReportViewModel.DateTime))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
