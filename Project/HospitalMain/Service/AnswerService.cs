using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using Service;
using Repository;
using HospitalMain.Model;

namespace Service
{
    public class AnswerService
    {
        private PatientService patientService;
        private DoctorService doctorService;

        public AnswerService(PatientService patientService, DoctorService doctorService)
        {
            this.patientService = patientService;
            this.doctorService = doctorService;
        }

        private ObservableCollection<Answer> GetAnswers()
        {
            ObservableCollection<Answer> answers = new ObservableCollection<Answer>();
            ObservableCollection<Patient> patients = patientService.GetPatients();

            foreach (Patient p in patients)
                foreach (Answer a in p.Answers)
                    answers.Add(a);

            return answers;
        }

        public static double AverageRating(Answer answer)
        {
            return answer.Grades.Average();
        }

        public Dictionary<Doctor, Answer> DoctorRatings()
        {
            Dictionary<Doctor, Answer> ratings = new Dictionary<Doctor, Answer>();
            ObservableCollection<Answer> answers = GetAnswers();

            foreach (Answer a in answers)
                ratings.Add(doctorService.GetDoctor(a.IdDoctor), a);

            return ratings;
        }
    }
}
