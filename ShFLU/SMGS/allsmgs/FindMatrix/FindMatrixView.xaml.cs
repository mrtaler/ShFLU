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
using System.Windows.Shapes;
using ShFLU.DataBase;
using ShFLU.DataBase.Table;

namespace ShFLU.SMGS.allsmgs.FindMatrix
{
    /// <summary>
    /// Логика взаимодействия для FindMatrixView.xaml
    /// </summary>
    public partial class FindMatrixView : Window
    {
        public FindMatrixView(WagInSmgs paramWagInSmgs, ShFluContext context)
        {
            DataContext = new FindMatrixViewModel(paramWagInSmgs,context);
            InitializeComponent();
        }
    }
}
