using QuizzApplication.Classess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzApplication.Entity.Classess
{
    public class PlayerResult
    {
        public int ID { get; set; }
        public int QuizID { get; set; }
        public int UserID { get; set; }
        public int CorrectAnswers { get; set; }

        public Quiz Quiz { get; set; }
        public User User { get; set; }
    }
}
