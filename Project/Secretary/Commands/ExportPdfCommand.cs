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
            PdfPage pdfPage = pdfDocument.Pages.Add();

            //Create a new PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();

            //Add three columns.
            pdfGrid.Columns.Add(5);

            //Add header.
            pdfGrid.Headers.Add(1);
            PdfGridRow pdfGridHeader = pdfGrid.Headers[0];
            pdfGridHeader.Cells[0].Value = " Appointment type";
            pdfGridHeader.Cells[1].Value = " Date and time";
            pdfGridHeader.Cells[2].Value = " Room number";
            pdfGridHeader.Cells[3].Value = " Floor";
            pdfGridHeader.Cells[4].Value = " Room type";

            _roomOccupancyReportViewModel.ExamsInWeek = _examController.GetAllExamsInWeek(_roomOccupancyReportViewModel.DateTime);
            _roomOccupancyReportViewModel.MeetingsInWeek = _meetingController.GetAllMeetingsInWeek(_roomOccupancyReportViewModel.DateTime);

            foreach (Examination exam in _roomOccupancyReportViewModel.ExamsInWeek)
            {
                PdfGridRow row = pdfGrid.Rows.Add();
                row.Cells[0].Value = " " + exam.EType;
                row.Cells[1].Value = " " + exam.Date;

                Room room = _roomController.ReadRoom(exam.ExamRoomId);
                row.Cells[2].Value = " " + room.RoomNb;
                row.Cells[3].Value = " " + room.Floor;
                row.Cells[4].Value = " " + room.Type;
            }

            foreach (Meeting meeting in _roomOccupancyReportViewModel.MeetingsInWeek)
            {
                PdfGridRow row = pdfGrid.Rows.Add();
                row.Cells[0].Value = " Meeting";
                row.Cells[1].Value = " " + meeting.DateTime;

                Room room = _roomController.ReadRoom(meeting.RoomID);
                row.Cells[2].Value = " " + room.RoomNb;
                row.Cells[3].Value = " " + room.Floor;
                row.Cells[4].Value = " " + room.Type;
            }

            //Draw the PdfGrid.
            pdfGrid.Draw(pdfPage, PointF.Empty);

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
