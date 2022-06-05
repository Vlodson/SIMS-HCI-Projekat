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

namespace Controller
{
    public class AnswerController
    {
        private AnswerService answerService;

        public AnswerController(AnswerService answerService)
        {
            this.answerService = answerService;
        }

        public static double AverageRating(Answer answer)
        {
            return AnswerService.AverageRating(answer);
        }

        public Dictionary<Doctor, Answer> DoctorRatings()
        {
            return answerService.DoctorRatings();
        }
    }
}
