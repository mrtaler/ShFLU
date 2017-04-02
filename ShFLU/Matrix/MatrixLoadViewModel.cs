using Microsoft.Win32;
using ShFLU.DataBase;
using ShFLU.DataBase.Table;
using ShFLU.MVVM;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using ShFLU.Matrix.OpenFromDB;

namespace ShFLU.Matrix
{
    public class MatrixLoadViewModel : ViewModelBase
    {
        private ShFluContext context = new ShFluContext();
        private string matrixFilePatch;
        public Matrixx Matrix { get; set; }

        public ViewModelCommand LoadMatrixCommand { get; set; }
        public ViewModelCommand EditCommand { get; set; }
        public ViewModelCommand ViewAllCommand { get; set; }
        public ViewModelCommand SaveContextCommand { get; set; }
        public ViewModelCommand OpenFromDbCommand { get; set; }
        public MatrixLoadViewModel()
        {
            Matrix = new Matrixx();
            
            LoadMatrixCommand = new ViewModelCommand(LoadMatrix, true);
            EditCommand = new ViewModelCommand(edit, true);
            ViewAllCommand = new ViewModelCommand(ViewAll, true);
            SaveContextCommand = new ViewModelCommand(SaveContext, true);
            OpenFromDbCommand = new ViewModelCommand(OpenFromDb, true);
        }
        private void LoadMatrix(object param)
        {
            string str = "txt";
            string filter = "GCS Skill files (*." + str + ")| *." + str + "| All Files(*.*) | *.* ";
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = @"C:\Users\Derdan\Dropbox\Work\Matrix";
            dlg.Filter = filter;
            dlg.ShowDialog();
            if (dlg.FileName != "")
            {
                matrixFilePatch = dlg.FileName;
                NotifyPropertyChanged("MatrixFilePatch");

                Encoding enc = Encoding.GetEncoding(1251);

                string[] stringFromFile = File.ReadAllLines(matrixFilePatch, enc);
                StringBuilder strResult = new StringBuilder();
                foreach (var itemStringFromFile in stringFromFile)
                {
                    strResult.Append(itemStringFromFile);
                    strResult.Append('\n');
                }
                var date = Regex.Match(strResult.ToString(), @"(?<=Дата и время взвешивания: )(.*)");
                var dateee = Convert.ToDateTime(date.ToString());
                var mtrNumVar = Regex.Match(strResult.ToString(), @"(?<=Номер взвешивания:)\W\d*(?=;)").ToString();

                int mtrNum = Convert.ToInt32(mtrNumVar);
                var matrixContext = context.MatrixxDbSet.FirstOrDefault(p => p.MatrixNum == mtrNum);

                if (matrixContext != null)
                {
                    matrixContext.MatrixDate = dateee;
                    matrixContext.MatrixType = (string)param;
                    var vagonsFromMatrix = Regex.Matches(strResult.ToString(), @":.*:.*:.*:.*:");

                    foreach (var item in vagonsFromMatrix)
                    {
                        var spr = item.ToString().Split(':');
                        int vgnNum = Convert.ToInt32(spr[1]);

                        int? vagnum = null;
                        
                        if (Regex.IsMatch(spr[3], @"\d{8}"))
                        {
                            vagnum = Convert.ToInt32(spr[3]);
                        }

                        var matrWgn = matrixContext.MatrixWagons.First(p => p.WagonNumberPP == vgnNum);
                        matrWgn.WagonNumberMatrix = vagnum;
                        matrWgn.WagonNumberPP = Convert.ToInt32(spr[1]);
                        matrWgn.Speed = spr[2];
                        matrWgn.Weight = spr[4];
                    }
                    Matrix = matrixContext;
                    NotifyPropertyChanged("Matrix");
                    context.SaveChanges();
                }
                else
                {
                    Matrixx mtr = new Matrixx();
                    mtr.MatrixDate = dateee;
                    mtr.MatrixNum = mtrNum;
                    mtr.MatrixType = (string)param;

                    var vagons = Regex.Matches(strResult.ToString(), @":.*:.*:.*:.*:");
                    foreach (var item in vagons)
                    {

                        var spr = item.ToString().Split(':');

                        MatrixWagon mtrwgn = new MatrixWagon();
                        mtrwgn.WagonNumberPP = Convert.ToInt32(spr[1]);
                        mtrwgn.Speed = spr[2].Trim();
                        mtrwgn.Weight = spr[4].Trim();

                        int vagnum;
                        if (Regex.IsMatch(spr[3], @"\d{8}"))
                        {
                            vagnum = Convert.ToInt32(spr[3]);
                            Wagon insertWag = context.WagonDbSet.FirstOrDefault(p => p.Nwag == vagnum);
                            if (insertWag == null)
                            {
                                Wagon wags = new Wagon
                                {
                                    Nwag = Convert.ToInt32(vagnum),
                                    Ownerc = string.Empty,
                                    Gp = string.Empty,
                                    Tara = string.Empty
                                };
                                context.WagonDbSet.Add(wags);
                            }

                            mtrwgn.WagonNumberMatrix = vagnum;
                        }
                        mtr.MatrixWagons.Add(mtrwgn);

                    }
                    Matrix = mtr;
                    NotifyPropertyChanged("Matrix");
                    context.MatrixxDbSet.Add(mtr);
                    context.SaveChanges();
                }
            }
        }
        private void ViewAll(object param) {
            if ((bool)param)
            {

            }
        }

        private void SaveContext()
        {
            context.SaveChanges();
        }

        private void OpenFromDb()
        {
            
            OpenFromDbView openFromDb=new OpenFromDbView();
            Matrixx copy = Matrix.Copy();
            bool? result = openFromDb.ShowDialog();
            if (result.HasValue && (result == true))
            {
                if ((openFromDb.DataContext as OpenFromDbViewModel)?.SelectMatrixx != null)
                {
                    var qeryF= ((OpenFromDbViewModel) openFromDb.DataContext).SelectMatrixx;
                    Matrix = context.MatrixxDbSet.Find(qeryF.MatrixxId);
                }
                NotifyPropertyChanged("Matrix");
            }
            else
            {
                Matrix = copy;
                NotifyPropertyChanged("Matrix");
            }
           
        }

        private void edit(object param) {
            MessageBox.Show(param.ToString());
        }
    }
}
