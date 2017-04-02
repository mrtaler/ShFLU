using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShFLU.DataBase;
using ShFLU.DataBase.Table;

namespace ShFLU.Matrix.OpenFromDB
{
    public class OpenFromDbViewModel
    {

        private ShFluContext context;
        public List<Matrixx> MatrixList { get; set; }
        public Matrixx SelectMatrixx { get; set; }
        public OpenFromDbViewModel()
        {
            context=new ShFluContext();
            MatrixList = new List<Matrixx>(context.MatrixxDbSet);
        }
    }
}
