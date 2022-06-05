using HospitalMain.Model;
using HospitalMain.Service;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMain.Controller
{
    public class QuestionnaireController
    {
        private QuestionnaireService _questionnaireService;

        public QuestionnaireController(QuestionnaireService questionnaireService)
        {
            _questionnaireService = questionnaireService;
        }

        public Questionnaire GetHospitalQuestionnaire()
        {
            return _questionnaireService.GetHospitalQuestionnaire();
        }

        public Questionnaire GetDoctorQuestionnaire()
        {
            return _questionnaireService.GetDoctorQuestionnaire();
        }

        public bool CheckAnswerAvailable(String doctorId, MedicalRecord medicalRecord)
        {
            return _questionnaireService.CheckAnswerAvailable(doctorId, medicalRecord);
        }
        public void AddAnswer(String patientId, Answer answer)
        {
            _questionnaireService.AddAnswer(patientId, answer);
        }
    }
}
