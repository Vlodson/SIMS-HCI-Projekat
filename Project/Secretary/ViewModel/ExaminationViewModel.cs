using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using HospitalMain.Enums;

namespace Secretary.ViewModel
{
    public class ExaminationViewModel : ViewModelBase
    {

        private readonly Examination _examination;

        public String ExamRoomID => _examination.ExamRoomId;
        public String Date => _examination.Date.ToString("f");
        public String ID => _examination.Id;
        public int Duration => _examination.Duration;
        public ExaminationTypeEnum Type => _examination.EType;
        public String PatientID => _examination.PatientId;
        public String DoctorID => _examination.DoctorId;

        public ExaminationViewModel(Examination examination)
        {
            _examination = examination;
        }

    }
}
