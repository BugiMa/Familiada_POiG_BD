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
    using System.IO;
    using System.Windows.Controls;
    using Properties;

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
                            if (QuestionSection.Answer != "" && rightAnswer.Contains(QuestionSection.Answer.ToLower()) && !Board.DisplayedAnswers.Contains(QuestionSection.Answer.ToUpper()))
                            {
                                Board.Total += Convert.ToInt32(Board.Points[i]);
                                Board.DisplayedAnswers[i] = (i + 1) + ". " + QuestionSection.Answer.ToUpper();
                                Strasburger.CurrentGifPath = Resources.StrasburgerWowGif;
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
                                Board.CrossPaths[Board.Loss - 1] = Resources.CrossGif;
                                Strasburger.CurrentGifPath = Resources.StrasburgerBooGif;
                                Strasburger.GetRandomBoo();
                            }
                        }
                        if (Board.Loss == 3)
                        {
                            if (round == 5)
                            {
                                FinalMessage = Resources.Congrats + Menu.TeamName + Resources.YourTeamGained+" "+ Board.Total + Resources.PointsSign;
                                Board.Visible = Resources.Hidden;
                                QuestionSection.Stopwatch.Stop();
                                Strasburger.Saying = Resources.ByeMessage;
                                SaveTotal();
                                round++;
                            }
                            else
                            {
                                NewQuestion();
                            }
                        }


                        QuestionSection.Answer = "";
                    },
                    arg => round <= 5 && Menu.Visible == Resources.Hidden && Board.Loss < 3
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
                        arg => Menu.Visible == Resources.Hidden
                        );
                }
                return newRound;
            }
        }

        public MainWindowViewModel()
        {
            round = 0;
            Music = new SoundPlayer(Resources.MusicFile);
            MusicOn = true;

            Menu = new MenuViewModel();
            Board = new BoardViewModel();
            Strasburger = new StrasburgerViewModel();
            QuestionSection = new QuestionSectionViewModel();

            questions = DataAccess.GetAllQuestions().ToArray();

            Music.PlayLooping();
            Menu.MenuClosed += NewQuestion;
            QuestionSection.TimeOver += NewQuestion;
           
            //NewQuestion();

            //QuestionSection.RealTimer.Elapsed+=GameLoop;
        }

        public async void NewQuestion()
        {
            if(round!=0) await Task.Delay(2000);
                QuestionSection.GetRandomQuestion(questions);
                Board.GetRightAnswers(QuestionSection.Question);
                Board.GetDisplayedAnswers(QuestionSection.Question);
                Strasburger.GetRandomJoke();

                Board.Loss = 0;
                QuestionSection.Stopwatch.Restart();
                round++;
            Strasburger.CurrentGifPath = Resources.StrasburgerTalkingGif;


            for (int i = 0; i < 3; i++)
            {
                Board.CrossPaths[i] = Resources.NoCrossGif;
            }
        }

        private void TimerSourceUpdated(object sender, DataTransferEventArgs e)
        {

        }


        public void SaveTotal()
        {
            pathToSave = Resources.PointsFile;
            string dataToSave = File.ReadAllText(Resources.PointsFile);
            dataToSave += $"{Menu.TeamName};{Board.Total}\n";
            File.WriteAllText(pathToSave, dataToSave);
          
        }

        private ICommand bestScores;
        public ICommand BestScores
        {
            get
            {
                if (bestScores == null)
                {
                    bestScores = new RelayCommand(
                        arg =>
                        {
                            int i = 0;
                            string points = "";
                            List<string[]> scores = new List<string[]> ();
                            foreach (string line in File.ReadLines(Resources.PointsFile))
                            { 
                                if (line != "")
                                {
                                    scores.Add(line.Split(';'));
                                }
                            }
                            scores = scores.OrderByDescending(arr => Int32.Parse(arr[1])).ToList();

                            foreach (string[] player in scores)
                            {
                                points += $"{++i}. {player[0]}   {player[1]} {Resources.PointsSign}\n";
                            }
                            MessageBox.Show(points);
                        },
                        arg => true
                        );
                }
                return bestScores;
            }
        }

        private ICommand instruction;
        public ICommand Instruction
        {
            get
            {
                if (instruction == null)
                {
                    instruction = new RelayCommand(
                        arg =>
                        {
                            MessageBox.Show(Resources.Instruction);
                        },
                        arg => true
                        );
                }
                return instruction;
            }
        }

        private ICommand newGame;
        public ICommand NewGame
        {
            get
            {
                if (newGame == null)
                {
                    newGame = new RelayCommand(
                        arg =>
                        {
                            Application.Restart();
                            System.Windows.Application.Current.Shutdown();
                        },
                        arg => true
                        );
                }
                return newGame;
            }
        }

    }
}
