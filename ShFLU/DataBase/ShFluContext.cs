using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShFLU.DataBase.Table;

namespace ShFLU.DataBase
{
    public partial class ShFluContext : DbContext
    {
        public ShFluContext()
          //  : base("name=ContextGurpsModel")
          : base("offlineWagonDb")
        {
            //  Database.SetInitializer<ShFluContext>(new DbInit());
        }
        public virtual DbSet<SmgsNakl> SmgsNaklDbSet { get; set; }
        public virtual DbSet<WagInSmgs> WagInSmgsDbSet { get; set; }
        public virtual DbSet<Wagon> WagonDbSet { get; set; }

    }
}
