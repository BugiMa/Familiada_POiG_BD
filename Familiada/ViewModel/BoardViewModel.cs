using Familiada.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Familiada.ViewModel
{
    class BoardViewModel: ViewModelBase
    {
        private List<string> rightAnswers;
        private List<int> points;
        private int total;

        public List<string> RightAnswers
        {
            get => rightAnswers;
            set { rightAnswers = value; OnPropertyChanged(); }
        }
        public List<int> Points
        {
            get => points;
            set { points = value; OnPropertyChanged(); }
        }
        public int Total
        {
            get => total;
            set { total = value; OnPropertyChanged(); }
        }

        public BoardViewModel()
        {

        }



    }
}
