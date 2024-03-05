﻿using System;
using System.Text;
using System.IO;

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

        static string GetDiagnosis(int amountQuestions, int amountRightAnswers)
        {
            int score = (int)((amountRightAnswers / (double)amountQuestions) * 100);
            string[] diagnoses = new string[]
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

        static bool ConfirmQuestion(string name, string message)
        {
            while (true)
            {
                Console.WriteLine($@"{name}, {message} (да\нет)");
                string userAnswer = Console.ReadLine().ToLower();
                switch (userAnswer)
                {
                    case "да": return true;
                    case "нет": return false;
                    default: continue;
                }
            }
        }

        static string GetPathFile()
        {
            string fileName = "data.csv";
            string currentDirectory = Environment.CurrentDirectory;
            string path = $@"{currentDirectory}\{fileName}";
            return path;
        }

        static void SaveResult(string userName, int countRightAnswers, string diagnosis)
        {
            string path = GetPathFile();

            if (!File.Exists(path))
            {
                using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
                {
                    sw.WriteLine("ФИО;Количество правильных ответов;Диагноз;");
                }
            }

            using (StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8))
            {
                sw.WriteLine($"{userName};{countRightAnswers.ToString()};{diagnosis};");
            }
        }

        static string GetPatternLine(char symbol, int amount)
        {
            string line = string.Empty;
            for (int i = 0; i < amount; i++)
            {
                line += symbol.ToString();
            }
            return line;
        }

        static void ShowResults()
        {
            string path = GetPathFile();

            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
                {
                    string[] datas = sr.ReadLine().Split(';');
                    Console.WriteLine($"| {datas[0],-13}| {datas[1],-32}| {datas[2],-13}|");
                    Console.WriteLine($"|{GetPatternLine('-', 14)}|{GetPatternLine('-', 33)}|{GetPatternLine('-', 14)}|");
                    while (sr.Peek() >= 0)
                    {
                        datas = sr.ReadLine().Split(';');
                        Console.WriteLine($"| {datas[0],-13}| {datas[1],-32}| {datas[2],-13}|");
                    }
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

                string diagnosis = GetDiagnosis(amountQuestions, countRightAnswers);
                Console.WriteLine($"Количество правильных ответов: {countRightAnswers}");
                Console.WriteLine($"{userName}, Ваш диагноз: Вы \"{diagnosis}\"!");
                SaveResult(userName, countRightAnswers, diagnosis);

                if (ConfirmQuestion(userName, "хотите посмотреть все результаты?"))
                    ShowResults();

                oneMoreTime = ConfirmQuestion(userName, "хотели бы Вы пройти тест еще раз?");
            } while (oneMoreTime);

            Console.ReadKey();
        }
    }
}
