using QuizzApplication.Entity.Classess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzApplication.Classess
{
    public class MultyQuestion : QuestionBase
    {
        public List<int> Answers { get; set; }

        public MultyQuestion()
        {
            Title = String.Empty;
            Answers = null;
        }

        public MultyQuestion(string title, List<int> answers)
        {
            Title = title;
            Answers = answers;
        }

        public override void Display()
        {
            int index = 1;
            Console.WriteLine($"Question title {Title}");
        }

        public override bool IsRight(params int[] answers)
        {
            if (Answers == null || answers.Length != Answers.Count)
                return false;

            for (int index = 0; index < answers.Length; index++)
            {
                if (answers[index] != Answers[index])
                    return false;
            }

            return true;
        }
    }
}
