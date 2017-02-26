using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShFLU.DataBase.Table
{
    public class WagInSmgs
    {
        public Wagon WagonSmgs { get; set; }
        public SmgsNakl SmgsNaklWag { get; set; }
        public int Tarapr { get; set; }
        public int Weightb { get; set; }


    }
}
