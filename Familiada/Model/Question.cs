using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Familiada.Model
{
    class Question
    {
        public string QuestionContent { get; set; }
        public string[] Answers;
        public string[] Points;

        public Question(string QuestionContent, string[] Answers, string[] Points)
        {
            this.QuestionContent = QuestionContent;
            this.Answers = Answers;
            this.Points = Points;
        }
    }
}
