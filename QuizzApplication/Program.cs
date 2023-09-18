using QuizzApplication.Classess;
using System;

namespace QuizzApplication
{
    static class Program
    {
        static bool state = true;
        static bool isAuthenticated = false;
        static int currentUserId = -1;
        static InMemoryUserRepository userRepository = new InMemoryUserRepository();
        static InMemoryQuizRepository quizRepository = new InMemoryQuizRepository();

        private static void ProcessRegistration()
        {
            Console.WriteLine("Registration: ");

            Console.WriteLine("Enter your email: ");
            string inputEmail = Console.ReadLine();

            User existingUser = userRepository.GetByEmail(inputEmail);

            if (existingUser != null)
            {
                Console.WriteLine("User with this email already exists. Please enter a different email.");
                inputEmail = Console.ReadLine();
            }

            Console.WriteLine("Enter a password: ");
            string inputPassword = Console.ReadLine();

            Console.WriteLine("Enter your name: ");
            string inputName = Console.ReadLine();

            User newUser = new User
            {
                Email = inputEmail,
                Password = inputPassword,
                Name = inputName
            };

            userRepository.Add(newUser);
        }


        private static void ProcessAuthorization()
        {
            Console.WriteLine("Authorization: ");

            Console.WriteLine("Enter your email: ");
            string inputEmail = Console.ReadLine();

            Console.WriteLine("Enter your password: ");
            string inputPassword = Console.ReadLine();

            User user = userRepository.GetByEmail(inputEmail);

            if (user != null && user.Password == inputPassword)
            {
                isAuthenticated = true;
                currentUserId = user.UserId;
                Console.WriteLine("Authorization successful!");
            }
            else
            {
                Console.WriteLine("Invalid email or password. Please try again.");
            }
        }

        private static void ProcessBrowsingQuizzes()
        {
            Console.WriteLine("\tAvailable quizzes: ");
            
            var quizes = quizRepository.GetAll();

            foreach(Quiz quiz in quizes)
            {
                Console.WriteLine($"\t{quiz.ID}. {quiz.Title}");
            }

            Console.WriteLine("\n\n\n");
        }

        private static void ProcessSignUpForQuiz()
        {
            Console.WriteLine("Sign up for the quiz: ");
            Console.WriteLine("Enter quiz ID to join this quiz:");
            
            int joinableQuizID = Convert.ToInt32(Console.ReadLine());

            Quiz targetQuiz = quizRepository.GetById(joinableQuizID);

            targetQuiz.Start(userRepository.GetById(currentUserId));
        }

        private static void SetPassword()
        {
            Console.WriteLine("Input a new password: ");

            string inputPassword = Console.ReadLine();

            User user = userRepository.GetById(currentUserId);

            user.Password = inputPassword;

            Console.WriteLine("Password was changed!");
        }

        private static void QuizBuild()
        {
            Question tempQuestion = new Question("How many wives did Henry VIII have?", 6);
            Quiz tempQuiz = new Quiz("History quiz", "Something desc...");
            tempQuiz.AddQuestion(tempQuestion);
            quizRepository.Add(tempQuiz);

            tempQuiz = new Quiz("English quiz", "Something desc...");
            tempQuestion = new Question("We are eating at home tonight\n\nAnswer option:\n1. in\n2. at\n3.in the", 2);
            tempQuiz.AddQuestion(tempQuestion);

            MultyQuestion tempMultyQuestion = new MultyQuestion("Test, anwser is 4 5 7", new int[] { 4, 5, 7 });

            tempQuiz.AddQuestion(tempMultyQuestion);

            quizRepository.Add(tempQuiz);
        }

        public static void Main()
        {
            QuizBuild();

            while (state) {
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