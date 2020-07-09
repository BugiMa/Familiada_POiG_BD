using Familiada.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Familiada
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //MainWindowViewModel observer = new MainWindowViewModel();
        //MenuViewModel observed = new MenuViewModel();
        public MainWindow()
        {
            InitializeComponent();

            //observed.MenuClosed += observer.NewQuestion;
        }
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    observed.Visible = "Hidden";
            //zmieniając własność obiekt emituje MojeZdarzenie
        //}
    }
}
