using Familiada.ViewModel.Base;
using Familiada.Model;

namespace Familiada.ViewModel
{
    class QuestionSectionViewModel: ViewModelBase
    {
        private string answer="";
        private int timer;
        private Question question;
        private string questionContent;

        public string Answer
        {
            get => answer;
            set { answer = value; OnPropertyChanged(); }
        }

        public int Timer
        {
            get => timer;
            set { timer = value; OnPropertyChanged(); }
        }
        public Question Question
        {
            get => question;
            set { question = value; OnPropertyChanged(); }
        }
        public string QuestionContent
        {
            get => questionContent;
            set { questionContent = value; OnPropertyChanged(); }
        }

        public void GetRandomQuestion(Question[] questions)
        {
            System.Random rand = new System.Random();
            int randint = rand.Next(0, questions.Length);

            question = questions[randint];
            QuestionContent = question.QuestionContent;
        }

    }
}
