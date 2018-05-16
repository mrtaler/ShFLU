using System.Windows;

namespace ShFLY.SMGS
{
    /// <summary>
    /// Логика взаимодействия для allSmgsView.xaml
    /// </summary>
    public partial class allSmgsView : Window
    {
        public allSmgsView()
        {
            this.DataContext = new AllSmgsViewModel();
            this.InitializeComponent();
        }
    }
}
