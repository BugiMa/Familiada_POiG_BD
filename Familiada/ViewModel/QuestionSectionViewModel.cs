using Familiada.ViewModel.Base;
using Familiada.Model;
using System.Windows;
using System.Timers;
using System.Diagnostics;
using System.Windows.Threading;
using System;

namespace Familiada.ViewModel
{
    class QuestionSectionViewModel : ViewModelBase
    {
        private string answer;
        public DispatcherTimer RealTimer;
        private string timer;
        private Question question;
        private string questionContent;
        public Stopwatch Stopwatch {get;set;}
        public event Action TimeOver;

        public string Answer
        {
            get => answer;
            set { answer = value; OnPropertyChanged(); }
        }
        
        public string Timer
        {
            get => timer;
            set
            {
                timer = value;              
                OnPropertyChanged();
            }
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

        private void TimeTicking(object source, System.EventArgs e)
        {
            int timeLeft = 60 - Stopwatch.Elapsed.Seconds;
            Timer = timeLeft.ToString();
            if(timeLeft==1)
            {
                if (TimeOver != null)
                {
                    TimeOver();
                }
            }
        }
       
        public QuestionSectionViewModel()
        {
            Answer = "";
            Stopwatch = new Stopwatch();
            RealTimer = new DispatcherTimer();
            RealTimer.Interval = System.TimeSpan.FromSeconds(1);
            RealTimer.Tick += TimeTicking;
            RealTimer.Start();
        }



    }
}
