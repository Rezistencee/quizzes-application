using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzApplication.Entity.Classess
{
    public abstract class QuestionBase
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public abstract void Display();
        public abstract bool IsRight(params int[] answers);
    }
}
