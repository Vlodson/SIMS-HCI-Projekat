using Controller;
using Model;
using Repository;
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

namespace Patient.View
{
    /// <summary>
    /// Interaction logic for EditExamination.xaml
    /// </summary>
    public partial class EditExamination : Window
    {
        private DoctorController _doctorController;
        private ExamController _examController;
        private RoomController _roomController;
        private PatientController _patientController;
        //private ExaminationRepo _examinationRepo;

        public EditExamination()
        {
            InitializeComponent();
            App app = Application.Current as App;
            _doctorController = app.DoctorController;
            _examController = app.ExamController;
            _roomController = app.RoomController;
            _patientController = app.PatientController;
            //_examinationRepo = app.ExaminationRepo;

            //ExamsAvailable.ItemsSource = _doctorController.GetFreeGetFreeExaminations(ListExaminations.selected.Doctor);
            //ExamsAvailable.ItemsSource = _doctorController.MoveExaminations(ExaminationsList.selected);
            /*
            List<Examination> listExaminationsWithRooms = new List<Examination>();
            List<Examination> listForDoctor = _doctorController.MoveExaminations(ExaminationsList.selected);
            foreach (Examination exam in listForDoctor)
            {
                int counter = 0;
                foreach (Room room in _roomController.ReadAll())
                {
                    if (room.Type == HospitalMain.Enums.RoomTypeEnum.Patient_Room && room.Occupancy == true)
                    {
                        counter++;
                    }
                }
                if (counter < _roomController.ReadAll().Count)
                {
                    listExaminationsWithRooms.Add(exam);
                }
                //exam.DoctorNameSurname = _doctorController.GetDoctor(doctor.Id).NameSurname;
            }
            ExamsAvailable.ItemsSource = listExaminationsWithRooms;
            */
            ExamsAvailable.ItemsSource = _doctorController.MoveExaminations(ExaminationsList.selected);
            Odeljenje.Content = ExaminationsList.selected.DoctorType;
            Lekar.Content = ExaminationsList.selected.DoctorNameSurname;
            StariTermin.Content = ExaminationsList.selected.Date;
            //AddExamination
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Examination newExamination = (Examination)ExamsAvailable.SelectedItem;
            DateTime newDate = newExamination.Date;
            /*
            Room getRoom = new Room();
            List<Room> patientRooms = new List<Room>();
            foreach (Room room in _roomController.ReadAll())
            {
                if (room.Type == HospitalMain.Enums.RoomTypeEnum.Patient_Room)
                {
                    patientRooms.Add(room);
                }
            }
            foreach (Examination examinationExists in _patientController.GetExamByTime(newDate))
            {
                bool take = false;
                foreach (Room room in patientRooms)
                {
                    if (room.Occupancy == false)
                    {
                        if (examinationExists.ExamRoomId != room.Id)
                        {
                            take = true;
                            getRoom = room;
                        }
                    }
                }
            }
            */
            _examController.PatientEditExamForMoving(ExaminationsList.selected, newDate);
            //_examinationRepo.SaveExamination();
            _examController.SaveExaminationRepo();
            
            
            this.Close();
        }
    }
}
