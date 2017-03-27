using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShFLU.DataBase.Table
{
    public class Matrixx
    {
        public Matrixx()
        {
            this.MatrixWagons = new HashSet<MatrixWagon>();
        }
        public int MatrixxId { get; set; }
        public int MatrixNum { get; set; }
        public DateTime MatrixDate { get; set; }
        public String MatrixType { get; set; }
     
        public virtual ICollection<MatrixWagon> MatrixWagons { get; set; }
    }
    public class MatrixxConfiguration : EntityTypeConfiguration<Matrixx>
    {
        public MatrixxConfiguration()
        {
            this.ToTable("Matrix", "dbo");
            this.HasKey(p => p.MatrixxId);//primary key

           // this.Property(b => b.MatrixNum).IsUnique();

        }
    }
}
