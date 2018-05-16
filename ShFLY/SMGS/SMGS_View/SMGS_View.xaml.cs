using System.Windows;

namespace ShFLY.SMGS
{
    /// <summary>
    /// Логика взаимодействия для SMGS_View.xaml
    /// </summary>
    public partial class SMGS_View : Window
    {
        public SMGS_View()
        {
            this.DataContext = new SmgsViewModel();
            this.InitializeComponent();
        }
    }
}
