using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeniusIdiotConsoleApp
{
    public class QuestionsStorage
    {
        private Question[] questions;

        public QuestionsStorage()
        {
            questions = new Question[]
            {
                new Question("Сколько будет два плюс два умноженное на два?", 6),
                new Question("Бревно нужно распилить на 10 частей. Сколько распилов нужно сделать?", 9),
                new Question("На двух руках 10 пальцев. Сколько пальцев на 5 руках?", 25),
                new Question("Укол делают каждые полчаса. Сколько нужно минут, чтобы сделать три укола?", 60),
                new Question( "Пять свечей горело, две потухли. Сколько свечей осталось?", 2)
            };
        }

        public Question[] GetQuestions()
        {
            var random = new Random();
            for(int i = questions.Length - 1; i >= 0; i--)
            {
                var j = random.Next(0, i);
                var tempQuestion = questions[j];
                questions[j] = questions[i];
                questions[i] = tempQuestion;
            }
            return questions;
        }
    }
}
