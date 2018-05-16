using ShFLY.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShFLY.SMGS
{
    public class MatrixForm
    {
        public MatrixForm(SmgsNakl smgsNakl, MatrixType mt)
        {
            MatrixNum = smgsNakl.Smgs;
            MatrixDate = smgsNakl.Smgsdat;
            switch (mt)
            {
                case SMGS.MatrixType.Tara:
                    MatrixType = "ШФЛУ Тара";
                    break;
                case SMGS.MatrixType.Brutto:
                    MatrixType = "ШФЛУ Брутто";
                    break;
            }

            MatrixWagons = new List<MatrixItem>();
            int wagNum = 1;
            foreach (WagInSmgs item in smgsNakl.WagInSmgses)
            {
                var vagMatrItem = new MatrixItem(item, wagNum++, mt);
                MatrixWagons.Add(vagMatrItem);
            }

        }

        public int MatrixNum { get; set; }

        public DateTime MatrixDate { get; set; }

        public string MatrixType { get; set; }

        public List<MatrixItem> MatrixWagons { get; set; }


        public string SmgsToMatrix()
        {
            var outString = new StringBuilder();
            outString.AddNewLine($"{MatrixNum}");
            outString.AddNewLine($"{MatrixDate};                 {MatrixDate}");

            outString.AddNewLine($"------------------------------------------");
            outString.AddNewLine($"---------------{MatrixType}---------------");
            foreach (var item in MatrixWagons)
            {
                outString.Append(item.WagonToMatrix());
            }
            return outString.ToString();
        }


    }

    public static class SbExtention
    {
        public static StringBuilder AddNewLine(this StringBuilder sb, string text)
        {
            sb.Append(text);
            sb.Append("\n");
            return sb;
        }
    }
    public class MatrixItem
    {

        public int WagonNumberPP { get; set; }
        public int? WagonNumberMatrix { get; set; }
        public decimal Speed { get; set; }
        public decimal Weight { get; set; }
        public MatrixItem(WagInSmgs wagInSmgs, int wagNum, MatrixType mt)
        {
            WagonNumberPP = wagNum;
            WagonNumberMatrix = wagInSmgs.Wagon.Nwag;
            Speed = wagInSmgs.Weigher.VagonSpeed;
            switch (mt)
            {
                case MatrixType.Tara:
                    Weight = wagInSmgs.Weigher.WeigherTara;
                    break;
                case MatrixType.Brutto:
                    Weight = wagInSmgs.Weigher.WeigherBrutto;
                    break;
            }
        }

        public string WagonToMatrix()
        {
            string s1 = "            ";
            string s2 = "            ";
            string s3 = "            ";
            string s4 = "            ";

            string ss1 = s1.Insert(1, WagonNumberPP.ToString());
            string ss2 = s2.Insert(1, WagonNumberMatrix.ToString());
            string ss3 = s3.Insert(1, Speed.ToString());
            string ss4 = s4.Insert(1, Weight.ToString());

            return $";{ss1};{ss2};{ss3};{ss4};\n";
        }

    }

    public enum MatrixType
    {
        Tara,
        Brutto
    }
}
