using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


    }
    public class MatrixxWagonConfiguration : EntityTypeConfiguration<MatrixWagon>
    {
        public MatrixxWagonConfiguration()
        {
            this.ToTable("MatrixWagon", "dbo");
            this.HasKey(q => q.MatrixWagonId);
            #region 1


            //primary key
            /* this.HasKey(q =>
             new
             {
                 q.MatrixId,
                 q.WagonId
             });
             */
            /*   this.HasRequired(t => t.Wagon)
                   .WithMany(t => t.MatrixWagons)
                   .HasForeignKey(t => t.WagonId);
                   */
            #endregion
            this.HasRequired<Matrixx>(t => t.Matrixx)
               .WithMany(t => t.MatrixWagons)
               .HasForeignKey(t => t.MatrixId);

        }

    }
}
