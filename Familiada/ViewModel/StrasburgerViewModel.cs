using Familiada.ViewModel.Base;
using Familiada.Model.DAL;

namespace Familiada.ViewModel
{
    class StrasburgerViewModel: ViewModelBase
    {
        private string[] whatStrasburgerHasToSay;
        private string saying;
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

        public StrasburgerViewModel()
        {
            Saying = "Witaj w Familiadzie! Wpisz nazwę swojej drużyny.";
            whatStrasburgerHasToSay = DataAccess.GetAllJokes().ToArray();
        }

        public void GetRandomJoke()
        {
            System.Random rand = new System.Random();
            int randint = rand.Next(0,whatStrasburgerHasToSay.Length-1);

            Saying = whatStrasburgerHasToSay[randint];
        }
    }
}
