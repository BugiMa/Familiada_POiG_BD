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
        private ObservableCollection<string> points;
        private int total;
        private int loss;
        private string visible;
        private ObservableCollection<string> crossPaths;

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
        public ObservableCollection<string> Points
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

        public ObservableCollection<string> CrossPaths
        {
            get => crossPaths;
            set
            {
                crossPaths = value;
                OnPropertyChanged();
            }
        }
        public void GetRightAnswers(Question currentQuestion)
        {
            RightAnswers.Clear();
            Points.Clear();
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
            DisplayedAnswers.Clear();
            for(int i=0; i<6;i++)
            {
                DisplayedAnswers.Add((i+1)+Properties.Resources.HiddenWord);
            }
        }
        public BoardViewModel()
        {
            displayedAnswers = new ObservableCollection<string>();
            RightAnswers = new ObservableCollection<string>();
            Points = new ObservableCollection<string>();
            Visible = Properties.Resources.Visible;
            CrossPaths = new ObservableCollection<string>();
            for (int i = 0; i < 3; i++)
            {
                CrossPaths.Add(Properties.Resources.NoCrossGif);
            }
        }



    }
}
