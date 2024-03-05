using System;

namespace GeniusIdiotConsoleApp
{
    internal class Program
    {
        static string[] GetQuestions()
        {
            string[] questions = new string[]
            {
                "Сколько будет два плюс два умноженное на два?",
                "Бревно нужно распилить на 10 частей. Сколько распилов нужно сделать?",
                "На двух руках 10 пальцев. Сколько пальцев на 5 руках?",
                "Укол делают каждые полчаса. Сколько нужно минут, чтобы сделать три укола?",
                "Пять свечей горело, две потухли. Сколько свечей осталось?"
            };

            return questions;
        }

        static int[] GetAnswers()
        {
            int[] answers = new int[]
            {
                6,
                9,
                25,
                60,
                2
            };

            return answers;
        }

        static string GetDiagnosis(int amountRightAnswers)
        {
            string[] diagnoses = new string[]
            {
                "кретин",
                "идиот",
                "дурак",
                "нормальный",
                "талант",
                "гений"
            };

            return diagnoses[amountRightAnswers];
        }

        static int[] GetOrderOfQuestions(int amountQuestions)
        {
            int[] orderOfQuestions = new int[amountQuestions];

            for (int i = 0; i < orderOfQuestions.Length; i++)
                orderOfQuestions[i] = i;

            Random random = new Random();
            for (int i = orderOfQuestions.Length - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                int temp = orderOfQuestions[j];
                orderOfQuestions[j] = orderOfQuestions[i];
                orderOfQuestions[i] = temp;
            }

            return orderOfQuestions;
        }

        static bool GoOneMoreTime(string userName)
        {
            while (true)
            {
                Console.WriteLine($"{userName}, хотели бы Вы пройти тест еще раз? (да\\нет)");
                string userAnswer = Console.ReadLine().ToLower();
                switch (userAnswer)
                {
                    case "да": return true;
                    case "нет": return false;
                    default: continue;
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать на тестирование!");
            Console.Write("Пожалуйста представьтесь: ");
            string userName = Console.ReadLine();

            if (string.IsNullOrEmpty(userName))
                userName = "NoName";

            string[] questions = GetQuestions();
            int[] answers = GetAnswers();

            if (questions.Length != answers.Length)
                throw new ArgumentException("Количество вопросов не соответствует количеству ответов");

            int amountQuestions = questions.Length;
            bool oneMoreTime = default;
            do
            {
                int countRightAnswers = 0;
                int[] orderOfQuestions = GetOrderOfQuestions(amountQuestions);

                for (int i = 0; i < orderOfQuestions.Length; i++)
                {
                    int index = orderOfQuestions[i];
                    Console.WriteLine($"Вопрос №{i + 1}");
                    Console.WriteLine(questions[index]);
                    int userAnswer = -1;
                    try
                    {
                        userAnswer = Convert.ToInt32(Console.ReadLine());

                    }
                    catch (FormatException)
                    {
                        continue;
                    }

                    int rightAnswer = answers[index];
                    if (userAnswer == rightAnswer)
                        countRightAnswers++;
                }

                Console.WriteLine($"Количество правильных ответов: {countRightAnswers}");
                Console.WriteLine($"{userName}, Ваш диагноз: Вы \"{GetDiagnosis(countRightAnswers)}\"!");
                oneMoreTime = GoOneMoreTime(userName);
            } while (oneMoreTime);

            Console.ReadKey();
        }
    }
}
