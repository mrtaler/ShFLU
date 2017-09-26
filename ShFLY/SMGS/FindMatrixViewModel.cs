using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShFLY.DataBase.DAL.Interfaces;
using ShFLY.DataBase.Models;
using ShFLY.Base;

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
            WagInSmgs = wagInSmgs;
            AddCommand = new ViewModelCommand(Add, true);
            SaveCommand = new ViewModelCommand(Save, true);

            MatrixWagonList = new List<MatrixWagon>(context.MatrixWagonRepository.Get(p => p.WagonNumberMatrix == wagInSmgs.Wagon.Nwag));
        }

        private void Add(object param)
        {
            MatrixWagon qq = (MatrixWagon)param;
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
