using Familiada.ViewModel.Base;
using Familiada.Model;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using Familiada.View;

namespace Familiada.ViewModel
{
    class BoardViewModel: ViewModelBase
    {
        private ObservableCollection<string> rightAnswers;
        private ObservableCollection<string> displayedAnswers;
        private List<string> points;
        private int total;
        private int loss;
        private string visible;

        public ObservableCollection<string> RightAnswers
        {
            get => rightAnswers;
            set { rightAnswers = value; this.OnPropertyChanged(); }
        }

        public ObservableCollection<string> DisplayedAnswers
        {
            get => displayedAnswers;
            set { displayedAnswers = value; OnPropertyChanged(); }
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
        public int Loss
        {
            get => loss;
            set { loss = value; OnPropertyChanged(); }
        }
        public string Visible
        {
            get => visible;
            set
            {
                visible = value;
                OnPropertyChanged();
            }
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

        public void GetDisplayedAnswers(Question currentQuestion)
        {
            for(int i=0; i<6;i++)
            {
                DisplayedAnswers.Add((i+1)+". ----------------");
            }
        }
        public BoardViewModel()
        {
            displayedAnswers = new ObservableCollection<string>();
            RightAnswers = new ObservableCollection<string>();
            Points = new List<string>();
            Visible = "Visible";
        }



    }
}
