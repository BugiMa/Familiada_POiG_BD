using Familiada.ViewModel.Base;

namespace Familiada.ViewModel
{
    class QuestionSectionViewModel: ViewModelBase
    {
        private string question;
        private string answer;
        private int timer;

        public string Question
        {
            get => question;
            set { question = value; OnPropertyChanged(); }
        }

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
    }
}
