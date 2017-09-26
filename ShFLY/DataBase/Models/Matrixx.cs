using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;

namespace ShFLY.DataBase.Models
{
    public class Matrixx
    {
        public Matrixx()
        {
            MatrixDate = DateTime.Now;
            MatrixWagons = new HashSet<MatrixWagon>();
        }

        public int MatrixxId        {            get;            set;        }

        public int MatrixNum { get; set; }

        public DateTime MatrixDate { get; set; }

        public string MatrixType { get; set; }

        public virtual ICollection<MatrixWagon> MatrixWagons { get; set; }

        public Matrixx Copy()
        {
            return (Matrixx)MemberwiseClone();
        }
    }
    public class MatrixxConfiguration : EntityTypeConfiguration<Matrixx>
    {
        public MatrixxConfiguration()
        {
            ToTable("Matrix", "dbo");
            HasKey(p => p.MatrixxId);//primary key

            // this.Property(b => b.MatrixNum).IsUnique();

        }
    }
}
