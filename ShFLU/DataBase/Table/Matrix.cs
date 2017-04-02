using System;
using System.Collections.ObjectModel;
using System.Data.Entity.ModelConfiguration;
using ShFLU.MVVM;

namespace ShFLU.DataBase.Table
{
    public class Matrixx : ViewModelBase
    {
        public Matrixx()
        {
            matrixDate=DateTime.Now;
            MatrixWagons = new ObservableCollection<MatrixWagon>();
        }

        private int matrixxId;

        public int MatrixxId
        {
            get
            {
                return matrixxId;
            }
            set
            {
                if (matrixxId != value)
                {
                    matrixxId = value;
                    NotifyPropertyChanged("MatrixxId");
                }
            }
        }

        private int matrixNum;
        public int MatrixNum
        {
            get
            {
                return matrixNum;
            }
            set
            {
                if (matrixNum != value)
                {
                    matrixNum = value;
                    NotifyPropertyChanged("MatrixNum");
                }
            }
        }

        private DateTime matrixDate;
        public DateTime MatrixDate
        {
            get
            {
                return matrixDate;
            }
            set
            {
                if (matrixDate != value)
                {
                    matrixDate = value;
                    NotifyPropertyChanged("MatrixDate");
                }
            }
        }

        private string matrixType;
        public string MatrixType
        {
            get
            {
                return matrixType;
            }
            set
            {
                if (matrixType != value)
                {
                    matrixType = value;
                    NotifyPropertyChanged("MatrixType");
                }
            }
        }

        public virtual ObservableCollection<MatrixWagon> MatrixWagons { get; set; }

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
