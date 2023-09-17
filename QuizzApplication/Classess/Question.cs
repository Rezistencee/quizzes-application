using QuizzApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzApplication.Classess
{
    public class Question : IQuestion
    {
        private string _title;
        private int _answer;

        public string Title 
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        public Question()
        {
            _title = String.Empty;
            _answer = 0;
        }

        public Question(string title, int answer)
        {
            _title = title;
            _answer = answer;
        }

        public void Display()
        {
            Console.WriteLine($"Question title {_title}");
        }

        public bool isRight(params int[] answers)
        {
            if (answers[0] != _answer)
                return false;

            return true;
        }
    }
}
