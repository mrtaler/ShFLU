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
        public int SmgsId { get; set; }
        /// <summary>
        /// SMGS Number
        /// </summary>
        public int Smgs { get; set; }
        /// <summary>
        /// Date Of SMGS
        /// </summary>
        public DateTime Smgsdat { get; set; }
        /// <summary>
        /// GNG number
        /// </summary>
        public string gngc { get; set; }
        /// <summary>
        /// GNG Text
        /// </summary>
        public string gngn { get; set; }
        /// <summary>
        /// ETSNG number
        /// </summary>
        public string etsngc { get; set; }
        /// <summary>
        /// ETSNG Text
        /// </summary>
        public string Etsngn { get; set; }
        /// <summary>
        /// Mass of netto
        /// </summary>
        public string mnet { get; set; }
        /// <summary>
        /// Mass of Brutto
        /// </summary>
        public string mbrt { get; set; }
        /// <summary>
        /// SMGS Wagon
        /// </summary>
        public virtual ICollection<WagInSmgs> WagInSmgses { get; set; }
    }
    public class SmgsNaklConfiguration : EntityTypeConfiguration<SmgsNakl>
    {
        public SmgsNaklConfiguration()
        {
            this.ToTable("SmgsNakl", "dbo");
            this.HasKey(p => p.SmgsId);//primary key
        }
    }
}
