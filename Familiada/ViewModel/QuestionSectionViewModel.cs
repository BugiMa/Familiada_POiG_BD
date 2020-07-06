using Familiada.ViewModel.Base;
using Familiada.Model;

namespace Familiada.ViewModel
{
    class QuestionSectionViewModel: ViewModelBase
    {
        private string answer;
        private int timer;
        private Question currentQuestion;

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
        public Question CurrentQuestion
        {
            get => currentQuestion;
            set { currentQuestion = value; OnPropertyChanged(); }
        }


    }
}
