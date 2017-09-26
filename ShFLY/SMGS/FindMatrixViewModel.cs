using System.Collections.Generic;

using ShFLY.Base;
using ShFLY.DataBase.DAL.Interfaces;
using ShFLY.DataBase.Models;

namespace ShFLY.SMGS
{
    public class FindMatrixViewModel
    {
        IUnitOfWork context;
        public List<MatrixWagon> MatrixWagonList { get; set; }
        public ViewModelCommand AddCommand { get; set; }
        public ViewModelCommand SaveCommand { get; set; }
        public WagInSmgs WagInSmgs { get; set; }
        public FindMatrixViewModel(WagInSmgs wagInSmgs, IUnitOfWork context)
        {
            this.context = context;
            this.WagInSmgs = wagInSmgs;
            this.AddCommand = new ViewModelCommand(this.Add, true);
            this.SaveCommand = new ViewModelCommand(this.Save, true);

            this.MatrixWagonList = new List<MatrixWagon>(context.MatrixWagonRepository.Get(p => p.WagonNumberMatrix == wagInSmgs.Wagon.Nwag));
        }

        private void Add(object param)
        {
            MatrixWagon qq = (MatrixWagon)param;
            if (qq.Matrixx.MatrixType.Contains("Tara"))
            {
                this.WagInSmgs.MatrixWagonTara = qq;
            }
            else if (qq.Matrixx.MatrixType.Contains("Brutt"))
            {
                this.WagInSmgs.MatrixWagonBrutto = qq;
            }
        }

        private void Save()
        {
            this.context.SaveChanges();
        }
    }
}
