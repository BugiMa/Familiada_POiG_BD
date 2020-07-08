using Familiada.ViewModel.Base;
using Familiada.Model;
using System.Windows.Input;
using System.Timers;
using System.Diagnostics;

namespace Familiada.ViewModel
{
    class QuestionSectionViewModel: ViewModelBase
    {
        private string answer;
        private Timer realTimer;
        private string timer;
        private Question question;
        private string questionContent;
        private Stopwatch stopwatch;

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

            realTimer = new Timer(1000);
            realTimer.Elapsed += TimeTicking;
            realTimer.Start();
            stopwatch.Restart();
        }

        private void TimeTicking(object source, ElapsedEventArgs e)
        {
            int timeLeft = 30 - stopwatch.Elapsed.Seconds;
            Timer = timeLeft.ToString();
        }
       
        public QuestionSectionViewModel()
        {
            Answer = "";
            stopwatch = new Stopwatch();
        }



    }
}
