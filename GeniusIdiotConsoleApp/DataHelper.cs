using System;
using System.IO;
using System.Text;

namespace GeniusIdiotConsoleApp
{
    public static class DataHelper
    {
        private static readonly string fileName = "data.csv";
        private static readonly string path;

        static DataHelper()
        {
            path = $@"{Environment.CurrentDirectory}\{fileName}";
        }

        public static void SaveResult(string userName, int countRightAnswers, string diagnosis)
        {
            if (!File.Exists(path))
            {
                using (var sw = new StreamWriter(path, false, Encoding.UTF8))
                {
                    sw.WriteLine("ФИО;Количество правильных ответов;Диагноз;");
                }
            }

            using (var sw = new StreamWriter(path, true, Encoding.UTF8))
            {
                sw.WriteLine($"{userName};{countRightAnswers.ToString()};{diagnosis};");
            }
        }

        private static string GetPatternLine(char symbol, int amount)
        {
            var line = string.Empty;
            for (int i = 0; i < amount; i++)
            {
                line += symbol.ToString();
            }
            return line;
        }

        public static void ShowResults()
        {
            if (File.Exists(path))
            {
                using (var sr = new StreamReader(path, Encoding.UTF8))
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
    }
}
