using System;
using System.Collections.Generic;
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
using ShFLU.SMGS;
using ShFLY.AllWag;
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
    }
}
