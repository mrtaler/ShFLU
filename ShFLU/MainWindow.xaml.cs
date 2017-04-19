using System.Windows;
using ShFLU.SMGS;
using ShFLY.AllWag;
using ShFLY.SMGS;
using ShFLU.Matrix;

namespace ShFLU
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            SMGS_View smgs = new SMGS_View();
            smgs.ShowDialog();
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            AlllWagView allWag = new AlllWagView();
            allWag.Show();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            allSmgsView allsmgs = new allSmgsView();
            allsmgs.ShowDialog();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MatrixLoad mxLoad = new MatrixLoad();
            mxLoad.ShowDialog();
        }
    }
}
