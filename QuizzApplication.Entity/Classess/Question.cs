using QuizzApplication.Entity.Classess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzApplication.Classess
{
    public class Question : QuestionBase
    {
        public int Answer { get; set; }

        public Question()
        {
            Title = String.Empty;
            Answer = 0;
        }

        public Question(string title, int answer)
        {
            Title = title;
            Answer = answer;
        }

        public override void Display()
        {
            Console.WriteLine($"Question: {Title}\n");
        }

        public override bool IsRight(params int[] answers)
        {
            if (answers[0] != Answer)
                return false;

            return true;
        }
    }
}
