using QuizzApplication.Entity.Classess;
using QuizzApplication.Entity.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzApplication.Classess
{
    public class Quiz
    {
        private Dictionary<User, int> _playerResults = new Dictionary<User, int>();

        public int ID { get; set; }

        public string Title { get; set; }

        public virtual ICollection<QuestionBase> Questions { get; set; }
        public virtual ICollection<PlayerResult> PlayerResults { get; set; } = new List<PlayerResult>();

        public Quiz() => Questions = new List<QuestionBase>();

        public Quiz(string title, string description)
        {
            Title = title;
            Questions = new List<QuestionBase>();
        }

        public Quiz(string title, string description, List<QuestionBase> questions)
        {
            Title = title;
            Questions = questions;
        }

        public void AddQuestion(QuestionBase question)
        {
            Questions.Add(question);
        }

        public void DisplayTopPlayers(int topCount)
        {
            Console.WriteLine($"Top {topCount} Players:");

            using (var context = new QuizDBContext())
            {
                var topPlayers = context.PlayerResults
                    .Where(pr => pr.QuizID == ID)
                    .OrderByDescending(pr => pr.CorrectAnswers)
                    .Take(topCount)
                    .ToList();

                foreach (var playerResult in topPlayers)
                {
                    var playerName = context.Users
                        .Where(u => u.UserId == playerResult.UserID)
                        .Select(u => u.Name)
                        .FirstOrDefault();

                    Console.WriteLine($"{playerName}: {playerResult.CorrectAnswers} correct answers");
                }
            }
        }

        public void Start(User currentUser)
        {
            int correctAnswer = 0;
            Console.WriteLine($"You have started taking the quiz: {Title}");

            using (var context = new QuizDBContext())
            {
                foreach (var question in Questions)
                {
                    question.Display();
                    Console.WriteLine("Your answer: ");

                    string[] inputAnswerStr = Console.ReadLine().Split();
                    int[] answer = Array.ConvertAll(inputAnswerStr, Int32.Parse);

                    if (question.IsRight(answer))
                        correctAnswer++;
                }

                Console.WriteLine($"You finished the quiz! Your correct answer count is {correctAnswer}");

                context.PlayerResults.Add(new PlayerResult
                {
                    QuizID = ID,
                    UserID = currentUser.UserId,
                    CorrectAnswers = correctAnswer
                });

                context.SaveChanges();

                Console.WriteLine("\n\n\n");

                DisplayTopPlayers(20);

                Console.WriteLine("\n\n\n");
            }
        }
    }
}
