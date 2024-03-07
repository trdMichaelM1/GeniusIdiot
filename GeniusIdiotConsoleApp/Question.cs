using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeniusIdiotConsoleApp
{
    public class Question
    {
        public string Qu { get; }
        public int Answer { get; }
        public Question(string question, int answer)
        {
            Qu = question;
            Answer = answer;
        }
    }
}
