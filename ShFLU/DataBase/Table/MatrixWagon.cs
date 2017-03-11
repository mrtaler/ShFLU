using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShFLU.DataBase.Table
{
    public class MatrixWagon
    {
        public int MatrixId { get; set; }
        public int WagonId { get; set; }
        /// <summary>
        /// Wagon
        /// </summary>
        public virtual Wagon Wagon { get; set; }
        /// <summary>
        /// SMGS
        /// </summary>
        public virtual Matrixx Matrixx { get; set; }


        
        /// <summary>
        /// Tara from SMGS
        /// </summary>
        public int WagonNumberPP { get; set; }
        /// <summary>
        /// weight Brutto
        /// </summary>
        public string Speed { get; set; }
        /// <summary>
        /// Netto
        /// </summary>
        public string Weight { get; set; }

    }
    public class MatrixxWagonConfiguration : EntityTypeConfiguration<MatrixWagon>
    {
        public MatrixxWagonConfiguration()
        {
            this.ToTable("MatrixWagon", "dbo");
            //primary key
            this.HasKey(q =>
            new
            {
                q.MatrixId,
                q.WagonId
            });

            this.HasRequired(t => t.Wagon)
                .WithMany(t => t.MatrixWagons)
                .HasForeignKey(t => t.WagonId);

            this.HasRequired(t => t.Matrixx)
               .WithMany(t => t.MatrixWagons)
               .HasForeignKey(t => t.MatrixId);

        }

    }
}
