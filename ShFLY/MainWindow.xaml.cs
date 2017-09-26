using System.Windows;

using ShFLY.SMGS;

namespace ShFLU
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            SMGS_View smgs = new SMGS_View();
            smgs.ShowDialog();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
   // AlllWagView allWag = new AlllWagView();
     // allWag.Show();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            allSmgsView allsmgs = new allSmgsView();
            allsmgs.ShowDialog();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
           // MatrixLoad mxLoad = new MatrixLoad();
       // mxLoad.ShowDialog();
        }

    }
}
