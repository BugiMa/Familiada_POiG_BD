using Familiada.ViewModel.Base;
using Familiada.Model;
using System.Windows.Input;
using System.Timers;
using System.Diagnostics;

namespace Familiada.ViewModel
{
    class QuestionSectionViewModel : ViewModelBase
    {
        private string answer;
        public Timer RealTimer;
        private string timer;
        private Question question;
        private string questionContent;
        public Stopwatch Stopwatch {get;set;}

        public string Answer
        {
            get => answer;
            set { answer = value; OnPropertyChanged(); }
        }
        
        public string Timer
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

        private void TimeTicking(object source, ElapsedEventArgs e)
        {
            int timeLeft = 60 - Stopwatch.Elapsed.Seconds;
            Timer = timeLeft.ToString();
        }
       
        public QuestionSectionViewModel()
        {
            Answer = "";
            Stopwatch = new Stopwatch();
            RealTimer = new Timer(1000);
            RealTimer.Elapsed += TimeTicking;
            RealTimer.Start();
        }



    }
}
