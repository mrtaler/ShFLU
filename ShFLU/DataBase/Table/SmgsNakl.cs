using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShFLU.DataBase.Table
{
    public class SmgsNakl
    {
        /// <summary>
        /// default constrictor
        /// </summary>
        public SmgsNakl()
        {
            this.WagInSmgses = new HashSet<WagInSmgs>();
        }
        public int Id { get; set; }
        /// <summary>
        /// SMGS Number
        /// </summary>
        public int Smgs { get; set; }
        /// <summary>
        /// Date Of SMGS
        /// </summary>
        public DateTime Smgsdat { get; set; }
        /// <summary>
        /// ETSNG Number
        /// </summary>
        public string Etsngn { get; set; }
        /// <summary>
        /// SMGS Wagon
        /// </summary>
        public virtual ICollection<WagInSmgs> WagInSmgses { get; set; }
    }
    public class SmgsNaklConfiguration : EntityTypeConfiguration<SmgsNakl>
    {
        public SmgsNaklConfiguration()
        {
            this.HasKey(p => p.Id);//primary key

            this.Property(b => b.Smgs).IsUnique();
        }
    }
}
