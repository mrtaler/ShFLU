using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace ShFLY.DataBase.Models
{
    public class Matrixx
    {
        public Matrixx()
        {
            this.MatrixDate = DateTime.Now;
            this.MatrixWagons = new HashSet<MatrixWagon>();
        }

        public int MatrixxId { get; set; }

        public int MatrixNum { get; set; }

        public DateTime MatrixDate { get; set; }

        public string MatrixType { get; set; }

        public virtual ICollection<MatrixWagon> MatrixWagons { get; set; }

        public Matrixx Copy()
        {
            return (Matrixx)this.MemberwiseClone();
        }
    }
    public class MatrixxConfiguration : EntityTypeConfiguration<Matrixx>
    {
        public MatrixxConfiguration()
        {
            this.ToTable("Matrix", "dbo");
            this.HasKey(p => p.MatrixxId);// primary key

            // this.Property(b => b.MatrixNum).IsUnique();
        }
    }
}
