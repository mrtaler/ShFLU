using Microsoft.Win32;
using ShFLU.DataBase;
using ShFLU.MVVM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShFLU.Matrix
{
  public  class MatrixLoadViewModel:ViewModelBase
    {
        private ShFluContext context = new ShFluContext();
        private string MatrixFilePatch;
        public ViewModelCommand LoadMatrixCommand { get; set; }
        public ViewModelCommand SerialMatrixCommand { get; set; }
        public MatrixLoadViewModel()
        {
            LoadMatrixCommand = new ViewModelCommand(LoadMatrix, true);
            SerialMatrixCommand = new ViewModelCommand(SerialMatrix, false);
        }
        private void LoadMatrix(object param)
        {
            string str = "pkc7";
            string filter = "GCS Skill files (*." + str + ")| *." + str + "| All Files(*.*) | *.* ";
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = @"D:\ДокументыЛинкевич\ПРИХОД ШФУ\ЕТК\02";
            dlg.Filter = filter;
            dlg.ShowDialog();
            if (dlg.FileName != "")
            {
                MatrixFilePatch = dlg.FileName;
                NotifyPropertyChanged("MatrixFilePatch");
                SerialMatrixCommand.CanExecute = true;
            }
        }
        private void SerialMatrix(object param)
        {
            string[] stringFromFile = File.ReadAllLines(MatrixFilePatch);
            StringBuilder strResult = new StringBuilder();
            foreach (var itemStringFromFile in stringFromFile)
            {
                strResult.Append(itemStringFromFile);
                strResult.Append('\n');
            }

        }
    }
}
