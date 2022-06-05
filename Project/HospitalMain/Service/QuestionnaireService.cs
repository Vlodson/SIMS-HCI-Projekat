using HospitalMain.Model;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMain.Service
{
    public class QuestionnaireService
    {
        private QuestionnaireRepo _questionnaireRepo;
        private readonly PatientRepo _patientRepo;


        public QuestionnaireService(QuestionnaireRepo questionnaireRepo, PatientRepo patientRepo)
        {
            _questionnaireRepo = questionnaireRepo;
            _patientRepo = patientRepo;
        }

        public Questionnaire GetHospitalQuestionnaire()
        {
            foreach (Questionnaire questionnaire in _questionnaireRepo.questionnaireList)
            {
                if (questionnaire.IdDoctor.Equals("hospital"))
                {
                    return questionnaire;
                }
            }
            return null;
        }

        public Questionnaire GetDoctorQuestionnaire()
        {
            foreach (Questionnaire questionnaire in _questionnaireRepo.questionnaireList)
            {
                if (!questionnaire.IdDoctor.Equals("hospital"))
                {
                    return questionnaire;
                }
            }
            return null;
        }

        public bool CheckAnswerAvailable(String doctorId, MedicalRecord medicalRecord)
        {
            Answer existing = ContainsAnswer(medicalRecord.ID, doctorId);
            if (existing == null) return true;
            if (existing != null && existing.CounterGrades >= medicalRecord.Reports.Where(report => report.DoctorId.Equals(doctorId)).Count())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Answer ContainsAnswer(String idPatient, String idAnswer)
        {
            foreach (Answer answer in GetPatient(idPatient).Answers)
            {
                if (idAnswer.Equals(answer.IdDoctor))
                {
                    return answer;
                }
            }
            return null;
        }

        public void AddAnswer(String idPatient, Answer answer)
        {
            Answer existing = ContainsAnswer(idPatient, answer.IdDoctor);
            if (existing == null)
            {
                answer.CounterGrades = 1;
            }
            else
            {
                answer.CounterGrades = existing.CounterGrades + 1;
                GetPatient(idPatient).Answers.Remove(existing);
            }
            GetPatient(idPatient).Answers.Add(answer);
            _patientRepo.SavePatient();
        }

        public Patient GetPatient(String id)
        {
            return _patientRepo.GetPatient(id);
        }
    }
}
