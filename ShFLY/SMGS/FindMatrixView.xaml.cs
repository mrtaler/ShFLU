using System.Windows;

using ShFLY.DataBase.DAL.Interfaces;
using ShFLY.DataBase.Models;

namespace ShFLY.SMGS
{
    /// <summary>
    /// Логика взаимодействия для FindMatrixView.xaml
    /// </summary>
    public partial class FindMatrixView : Window
    {
        public FindMatrixView(WagInSmgs paramWagInSmgs, IUnitOfWork context)
        {
            this.DataContext = new FindMatrixViewModel(paramWagInSmgs, context);
            this.InitializeComponent();
        }
    }
}
