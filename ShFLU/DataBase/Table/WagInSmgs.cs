using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ShFLU.DataBase.Table
{
    public class WagInSmgs
    {
        // [Key, Column(Order = 0)]
        public int WagonSmgsId { get; set; }
        //  [Key, Column(Order = 1)]
        public int SmgsNaklWagId { get; set; }
        /// <summary>
        /// Wagon
        /// </summary>
        public virtual Wagon WagonSmgs { get; set; }
        /// <summary>
        /// SMGS
        /// </summary>
        public virtual SmgsNakl SmgsNaklWag { get; set; }
        /// <summary>
        /// Tara from SMGS
        /// </summary>
        public string Tarapr { get; set; }
        /// <summary>
        /// weight Brutto
        /// </summary>
        public string Weightb { get; set; }

    }

    public class WagInSmgsConfiguration : EntityTypeConfiguration<WagInSmgs>
    {
        public WagInSmgsConfiguration()
        {
            //primary key
            this.HasKey(q =>
            new
            {
                q.WagonSmgsId,
                q.SmgsNaklWagId
            });

            this.HasRequired(t => t.WagonSmgs)
                .WithMany(t => t.WagInSmgs)
                .HasForeignKey(t => t.WagonSmgsId);

            this.HasRequired(t => t.SmgsNaklWag)
               .WithMany(t => t.WagInSmgses)
               .HasForeignKey(t => t.SmgsNaklWagId);

        }

    }
}
