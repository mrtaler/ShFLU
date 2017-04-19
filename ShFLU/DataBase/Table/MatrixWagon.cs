using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ShFLU.MVVM;

namespace ShFLU.DataBase.Table
{
    public class MatrixWagon : ViewModelBase
    {
        private int? wagonNumberMatrix;
        private string speed;
        private string weight;

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
        public int? WagonNumberMatrix
        {
            get
            {
                return wagonNumberMatrix;
            }
            set
            {
                if (wagonNumberMatrix != value)
                {
                    wagonNumberMatrix = value;
                    NotifyPropertyChanged("WagonNumberMatrix");
                }
            }
        }
        /// <summary>
        /// weight Brutto
        /// </summary>
        public string Speed
        {
            get
            {
                return speed;
            }
            set
            {
                if (speed != value)
                {
                    speed = value;
                    NotifyPropertyChanged("Speed");
                }
            }
        }
        /// <summary>
        /// Netto
        /// </summary>
        public string Weight
        {
            get
            {
                return weight;
            }
            set
            {
                if (weight != value)
                {
                    weight = value;
                    NotifyPropertyChanged("Weight");
                }
            }
        }

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
