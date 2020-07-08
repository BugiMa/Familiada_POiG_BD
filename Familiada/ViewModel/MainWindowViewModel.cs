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
    using Model.DAL;
    using System.Windows.Controls;

    class MainWindowViewModel: ViewModelBase
    {
        public MenuViewModel Menu { get; set; }
        public BoardViewModel Board { get; set; }
        public QuestionSectionViewModel QuestionSection { get; set; }
        public StrasburgerViewModel Strasburger { get; set; }
        private Question[] questions;
        

        public SoundPlayer Music { get; set; }
        private bool musicOn;
        public bool MusicOn
        {
            get => musicOn;
            set
            {
                musicOn = value;
                this.OnPropertyChanged();
            }
        }


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
                    int i = -1;
                        int notIt = 0;
                    foreach (var rightAnswer in Board.RightAnswers)
                    {
                        i++;
                        if (QuestionSection.Answer != "" && rightAnswer.Contains(QuestionSection.Answer))
                        {
                            Board.Total += Convert.ToInt32(Board.Points[i]);
                            Board.DisplayedAnswers[i] =(i+1)+". "+QuestionSection.Answer;
                            break;
                        }
                        else
                        {
                                notIt++;                                
                        }

                            if (notIt == 6) Board.Loss++;
                    }
                    if(Board.Loss==3)
                    {
                            Board.Total = 0;
                    }
                        



                        QuestionSection.Answer = "";
                    },
                    arg => true
                    );

                }
                return checkAnswer;
            }
        }
        private ICommand musicOnOff;
        public ICommand MusicOnOff
        {
            get
            {
                if (musicOnOff == null)
                {
                    musicOnOff = new RelayCommand(
                        arg =>
                        {
                            if (MusicOn == true)
                            {
                                Music.Stop();
                                MusicOn = false;
                            }
                            else
                            {
                                Music.PlayLooping();
                                MusicOn = true;
                            }
                        },
                        arg => true
                        );
                }
                return musicOnOff;
            }
        }

        public MainWindowViewModel()
        {
            Music = new SoundPlayer(@"..\..\Familjadee.wav");
            MusicOn = true;

            Menu = new MenuViewModel();
            Board = new BoardViewModel();
            Strasburger = new StrasburgerViewModel();
            QuestionSection = new QuestionSectionViewModel();

            questions = DataAccess.GetAllQuestions().ToArray();

            Music.PlayLooping();
            QuestionSection.GetRandomQuestion(questions);
            Board.GetRightAnswers(QuestionSection.Question);
            Board.GetDisplayedAnswers(QuestionSection.Question);
            

        }

        public void GameLoop()
        {
           
             
        }

        

    }
}
