using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ShFLU.DataBase.Table
{
    public class WagInSmgs
    {
        public int WagonSmgsId { get; set; }
        public int SmgsNaklWagId { get; set; }
        /// <summary>
        /// Wagon
        /// </summary>
        public virtual Wagon Wagon { get; set; }
        /// <summary>
        /// SMGS
        /// </summary>
        public virtual SmgsNakl SmgsNakl { get; set; }
        
        
        
        
        /// <summary>
        /// Tara from SMGS
        /// </summary>
        public string Tarapr { get; set; }
        /// <summary>
        /// weight Brutto
        /// </summary>
        public string Weightb { get; set; }
        /// <summary>
        /// Netto
        /// </summary>
        public string Weight { get; set; }



    }

    public class WagInSmgsConfiguration : EntityTypeConfiguration<WagInSmgs>
    {
        public WagInSmgsConfiguration()
        {
            this.ToTable("WagInSmgs", "dbo");
            //primary key
            this.HasKey(q =>
            new
            {
                q.WagonSmgsId,
                q.SmgsNaklWagId
            });

            this.HasRequired(t => t.Wagon)
                .WithMany(t => t.WagInSmgses)
                .HasForeignKey(t => t.WagonSmgsId);

            this.HasRequired(t => t.SmgsNakl)
               .WithMany(t => t.WagInSmgses)
               .HasForeignKey(t => t.SmgsNaklWagId);

        }

    }
}
