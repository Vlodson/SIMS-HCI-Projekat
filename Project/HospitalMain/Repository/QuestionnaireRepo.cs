using HospitalMain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HospitalMain.Repository
{
    public class QuestionnaireRepo
    {
        public String dbPath { get; set; }
        public ObservableCollection<Questionnaire> questionnaireList { get; set; }


        public QuestionnaireRepo(string dbPath)
        {
            this.dbPath = dbPath;
            this.questionnaireList = new ObservableCollection<Questionnaire>();

            //List<String> questions1 = new List<String>();  

            //questions1.Add("Stručnost osoblja");
            //questions1.Add("Dostupnost termina");
            //questions1.Add("Pristup bolnici");
            //questions1.Add("Organizacija prostorija");
            //questions1.Add("Opremljenost");

            //Questionnaire questionnaire1 = new Questionnaire("hospital", questions1);

            //questions1 = new List<String>();
            //questions1.Add("Ljubaznost");
            //questions1.Add("Brzina pregleda");
            //questions1.Add("Dostupnost kontrole");
            //questions1.Add("Jasno definisana terapija");

            //Questionnaire questionnaire2 = new Questionnaire("d1", questions1);
            //Questionnaire questionnaire3 = new Questionnaire("d11", questions1);

            //questionnaireList.Add(questionnaire1);
            //questionnaireList.Add(questionnaire2);
            //questionnaireList.Add(questionnaire3);

            if (File.Exists(dbPath))
                LoadQuestionnaire();

        }
        public bool LoadQuestionnaire()
        {

            using FileStream stream = File.OpenRead(dbPath);
            questionnaireList = JsonSerializer.Deserialize<ObservableCollection<Questionnaire>>(stream);

            return true;
        }

        public bool SaveQuestionnaire()
        {
            string jsonString = JsonSerializer.Serialize(questionnaireList);

            File.WriteAllText(dbPath, jsonString);
            return true;
        }

        public Questionnaire GetHospitalQuestionnaire()
        {
            foreach(Questionnaire questionnaire in questionnaireList)
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
            foreach (Questionnaire questionnaire in questionnaireList)
            {
                if (!questionnaire.IdDoctor.Equals("hospital"))
                {
                    return questionnaire;
                }
            }
            return null;
        }
    }
}
