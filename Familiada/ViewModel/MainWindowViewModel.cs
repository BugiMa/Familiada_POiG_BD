using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Familiada.ViewModel
{
    class MainWindowViewModel
    {
        public BoardViewModel Board { get; set; }
        public QuestionSectionViewModel QuestionSection { get; set; }
        public StrasburgerViewModel Strasburger { get; set; }
    }
}
