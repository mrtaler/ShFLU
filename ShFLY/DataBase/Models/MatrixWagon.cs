using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;

namespace ShFLY.DataBase.Models
{
    public class MatrixWagon 
    {
        /// <summary>
        /// primary id
        /// </summary>
        public int MatrixWagonId { get; set; }
        /// <summary>
        /// number p/p
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

        public virtual WagInSmgs WagInSmgs { get; set; }

    }
    public class MatrixxWagonConfiguration : EntityTypeConfiguration<MatrixWagon>
    {
        public MatrixxWagonConfiguration()
        {
            ToTable("MatrixWagon", "dbo");
            HasKey(q => q.MatrixWagonId);

            HasRequired(t => t.Matrixx)
               .WithMany(t => t.MatrixWagons)
               .HasForeignKey(t => t.MatrixId);


            HasOptional(p => p.WagInSmgs).WithOptionalPrincipal(z => z.MatrixWagonBrutto);

            HasOptional(p => p.WagInSmgs).WithOptionalPrincipal(z => z.MatrixWagonTara);


        }

    }
}
