using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzApplication.Interfaces
{
    public interface IQuestion
    {
        string Title { get; set; }
        void Display();
        bool isRight(params int[] answers);
    }
}
