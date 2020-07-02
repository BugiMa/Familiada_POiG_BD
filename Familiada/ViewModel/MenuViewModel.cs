using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Familiada.ViewModel
{
    using Base;
    class MenuViewModel: ViewModelBase
    {
        public string TeamName { get; set; }
        private string visible="";
        public string Visible
        {
            get => visible;
            set
            {
                visible = value;
                this.OnPropertyChanged();
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
                            Visible = "Hidden";
                            this.OnPropertyChanged();
                        },
                        arg => true
                        );
                }
                return newGame;
            }
        }

        public MenuViewModel()
        {
        }
    }
}
