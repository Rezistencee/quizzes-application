using Microsoft.EntityFrameworkCore;
using QuizzApplication.Classess;
using QuizzApplication.Entity.Context;
using System;

namespace QuizzApplication
{
    static class Program
    {
        static bool state = true;
        static bool isAuthenticated = false;
        static int currentUserId = -1;

        private static void ProcessRegistration()
        {
            Console.WriteLine("\t| Registration: ");

            Console.WriteLine("\t| Enter your email: ");
            string inputEmail = Console.ReadLine();

            using(var context = new QuizDBContext())
            {
                var existingUser = context.Users.FirstOrDefault(u => u.Email == inputEmail);

                if(existingUser != null)
                {
                    Console.WriteLine("\t| User with this email already exists. Please enter a different email.");
                    inputEmail = Console.ReadLine();
                }

                Console.WriteLine("\t| Enter a password: ");
                string inputPassword = Console.ReadLine();

                Console.WriteLine("\t| Enter your name: ");
                string inputName = Console.ReadLine();

                var newUser = new User
                {
                    Email = inputEmail,
                    Password = inputPassword,
                    Name = inputName
                };

                context.Users.Add(newUser);
                context.SaveChanges();
            }
        }


        private static void ProcessAuthorization()
        {
            Console.WriteLine("\t| Authorization: ");

            Console.WriteLine("Enter your email: ");
            string inputEmail = Console.ReadLine();

            Console.WriteLine("\t| Enter your password: ");
            string inputPassword = Console.ReadLine();

            using (var context = new QuizDBContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email == inputEmail && u.Password == inputPassword);

                if (user != null)
                {
                    isAuthenticated = true;
                    currentUserId = user.UserId;
                    Console.WriteLine("\t| Authorization successful!");
                }
                else
                {
                    Console.WriteLine("\t| Invalid email or password. Please try again.");
                }
            }
        }

        private static void ProcessBrowsingQuizzes()
        {
            Console.WriteLine("\tAvailable quizzes: ");

            using(var context = new QuizDBContext())
            {
                var quizes = context.Quizzes.ToList();

                foreach (Quiz quiz in quizes)
                {
                    Console.WriteLine($"\t{quiz.ID}. {quiz.Title}");
                }
            }

            Console.WriteLine("\n\n\n");
        }

        private static void ProcessSignUpForQuiz()
        {
            Console.WriteLine("\t| Sign up for the quiz: ");
            Console.WriteLine("\t| Enter quiz ID to join this quiz:");

            int joinableQuizID = Convert.ToInt32(Console.ReadLine());

            using (var context = new QuizDBContext())
            {
                Quiz targetQuiz = context.Quizzes.Include(q => q.Questions).FirstOrDefault(q => q.ID == joinableQuizID);

                if (targetQuiz != null)
                {
                    User currentUser = context.Users.FirstOrDefault(u => u.UserId == currentUserId);

                    if (currentUser != null)
                    {
                        targetQuiz.Start(currentUser);
                    }
                    else
                    {
                        Console.WriteLine("\t| User not found!");
                    }
                }
                else
                {
                    Console.WriteLine("\t| Quiz with this ID does not exist!");
                }
            }
        }

        private static void SetPassword()
        {
            Console.WriteLine("\t| Input a new password: ");

            string inputPassword = Console.ReadLine();

            using (var context = new QuizDBContext())
            {
                var user = context.Users.FirstOrDefault(u => u.UserId == currentUserId);

                if (user != null)
                {
                    user.Password = inputPassword;
                    context.SaveChanges();

                    Console.WriteLine("\t| Password was changed!");
                }
                else
                {
                    Console.WriteLine("\t| User not found.");
                }
            }
        }

        private static void QuizBuild()
        {
            using(var context = new QuizDBContext())
            {
                Question tempQuestion = new Question
                {
                    Title = "How many wives did Henry VIII have?",
                    Answer = 6
                };

                Quiz tempQuiz = new Quiz("History quiz", "Something desc...");
                tempQuiz.AddQuestion(tempQuestion);
                context.Quizzes.Add(tempQuiz);

                tempQuiz = new Quiz("English quiz", "Something desc...");

                tempQuestion = new Question
                {
                    Title = "We are eating ... home tonight\n\nAnswer option:\n1. in\n2. at\n3.in the",
                    Answer = 2
                };
                tempQuiz.AddQuestion(tempQuestion);

                MultyQuestion tempMultyQuestion = new MultyQuestion
                {
                    Title = "Test,anwser is 4 5 7",
                    Answers = new List<int> { 4, 5, 7 }
                };

                tempQuiz.AddQuestion(tempMultyQuestion);

                context.Quizzes.Add(tempQuiz);

                context.SaveChanges();
            }
        }

        public static void Main()
        {
            QuizBuild();

            while (state)
            {
                Console.WriteLine("1. Registration");
                Console.WriteLine("2. Authorization");

                if (isAuthenticated)
                {
                    Console.WriteLine("3. Browse available quizzes");
                    Console.WriteLine("4. Sign up for the quiz");
                    Console.WriteLine("5. Set password");
                    Console.WriteLine("0. Logout");
                }

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        {
                            ProcessRegistration();
                            break;
                        }

                    case "2":
                        {
                            ProcessAuthorization();
                            break;
                        }

                    case "3":
                        {
                            if (isAuthenticated)
                                ProcessBrowsingQuizzes();
                            else
                                Console.WriteLine("You need to be authenticated in system!");

                            break;
                        }

                    case "4":
                        {
                            if (isAuthenticated)
                                ProcessSignUpForQuiz();
                            else
                                Console.WriteLine("You need to be authenticated in system!");
                            break;
                        }
                    case "5":
                        {
                            if (isAuthenticated)
                                SetPassword();
                            else
                                Console.WriteLine("You need to be authenticated in system!");
                            break;
                        }
                    case "0":
                        {
                            isAuthenticated = false;
                            currentUserId = -1;
                            break;
                        }

                    default:
                        {
                            Console.WriteLine("Wrong choice. Try again.");
                            break;
                        }
                }
            }
        }
    }
}