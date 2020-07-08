using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Familiada.ViewModel
{
    using Base;
    using System.Windows.Input;

    class GameOverViewModel:ViewModelBase
    {

        private string visible = "";
        public string Visible
        {
            get => visible;
            set
            {
                visible = value;
                this.OnPropertyChanged();
            }
        }
        private ICommand gameOver;

        public ICommand GameOver
        {
            get
            {
                if (gameOver != null)
                {
                    gameOver = new RelayCommand(
                        arg =>
                        {
                            Visible = "Visible";
                            this.OnPropertyChanged();
                        },
                        arg => true
                        );
                }
                return gameOver;
            }
        }
        public GameOverViewModel()
        {
        }
    }


}
