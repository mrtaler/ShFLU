using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShFLU.DataBase.Table
{
   /* public class TaraBruttoFromMatrix
    {
        public int IdTaraBruttoFromMatrix { get; set; }

      /*  public virtual MatrixWagon MatrixWagonTara { get; set; }
        public virtual MatrixWagon MatrixWagonBrutto { get; set; }
        public virtual WagInSmgs WagInSmgs { get; set; }

    }
    public class TaraBruttoFromMatrixConfiguration : EntityTypeConfiguration<TaraBruttoFromMatrix>
    {
        public TaraBruttoFromMatrixConfiguration()
        {
            this.ToTable("TaraBruttoFromMatrix", "dbo");
            this.HasKey(p => p.IdTaraBruttoFromMatrix);//primary key

            //config one-to-one WagInSmgs
         /*   HasRequired(t => t.WagInSmgs)
                .WithOptional(c => c.TaraBruttoFromMatrix);

          /*  HasRequired(p=>p.MatrixWagonBrutto)
                .WithOptional(e => e.TaraBruttoFromMatrix);

            HasRequired(p => p.MatrixWagonTara)
                .WithOptional(e => e.TaraBruttoFromMatrix);

   
        }
    }*/
}
