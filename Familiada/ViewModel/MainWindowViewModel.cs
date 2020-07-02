using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Media;
using Microsoft.VisualBasic;
using System.Windows.Forms;

namespace Familiada.ViewModel
{
    using Base;
    using Model;
    class MainWindowViewModel: ViewModelBase
    {
        public MenuViewModel Menu { get; set; }
        public BoardViewModel Board { get; set; }
        public QuestionSectionViewModel QuestionSection { get; set; }
        public StrasburgerViewModel Strasburger { get; set; }
        public SoundPlayer Music { get; set; }
        //public string TeamName { get; set; }

        private ICommand checkAnswer;
        public ICommand CheckAnswer
        {
            get
            {
                if (checkAnswer == null)
                {
                    checkAnswer = new RelayCommand(
                        arg =>
                        {

                        },
                        arg => true
                        );
                }
                return checkAnswer;
            }
        }

        public MainWindowViewModel()
        {
            Music = new SoundPlayer(@"..\..\Familjadee.wav");

            Menu = new MenuViewModel();
            Board = new BoardViewModel();
            Strasburger = new StrasburgerViewModel();
            QuestionSection = new QuestionSectionViewModel();

            Music.PlayLooping();

        }

        

    }
}
