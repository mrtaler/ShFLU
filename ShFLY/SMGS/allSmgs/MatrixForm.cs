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
        public MatrixForm(SmgsNakl smgsNakl, MatrixTypes mt)
        {
            this.mt = mt;
           
           
            switch (mt)
            {
                case SMGS.MatrixTypes.Tara:
                    MatrixType = "ШФЛУ Тара";
                    MatrixDate = smgsNakl.Smgsdat.AddDays(2.75);
                    MatrixNum = "2"+smgsNakl.Smgs.ToString();
                    break;
                case SMGS.MatrixTypes.Brutto:
                    MatrixType = "ШФЛУ Брутто";
                    MatrixNum = "1" + smgsNakl.Smgs.ToString();
                    MatrixDate = smgsNakl.Smgsdat.AddDays(0.75);
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
        public MatrixTypes mt { get; set; }
        public string MatrixNum { get; set; }

        public DateTime MatrixDate { get; set; }

        public string MatrixType { get; set; }

        public List<MatrixItem> MatrixWagons { get; set; }


        public string SmgsToMatrix()
        {
            var outString = new StringBuilder();
            outString.AddNewLine($"                         М А Т Р И К С");
            outString.Append("\n");
            outString.AddNewLine($"     Установлен: БГПЗ");
            outString.Append("\n");
            outString.AddNewLine($"Дата и время взвешивания: {MatrixDate.ToString("dd-mm-yyyy hh:mm")}");
            outString.Append("\n");

            outString.AddNewLine($"Номер взвешивания: {MatrixDate.ToString("yy")}{MatrixNum};     Полный вес: {MatrixWagons.Sum(p => p.Weight)};");
            outString.Append("\n");
            var mode = mt == MatrixTypes.Brutto ? TrainMode.Тянет : TrainMode.Толкает;
            var modeNapr = mt == MatrixTypes.Brutto ? "Справа Налево" : "Слева Направо";

            outString.AddNewLine($"Положение локомотива: { mode}.");
            outString.AddNewLine($" Направление движения: { modeNapr}.");
            outString.Append("\n");

            var delimit = "_______________________________________________________";
            var type = mt == MatrixTypes.Brutto ? "Брутто, ШФЛУ" : "Тара, ШФЛУ";


            outString.AddNewLine(delimit);
            outString.AddNewLine(delimit);
            var tt = (double)type.Length / (double)2;
            var delCount = delimit.Length / (double)2;
            var delimitNew = delimit.Insert((int)(delCount - tt), type);
            outString.AddNewLine(delimitNew.Substring(0, delimitNew.Length - type.Count()));

            outString.AddNewLine(delimit);
            outString.AddNewLine(delimit);
            outString.Append("\n");
            outString.Append("-------------------------------------------------------");
            outString.Append("\n");

            foreach (var item in MatrixWagons)
            {
                outString.Append(item.WagonToMatrix());
            }
            outString.Append("-------------------------------------------------------");
            outString.Append("\n");

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
        public MatrixItem(WagInSmgs wagInSmgs, int wagNum, MatrixTypes mt)
        {
            WagonNumberPP = wagNum;
            WagonNumberMatrix = wagInSmgs.Wagon.Nwag;
            Speed = wagInSmgs.Weigher.VagonSpeed;
            switch (mt)
            {
                case MatrixTypes.Tara:
                    Weight = wagInSmgs.Weigher.WeigherTara;
                    break;
                case MatrixTypes.Brutto:
                    Weight = wagInSmgs.Weigher.WeigherBrutto;
                    break;
            }
        }

        public string WagonToMatrix()
        {
            string s1 = "           ";
            string s2 = "       ";
            string s3 = "                 ";
            string s4 = "       ";

            string ss1 = s1.Insert(
                WagonNumberPP.ToString()
                .Length < 2 ? s1.Length - 3 : s1.Length - 4
                , WagonNumberPP.ToString());

            if (WagonNumberPP.ToString().Length >= 2)
            {
                ss1= ss1.Substring(0, ss1.Length - 1);
            }

            var tt = Speed.ToString().Substring(0, Speed.ToString().Length - 1);
            if (tt.Length==2)
            {
                tt = tt.Insert(tt.Length, "0");
            }    
            string ss2 = s2.Insert(4, tt.Replace(".", ","));
            string ss3 = s3.Insert(0, WagonNumberMatrix.ToString());
            string ss4 = s4.Insert(4, Weight.ToString());

            return $":{ss1}:{ss2}:{ss3}:{ss4}:\n";
        }

    }

    public enum MatrixTypes
    {
        Tara,
        Brutto
    }

    public enum TrainMode
    {
        Тянет,
        Толкает
    }
}
