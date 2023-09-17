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

        //TODO: Complete this 4 methods.
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
            Console.WriteLine("Available quizzes: ");
            
        }

        private static void ProcessSignUpForQuiz()
        {
            Console.WriteLine("Sign up for the quiz: ");
            
        }

        public static void Main()
        {
            while (state) {
                Console.WriteLine("1. Registration");
                Console.WriteLine("2. Authorization");
                Console.WriteLine("3. Browse available quizzes");
                Console.WriteLine("4. Sign up for the quiz");
                Console.WriteLine("5. Exit");

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
                            ProcessBrowsingQuizzes();
                            break;
                        }

                    case "4":
                        {
                            ProcessSignUpForQuiz();
                            break;
                        }

                    case "5":
                        {
                            state = false;
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