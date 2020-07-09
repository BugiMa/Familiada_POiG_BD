using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    using System.IO;
    using System.Windows.Controls;

    class MainWindowViewModel: ViewModelBase
    {
        public MenuViewModel Menu { get; set; }
        public BoardViewModel Board { get; set; }
        public QuestionSectionViewModel QuestionSection { get; set; }
        public StrasburgerViewModel Strasburger { get; set; }
        public GameOverViewModel GameOver { get; set; }
        private Question[] questions;
        private int round;
        private string finalMessage;
        private string pathToSave;
        public string FinalMessage
        {
            get => finalMessage;
            set
            {
                finalMessage = value;
                OnPropertyChanged();
            }
        }

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
                            Board.DisplayedAnswers[i] =(i+1)+". "+QuestionSection.Answer.ToUpper();
                            Strasburger.CurrentGifPath = "/GameResources/STRASBURGER_WOW.gif";
                                Strasburger.GetRandomYay();
                            break;
                        }
                        else
                        {
                                notIt++;                                
                        }

                            if (notIt == 6)
                            {
                                Board.Loss++;
                                Board.CrossPaths[Board.Loss-1] = "/GameResources/Cross.gif";
                                Strasburger.CurrentGifPath = "/GameResources/STRASBURGER_Boo.gif";
                                Strasburger.GetRandomBoo();
                            }
                    }
                    if(Board.Loss==3)
                    {
                            if (round == 5)
                            {
                                FinalMessage = "Gratulacje " + Menu.TeamName + "! Twoja drużyna uzyskała " + Board.Total + " punktów.";
                                Board.Visible = "Hidden";
                                QuestionSection.Stopwatch.Stop();
                                SaveTotal();
                            }
                            else
                            {
                                /*Stopwatch time = new Stopwatch();
                                time.Restart();
                                while (2-time.Elapsed.Seconds>0)
                                {
                                    Console.WriteLine("dziala");
                                }
                                time.Stop();*/
                                
                                NewQuestion();
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
            Menu.MenuClosed += NewQuestion;
           
            //NewQuestion();

            //QuestionSection.RealTimer.Elapsed+=GameLoop;
        }

        public void NewQuestion()
        {
                QuestionSection.GetRandomQuestion(questions);
                Board.GetRightAnswers(QuestionSection.Question);
                Board.GetDisplayedAnswers(QuestionSection.Question);
                Strasburger.GetRandomJoke();

                Board.Loss = 0;
                QuestionSection.Stopwatch.Restart();
                round++;
                Strasburger.CurrentGifPath = "/GameResources/STRASBURGER_Talking.gif";


            for (int i = 0; i < 3; i++)
            {
                Board.CrossPaths[i] = "/GameResources/NoCross.gif";
            }
        }

        private void TimerSourceUpdated(object sender, DataTransferEventArgs e)
        {

        }


        public void SaveTotal()
        {

            pathToSave = @"C:/Users/Prucfka/source/repos/Familiada_POiG_BD1/Familiada/GameResources/points.txt";
            int counter = 1;
            foreach (string line in File.ReadLines(pathToSave))
            {
                if (line != String.Empty) ++counter;
            }
            string dataToSave = string.Empty;
            dataToSave += $"{counter}.{Menu.TeamName} {Board.Total} pkt";
            if (File.Exists(pathToSave))
            {
                if (new FileInfo(pathToSave).Length == 0)
                {
                    File.WriteAllText(pathToSave, dataToSave);
                    File.AppendAllText(pathToSave, "\n");
                }
                else
                {
                    File.AppendAllText(pathToSave, dataToSave);
                    File.AppendAllText(pathToSave, "\n");
                }

            }
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
