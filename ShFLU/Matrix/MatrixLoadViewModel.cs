using Microsoft.Win32;
using ShFLU.DataBase;
using ShFLU.DataBase.Table;
using ShFLU.MVVM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShFLU.Matrix
{
    public class MatrixLoadViewModel : ViewModelBase
    {
        private ShFluContext context = new ShFluContext();
        private string MatrixFilePatch;
        public Matrixx Matrix { get; set; }
        public ViewModelCommand LoadMatrixCommand { get; set; }

        public MatrixLoadViewModel()
        {
            Matrix = new Matrixx();
            LoadMatrixCommand = new ViewModelCommand(LoadMatrix, true);

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
                MatrixFilePatch = dlg.FileName;
                NotifyPropertyChanged("MatrixFilePatch");
               
                Encoding enc = Encoding.GetEncoding(1251);

                string[] stringFromFile = File.ReadAllLines(MatrixFilePatch, enc);
                StringBuilder strResult = new StringBuilder();
                foreach (var itemStringFromFile in stringFromFile)
                {
                    strResult.Append(itemStringFromFile);
                    strResult.Append('\n');
                }
                var date = Regex.Match(strResult.ToString(), @"(?<=Дата и время взвешивания: )(.*)");
                var dateee = Convert.ToDateTime(date.ToString());
                Matrix.MatrixDate = dateee;
                Matrix.MatrixNum = Regex.Match(strResult.ToString(), @"(?<=Номер взвешивания:)\W\d*(?=;)").ToString();
                Matrix.MatrixType = (string)param;

                var vagons = Regex.Matches(strResult.ToString(), @":.*:.*:.*:.*:");
                foreach (var item in vagons)
                {
                    var spr = item.ToString().Split(':');

                    MatrixWagon mtrwgn = new MatrixWagon();
                    mtrwgn.WagonNumberPP = Convert.ToInt32(spr[1]);
                    mtrwgn.Speed = spr[2];
                    mtrwgn.Weight = spr[4];

                    int vagnum = 0;
                    Wagon insertWag = null;
                    if (Regex.IsMatch(spr[3].ToString(), @"\d{8}"))
                    {
                        vagnum = Convert.ToInt32(spr[3]);
                        insertWag = context.WagonDbSet.FirstOrDefault(p => p.Nwag == vagnum);
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
                            mtrwgn.Wagon = context.WagonDbSet.Local.FirstOrDefault(p => p.Nwag == vagnum); 
                        }
                        else
                        {
                            mtrwgn.Wagon = context.WagonDbSet.Local.FirstOrDefault(p => p.Nwag == vagnum); 
                        }
                       
                    }

                    Matrix.MatrixWagons.Add(mtrwgn);
                    NotifyPropertyChanged("Matrix");
                }
                context.MatrixxDbSet.Add(Matrix);
                context.SaveChanges();

            }

            /* using (StreamReader ins = new StreamReader(MatrixFilePatch))
             {
                 string fileDataPerString = null;
                 while ((fileDataPerString = ins.ReadLine()) != null)
                 {

                 }


                 }


 */


        }

    }
}
