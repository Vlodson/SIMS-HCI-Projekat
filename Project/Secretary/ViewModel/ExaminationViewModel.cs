using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using HospitalMain.Enums;
using Controller;
using System.Windows.Media;

namespace Secretary.ViewModel
{
    public class ExaminationViewModel : ViewModelBase
    {

        private readonly Examination _examination;
        private readonly DoctorController _doctorController;

        public String ExamRoomID => _examination.ExamRoomId;
        public DateTime StartDate => _examination.Date;
        public String ID => _examination.Id;
        public int Duration => _examination.Duration;
        public ExaminationTypeEnum Type => _examination.EType;
        public String PatientID => _examination.PatientId;
        public Doctor Doctor => _doctorController.GetDoctor(_examination.DoctorId);
        public DoctorType DoctorType => _doctorController.GetDoctorsType(_examination.DoctorId);

        public ExaminationViewModel(Examination examination)
        {
            _examination = examination;
        }

    }
}
