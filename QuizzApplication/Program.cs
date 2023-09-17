using System;

namespace QuizzApplication
{
    static class Program
    {
        static bool state = true;
        static bool isAuthenticated = false;
        static int currentUserId = -1;

        //TODO: Complete this 4 methods.
        private static void ProcessRegistration()
        {
            Console.WriteLine("Registration: ");
            
        }

        private static void ProcessAuthorization()
        {
            Console.WriteLine("Authorization: ");
            
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