using Familiada.ViewModel.Base;
using Familiada.Model.DAL;
using System.Runtime;

namespace Familiada.ViewModel
{
    class StrasburgerViewModel: ViewModelBase
    {
        private string[] hisJokes;
        private string[] hisYesYes;
        private string[] hisNoNo;
        private string saying;
        private string currentGifPath;
        //private Image strasburger;
        public string Saying
        {
            get => saying;
            set
            {
                saying = value;
                OnPropertyChanged();
            }
        }
        public string CurrentGifPath
        {
            get => currentGifPath;
            set
            {
                currentGifPath = value;
                OnPropertyChanged();
            }
        }

        public StrasburgerViewModel()
        {
            Saying = "Witaj w Familiadzie! Wpisz nazwę swojej drużyny.";
            CurrentGifPath = "/GameResources/STRASBURGER_Talking.gif";
            hisJokes = DataAccess.GetAllJokes().ToArray();
            hisYesYes = DataAccess.GetYays();
            hisNoNo = DataAccess.GetBoos();
        }

        public void GetRandomJoke()
        {
            System.Random rand = new System.Random();
            int randint = rand.Next(0,hisJokes.Length-1);

            Saying = hisJokes[randint];         
        }
        public void GetRandomYay()
        {
            System.Random rand = new System.Random();
            int randint = rand.Next(0, hisYesYes.Length - 1);

            Saying = hisYesYes[randint];
        }
        public void GetRandomBoo()
        {
            System.Random rand = new System.Random();
            int randint = rand.Next(0, hisNoNo.Length - 1);

            Saying = hisNoNo[randint];
        }
    }
}
