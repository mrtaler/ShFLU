using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;

namespace ShFLY.DataBase.Models
{
    public class WagInSmgs
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public int WagonSmgsId { get; set; }
        /// <summary>
        /// Wagon
        /// </summary>
        public virtual Wagon Wagon { get; set; }
        public int WgaonId { get; set; }


        /// <summary>
        /// SMGS
        /// </summary>
        public virtual SmgsNakl SmgsNakl { get; set; }
        public int SmgsNaklId { get; set; }

        //  public virtual TaraBruttoFromMatrix TaraBruttoFromMatrix { get; set; }


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

        public virtual MatrixWagon MatrixWagonTara { get; set; }
        public virtual MatrixWagon MatrixWagonBrutto { get; set; }

    }

    public class WagInSmgsConfiguration : EntityTypeConfiguration<WagInSmgs>
    {
        public WagInSmgsConfiguration()
        {
            this.ToTable("WagInSmgs", "dbo");
            //primary key
            this.HasKey(p => p.WagonSmgsId);
            //Config one-to-many for SMGSNAKL
            this.HasRequired(t => t.SmgsNakl)
                .WithMany(t => t.WagInSmgses)
                .HasForeignKey(t => t.SmgsNaklId);

            //config one-to-many Wagon
            this.HasRequired(t => t.Wagon)
                .WithMany(t => t.WagInSmgses)
                .HasForeignKey(x => x.WgaonId);



        }

    }
}
