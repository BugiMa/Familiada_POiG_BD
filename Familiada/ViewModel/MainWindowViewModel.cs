using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Media;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.Timers;
using System.Diagnostics;
using System.Windows.Threading;
using System.Windows.Data;

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
        private int round;

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
                        if (QuestionSection.Answer != "" && rightAnswer.Contains(QuestionSection.Answer) && !Board.DisplayedAnswers.Contains(QuestionSection.Answer))
                        {
                            Board.Total += Convert.ToInt32(Board.Points[i]);
                            Board.DisplayedAnswers[i] =(i+1)+". "+QuestionSection.Answer;
                                Strasburger.CurrentGifPath = "/GameResources/STRASBURGER_WOW.gif";
                            break;
                        }
                        else
                        {
                                notIt++;                                
                        }

                            if (notIt == 6)
                            {
                                Board.Loss++;
                                Strasburger.CurrentGifPath = "/GameResources/STRASBURGER_Boo.gif";
                            }
                    }
                    if(Board.Loss==3)
                    {
                            if (round == 5)
                            {
                                Board.Visible = "Hidden";
                                QuestionSection.Stopwatch.Stop();
                            }
                            else
                            {
                                NewQuestion();
                                Strasburger.GetRandomJoke();
                            }
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

        private ICommand newRound;

        public ICommand NewRound
        {
            get
            {
                if (newRound == null)
                {
                    newRound = new RelayCommand(
                        arg =>
                        {
                            NewQuestion();
                        },
                        arg => Menu.Visible == "Hidden"
                        );
                }
                return newRound;
            }
        }

        public MainWindowViewModel()
        {
            round = 0;
            Music = new SoundPlayer(@"..\..\Familjadee.wav");
            MusicOn = true;

            Menu = new MenuViewModel();
            Board = new BoardViewModel();
            Strasburger = new StrasburgerViewModel();
            QuestionSection = new QuestionSectionViewModel();

            questions = DataAccess.GetAllQuestions().ToArray();

            Music.PlayLooping();

           
            NewQuestion();

            //QuestionSection.RealTimer.Elapsed+=GameLoop;
        }

        public void NewQuestion()
        {
                QuestionSection.GetRandomQuestion(questions);
                Board.GetRightAnswers(QuestionSection.Question);
                Board.GetDisplayedAnswers(QuestionSection.Question);

                Board.Loss = 0;
                QuestionSection.Stopwatch.Restart();
                round++;
        }

        private void TimerSourceUpdated(object sender, DataTransferEventArgs e)
        {

        }

        /*public void GameLoop(object source, ElapsedEventArgs e)
        {
            if (Menu.Visible == "Hidden")
            {
                if (round==0)
                {
                    newGame = false;
                    QuestionSection.Stopwatch.Start();

                    NewQuestion();

                    
                }
                else
                {
                    while (QuestionSection.RealTimer.Enabled)
                    {
                        if (QuestionSection.Timer == "0" || Board.Loss == 3)
                        {
                            //QuestionSection.Stopwatch.Stop();
                            Board.Loss = 0;

                            NewQuestion();

                            QuestionSection.Stopwatch.Restart();
                        }
                    }
                }
            }
        }*/




    }
}
