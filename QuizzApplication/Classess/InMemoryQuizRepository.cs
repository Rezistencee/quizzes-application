using QuizzApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzApplication.Classess
{
    public class InMemoryQuizRepository : IRepository<Quiz>
    {
        private List<Quiz> _quizzes = new List<Quiz>();

        public IEnumerable<Quiz> GetAll()
        {
            return _quizzes;
        }

        public void Add(Quiz quiz)
        {
            quiz.ID = _quizzes.Count > 0 ? _quizzes.Max(q => q.ID) + 1 : 1;
            _quizzes.Add(quiz);
        }

        public void Delete(int id)
        {
            var quiz = GetById(id);

            if (quiz != null)
            {
                _quizzes.Remove(quiz);
            }
        }

        public Quiz GetById(int id)
        {
            return _quizzes.FirstOrDefault(q => q.ID == id);
        }
    }
}
