using Familiada.ViewModel.Base;
using Familiada.Model;
using System.Collections.Generic;

namespace Familiada.ViewModel
{
    class BoardViewModel: ViewModelBase
    {
        private List<string> rightAnswers;
        private List<string> points;
        private int total;

        public List<string> RightAnswers
        {
            get => rightAnswers;
            set { rightAnswers = value; OnPropertyChanged(); }
        }
        public List<string> Points
        {
            get => points;
            set { points = value; OnPropertyChanged(); }
        }
        public int Total
        {
            get => total;
            set { total = value; OnPropertyChanged(); }
        }

        public void GetRightAnswers(Question currentQuestion)
        {
            foreach (var answer in currentQuestion.Answers)
            {
                RightAnswers.Add(answer);
            }
            foreach (var point in currentQuestion.Points)
            {
                Points.Add(point);
            }
        }

        public BoardViewModel()
        {
            RightAnswers = new List<string>();
            Points = new List<string>();
        }



    }
}
