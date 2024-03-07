using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeniusIdiotConsoleApp
{
    public class User
    {
        public string Name { get; }
        private QuestionsStorage qs;
        public Question[] Questions { get; private set; }
        public int RightAnswers { get; private set; }
        public string Diagnosis
        {
            get
            {
                int score = (int)((RightAnswers / (double)Questions.Length) * 100);
                var diagnoses = new string[]
                {
                    "кретин",
                    "идиот",
                    "дурак",
                    "нормальный",
                    "талант",
                    "гений"
                };

                int index = 0;
                if (score == 0)
                {
                    index = 0;
                }
                else if (score > 0 && score <= 20)
                {
                    index = 1;
                }
                else if (score > 20 && score <= 40)
                {
                    index = 2;
                }
                else if (score > 40 && score <= 60)
                {
                    index = 3;
                }
                else if (score > 60 && score <= 80)
                {
                    index = 4;
                }
                else if (score > 80 && score <= 100)
                {
                    index = 5;
                }

                return diagnoses[index];
            }
        }

        public User(string? name)
        {
            qs = new QuestionsStorage();
            Name = string.IsNullOrEmpty(name) ? "NoName" : name;
            Questions = qs.GetQuestions();
            RightAnswers = 0;
        }

        public void GetQuestions()
        {
            Questions = qs.GetQuestions();
            RightAnswers = 0;
        }

        public void GiveAnswer(int i, int userAnswer)
        {
            if (Questions[i].Answer == userAnswer)
                RightAnswers++;
        }
    }
}
