using QuizzApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzApplication.Classess
{
    public class MultyQuestion : IQuestion
    {

        private string _title;
        private int[] _answers;

        public string Title { get; set; }

        public string Answer
        {
            get
            {
                return _title;
            }
        }

        public MultyQuestion()
        {
            _title = String.Empty;
            _answers = null;
        }

        public MultyQuestion(string title, int[] answers)
        {
            _title = title;
            _answers = answers;
        }

        public void Display()
        {
            int index = 1;
            Console.WriteLine($"Question title {_title}");

            foreach (var item in _answers)
            {
                Console.WriteLine($"\t{index}. {item}");
            }
        }

        public bool isRight(params int[] answers)
        {
            if (_answers == null || answers.Length != _answers.Length)
                return false;

            for (int index = 0; index < answers.Length; index++)
            {
                if (answers[index] != _answers[index])
                    return false;
            }

            return true;
        }
    }
}
