using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShFLU.DataBase.Table
{
    public class Wagon
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Wagon()
        {
            this.WagInSmgses = new HashSet<WagInSmgs>();
        }
        public int Id { get; set; }
        /// <summary>
        /// Wagon number
        /// </summary>
        public int Nwag { get; set; }
        /// <summary>
        /// Cod of sobstv
        /// </summary>
        public string Ownerc { get; set; }
        /// <summary>
        /// gryz pod
        /// </summary>
        public string Gp { get; set; }
        /// <summary>
        /// Tara of Wagon
        /// </summary>
        public string Tara { get; set; }
        /// <summary>
        /// wagon include in smgs
        /// </summary>
        public virtual ICollection<WagInSmgs> WagInSmgses { get; set; }
      //  public virtual ICollection<MatrixWagon> MatrixWagons { get; set; }
    }

    public class WagonConfiguration : EntityTypeConfiguration<Wagon>
    {
        public WagonConfiguration()
        {
            this.ToTable("Wagon", "dbo");
            this.HasKey(p => p.Id);//primary key
            this.Property(b => b.Nwag).IsUnique();

        }
    }
}
