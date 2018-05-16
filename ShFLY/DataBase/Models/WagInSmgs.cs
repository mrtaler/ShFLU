namespace ShFLY.DataBase.Models
{
    using ShFLY.SMGS;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// The wag in smgs.
    /// </summary>
    public class WagInSmgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WagInSmgs"/> class.
        /// </summary>
        public WagInSmgs()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WagInSmgs"/> class.
        /// </summary>
        /// <param name="wagInSmgs">
        /// The wag in smgs.
        /// </param>
        public WagInSmgs(WagInSmgs wagInSmgs)
        {
            this.WagonSmgsId = wagInSmgs.WagonSmgsId;
            this.Wagon = wagInSmgs.Wagon;
            this.WgaonId = wagInSmgs.WgaonId;
            this.SmgsNakl = wagInSmgs.SmgsNakl;
            this.SmgsNaklId = wagInSmgs.SmgsNaklId;
            this.Tarapr = wagInSmgs.Tarapr;
            this.Weightb = wagInSmgs.Weightb;
            this.Weight = wagInSmgs.Weight;
            this.MatrixWagonTara = wagInSmgs.MatrixWagonTara;
            this.MatrixWagonBrutto = wagInSmgs.MatrixWagonBrutto;
        }

        /// <summary>
        /// Gets or sets the wagon smgs id.
        /// </summary>
        public int WagonSmgsId { get; set; }

        /// <summary>
        /// Gets or sets the wagon.
        /// </summary>
        public virtual Wagon Wagon { get; set; }

        /// <summary>
        /// Gets or sets the wgaon id.
        /// </summary>
        public int WgaonId { get; set; }

        /// <summary>
        /// Gets or sets the smgs nakl.
        /// </summary>
        public virtual SmgsNakl SmgsNakl { get; set; }

        /// <summary>
        /// Gets or sets the smgs nakl id.
        /// </summary>
        public int SmgsNaklId { get; set; }

        /// <summary>
        /// Gets or sets the tarapr.
        /// </summary>
        public string Tarapr { get; set; }

        /// <summary>
        /// Gets or sets the weightb.
        /// </summary>
        public string Weightb { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// Gets or sets the matrix wagon tara.
        /// </summary>
        public virtual MatrixWagon MatrixWagonTara { get; set; }

        /// <summary>
        /// Gets or sets the matrix wagon brutto.
        /// </summary>
        public virtual MatrixWagon MatrixWagonBrutto { get; set; }
        public bool IsWeigherCalc { get; set; }
        public virtual Weigher Weigher { get; set; }
    }

    /// <summary>
    /// The wag in smgs configuration.
    /// </summary>
    public class WagInSmgsConfiguration : EntityTypeConfiguration<WagInSmgs>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WagInSmgsConfiguration"/> class.
        /// </summary>
        public WagInSmgsConfiguration()
        {
            this.ToTable("WagInSmgs", "dbo");

            // primary key
            this.HasKey(p => p.WagonSmgsId);

            // Config one-to-many for SMGSNAKL
            this.HasRequired(t => t.SmgsNakl).WithMany(t => t.WagInSmgses).HasForeignKey(t => t.SmgsNaklId);

            // config one-to-many Wagon
            this.HasRequired(t => t.Wagon).WithMany(t => t.WagInSmgses).HasForeignKey(x => x.WgaonId);

            this.HasOptional(o => o.Weigher)
               .WithRequired(r => r.wagInSmgs);
        }
    }
}
