using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;

namespace ShFLY.DataBase.Models
{
    public class Wagon
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Wagon()
        {
        }
        public int IdWagon { get; set; }
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
            this.HasKey(p => p.IdWagon);//primary key


            //  this.HasOptional(s => s.WagInSmgs).WithRequired(p => p.Wagon);
        }
    }
}
