using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ShFLY.AllWag
{
    /// <summary>
    /// Логика взаимодействия для AlllWagView.xaml
    /// </summary>
    public partial class AlllWagView : Window
    {
        public AlllWagView()
        {
            DataContext = new AllWagViewModel();
            InitializeComponent();
        }
    }
}
