using Familiada.ViewModel.Base;
using Familiada.Model.DAL;

namespace Familiada.ViewModel
{
    class StrasburgerViewModel: ViewModelBase
    {
        private string[] whatStrasburgerSays;
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
            //whatStrasburgerSays = DataAccess.GetAllJokes().ToArray();
        }
    }
}
