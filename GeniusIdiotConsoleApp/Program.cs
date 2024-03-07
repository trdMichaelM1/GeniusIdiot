using System;

namespace GeniusIdiotConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать на тестирование!");
            Console.Write("Пожалуйста представьтесь: ");
            var userName = Console.ReadLine();
            var user = new User(userName);
            var storage = new UsersResultStorage(user);

            do
            {
                for (int i = 0; i < user.Questions.Length; i++)
                {
                    Console.WriteLine($"Вопрос №{i + 1}");
                    Console.WriteLine(user.Questions[i].Qu);
                    try
                    {
                        int userAnswer = Convert.ToInt32(Console.ReadLine());
                        user.GiveAnswer(i, userAnswer);

                    }
                    catch (FormatException)
                    {
                        continue;
                    }
                }

                Console.WriteLine($"Количество правильных ответов: {user.RightAnswers}");
                Console.WriteLine($"{user.Name}, Ваш диагноз: Вы \"{user.Diagnosis}\"!");
                storage.Save();

                if (ConfirmQuestion(user.Name, "хотите посмотреть все результаты?"))
                    storage.Show();

                if (ConfirmQuestion(user.Name, "хотели бы Вы пройти тест еще раз?"))
                {
                    user.GetQuestions();
                    continue;
                }
                break;
            } while (true);

            Console.ReadKey();
        }

        static bool ConfirmQuestion(string name, string message)
        {
            while (true)
            {
                Console.WriteLine($@"{name}, {message} (да\нет)");
                var userAnswer = Console.ReadLine().ToLower();
                switch (userAnswer)
                {
                    case "да": return true;
                    case "нет": return false;
                    default: continue;
                }
            }
        }
    }
}
