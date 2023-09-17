using QuizzApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzApplication.Classess
{
    public class Quiz
    {
        private int _id;
        private string _title;
        private string _description;
        private Dictionary<User, int> _playerResults = new Dictionary<User, int>();
        private IQuestion[] _questions;
        private int _currentPosition;

        public int ID 
        {
            get
            {
                return _id;
            }
            set
            {
                if(value > 0)
                    _id = value;
                else 
                    throw new IndexOutOfRangeException();
            }
        }

        public string Title
        {
            get 
            { 
                return _title; 
            }
        }

        public Quiz()
        {
            _id = 0;
            _title = String.Empty;
            _description = String.Empty;
            _questions = null;
            _currentPosition = 0;
        }

        public Quiz(string title, string description)
        {
            _id = 0;
            _title = title;
            _description = description;
            _questions = new IQuestion[20];
            _currentPosition = 0;
        }

        public Quiz(string title, string description, IQuestion[] questions)
        {
            _id = 0;
            _title = title;
            _description = description;
            _questions = questions;
            _currentPosition = questions.Length;
        }

        public void AddQuestion(IQuestion question)
        {
            if(_currentPosition < _questions.Length)
                _questions[_currentPosition++] = question;
        }

        public void DisplayTopPlayers(int topCount)
        {
            Console.WriteLine($"Top {topCount} Players:");

            var sortedResults = _playerResults.OrderByDescending(kv => kv.Value).Take(topCount);

            foreach (var kvp in sortedResults)
            {
                Console.WriteLine($"{kvp.Key.Name}: {kvp.Value} correct answers");
            }
        }

        public void Start(User currentUser)
        {
            int correctAnswer = 0;
            Console.WriteLine($"You have started taking the quiz: {Title}");

            for(int index = 0; index < _currentPosition; index++)
            {
                _questions[index].Display();
                Console.WriteLine("Your answer: ");

                string[] inputAnswerStr = Console.ReadLine().Split();
                int[] answer = Array.ConvertAll(inputAnswerStr, Int32.Parse);

                if (_questions[index].isRight(answer))
                    correctAnswer++;
            }

            Console.WriteLine($"You finished the quiz! Your correct answer count is {correctAnswer}");

            _playerResults.Add(currentUser, correctAnswer);

            Console.WriteLine("\n\n\n");

            DisplayTopPlayers(20);

            Console.WriteLine("\n\n\n");
        }
    }
}
