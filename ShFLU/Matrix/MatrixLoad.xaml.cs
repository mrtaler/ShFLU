using ShFLU.DataBase.Table;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ShFLU.Matrix
{
    /// <summary>
    /// Логика взаимодействия для MatrixLoad.xaml
    /// </summary>
    public partial class MatrixLoad : Window
    {
        public MatrixLoad()
        {
            DataContext = new MatrixLoadViewModel();
            InitializeComponent();
        }

    }
    class EmptyWagonConverter : MarkupExtension, IMultiValueConverter
    {
        ICollection<MatrixWagon> par1;
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            par1 = values[0] as ICollection<MatrixWagon>;

            if (par1.Count!=0)
            {
               
                bool par2 = (bool)values[1];
                if (par2)
                {
                    return par1;
                }
                else
                {
                    var qt = par1.Where(p => p.WagonNumberMatrix != null);
                    return qt;
                }
            }
            else
            {
                return null;
            }
           
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #region MarkupExtension members
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
        #endregion
    }
}
