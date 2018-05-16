﻿using System.Data.Entity;

using ShFLY.DataBase.Models;
using ShFLY.SMGS;

namespace ShFLU.DataBase
{
    public class ShFluContext : DbContext
    {
        public ShFluContext()

            // : base("name=ContextGurpsModel")
            : base("offlineWagonDb")
        {
            // this.Configuration.LazyLoadingEnabled = false; 
        }

        public virtual DbSet<SmgsNakl> SmgsNaklDbSet { get; set; }
        public virtual DbSet<WagInSmgs> WagInSmgsDbSet { get; set; }
        public virtual DbSet<Wagon> WagonDbSet { get; set; }

        public virtual DbSet<MatrixWagon> MatrixWagonDbSet { get; set; }
        public virtual DbSet<Matrixx> MatrixxDbSet { get; set; }


        public virtual DbSet<Weigher> Weighers{ get; set; }
    // public virtual DbSet<TaraBruttoFromMatrix> TaraBruttoFromMatrixdDbSet { get; set; }
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            modelBuilder.Configurations.Add(new SmgsNaklConfiguration());
            modelBuilder.Configurations.Add(new WagonConfiguration());
            modelBuilder.Configurations.Add(new WagInSmgsConfiguration());


            modelBuilder.Configurations.Add(new MatrixxWagonConfiguration());
            modelBuilder.Configurations.Add(new MatrixxConfiguration());

            // modelBuilder.Configurations.Add(new TaraBruttoFromMatrixConfiguration());
        }
    }

}
