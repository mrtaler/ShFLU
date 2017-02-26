using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShFLU.DataBase.Table
{
    public class Wagon
    {
        public int Id { get; set; }
        public int Nwag { get; set; }
        public int Ownerc { get; set; }
        public int Gp { get; set; }
        public int Tara { get; set; }
        public List<WagInSmgs> WagInSmgs { get; set; }

    }
}
