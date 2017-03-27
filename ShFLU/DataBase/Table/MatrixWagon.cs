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
        public int MatrixWagonId { get; set; }
        /// <summary>
        /// Tara from SMGS
        /// </summary>
        public int WagonNumberPP { get; set; }
        /// <summary>
        /// Wagon number in matrix
        /// </summary>
        public int? WagonNumberMatrix { get; set; }
        /// <summary>
        /// weight Brutto
        /// </summary>
        public string Speed { get; set; }
        /// <summary>
        /// Netto
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// SMGS
        /// </summary>
        public virtual Matrixx Matrixx { get; set; }
        public int MatrixId { get; set; }

    }
    public class MatrixxWagonConfiguration : EntityTypeConfiguration<MatrixWagon>
    {
        public MatrixxWagonConfiguration()
        {
            this.ToTable("MatrixWagon", "dbo");
            this.HasKey(q => q.MatrixWagonId);
            #region 1


            //primary key
            /* this.HasKey(q =>
             new
             {
                 q.MatrixId,
                 q.WagonId
             });
             */
            /*   this.HasRequired(t => t.Wagon)
                   .WithMany(t => t.MatrixWagons)
                   .HasForeignKey(t => t.WagonId);
                   */
            #endregion
            this.HasRequired<Matrixx>(t => t.Matrixx)
               .WithMany(t => t.MatrixWagons)
               .HasForeignKey(t => t.MatrixId);

        }

    }
}
