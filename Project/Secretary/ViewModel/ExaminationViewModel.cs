﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using HospitalMain.Enums;
using Controller;

namespace Secretary.ViewModel
{
    public class ExaminationViewModel : ViewModelBase
    {

        private readonly Examination _examination;
        private readonly DoctorController _doctorController;

        public String ExamRoomID => _examination.ExamRoomId;
        public String Date => _examination.Date.ToString("f");
        public String ID => _examination.Id;
        public int Duration => _examination.Duration;
        public ExaminationTypeEnum Type => _examination.EType;
        public String PatientID => _examination.PatientId;
        public Doctor Doctor => _doctorController.GetDoctor(_examination.DoctorId);

        public ExaminationViewModel(Examination examination)
        {
            var app = System.Windows.Application.Current as App;
            _doctorController = app.DoctorController;

            _examination = examination;
        }

    }
}
