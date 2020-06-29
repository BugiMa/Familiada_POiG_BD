using Familiada.ViewModel.Base;

namespace Familiada.ViewModel
{
    class StrasburgerViewModel: ViewModelBase
    {
        private string saying;
        //private Image strasburger;

        public string Saying
        {
            get => saying;
            set { saying = value; OnPropertyChanged(); }
        }
    }
}
