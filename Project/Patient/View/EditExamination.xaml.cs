﻿using Controller;
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
        //private ExaminationRepo _examinationRepo;

        public EditExamination()
        {
            InitializeComponent();
            App app = Application.Current as App;
            _doctorController = app.DoctorController;
            _examController = app.ExamController;
            _roomController = app.RoomController;
            //_examinationRepo = app.ExaminationRepo;

            //ExamsAvailable.ItemsSource = _doctorController.GetFreeGetFreeExaminations(ListExaminations.selected.Doctor);
            //ExamsAvailable.ItemsSource = _doctorController.MoveExaminations(ExaminationsList.selected);
            List<Examination> listExaminationsWithRooms = new List<Examination>();
            List<Examination> listForDoctor = _doctorController.MoveExaminations(ExaminationsList.selected);
            foreach (Examination exam in listForDoctor)
            {
                bool add = true;
                foreach (Room room in _roomController.ReadAll())
                {
                    if (room.Type == HospitalMain.Enums.RoomTypeEnum.Patient_Room && room.Occupancy == true)
                    {
                        add = false;
                    }
                }
                if (add == true)
                {
                    listExaminationsWithRooms.Add(exam);
                }
                //exam.DoctorNameSurname = _doctorController.GetDoctor(doctor.Id).NameSurname;
            }
            ExamsAvailable.ItemsSource = listExaminationsWithRooms;
            Odeljenje.Content = ExaminationsList.selected.DoctorType;
            Lekar.Content = ExaminationsList.selected.DoctorNameSurname;
            StariTermin.Content = ExaminationsList.selected.Date;
            //AddExamination
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Examination newExamination = (Examination)ExamsAvailable.SelectedItem;
            DateTime newDate = newExamination.Date;
            _examController.PatientEditExam(ExaminationsList.selected, newDate);
            //_examinationRepo.SaveExamination();
            _examController.SaveExaminationRepo();
            
            
            this.Close();
        }
    }
}
