using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShFLU.DataBase;
using ShFLU.DataBase.Table;
using ShFLU.MVVM;

namespace ShFLU.SMGS.allsmgs.FindMatrix
{
 public   class FindMatrixViewModel
    {
        ShFluContext context;
        public List<MatrixWagon> MatrixWagonList { get; set; }
        public ViewModelCommand AddCommand { get; set; }
        public ViewModelCommand SaveCommand { get; set; }
        public WagInSmgs WagInSmgs { get; set; }
        public FindMatrixViewModel(WagInSmgs wagInSmgs, ShFluContext context)
        {
           this.context = context;
            WagInSmgs = wagInSmgs;
            AddCommand = new ViewModelCommand(Add, true);
            SaveCommand = new ViewModelCommand(Save, true);

            MatrixWagonList =new List<MatrixWagon>(context.MatrixWagonDbSet.Where(p=>p.WagonNumberMatrix==wagInSmgs.Wagon.Nwag));
        }

        private void Add(object param)
        {
            MatrixWagon qq = (MatrixWagon) param;
            if (qq.Matrixx.MatrixType.Contains("Tara"))
            {
                WagInSmgs.MatrixWagonTara = qq;
            }
            else if (qq.Matrixx.MatrixType.Contains("Brutt"))
            {
                WagInSmgs.MatrixWagonBrutto = qq;
            }
        }
        private void Save()
        {
            context.SaveChanges();
        }
    }
}
