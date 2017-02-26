using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShFLU.DataBase.Table
{
    public class SmgsNakl
    {
        public int Id { get; set; }
        public int Smgs { get; set; }
        public DateTime Smgsdat { get; set; }
        public string Etsngn { get; set; }
        public List<WagInSmgs> WagInSmgses { get; set; }
    }
}
