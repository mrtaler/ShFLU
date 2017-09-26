using System;
using System.Collections.Generic;

using ShFLU.DataBase;

using ShFLY.DataBase.DAL.Interfaces;
using ShFLY.DataBase.Models;

using TicketSaleCore.Models;

namespace ShFLY.DataBase.DAL.Implemtaations
{
    class UnitOfWork : IUnitOfWork
    {
        private readonly ShFluContext context;
        private bool disposed;

        private Dictionary<string, object> repositories;

        public UnitOfWork(ShFluContext context)
        {
            this.context = context;
        }

        public UnitOfWork()
        {
            this.context = new ShFluContext();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }
            }

            this.disposed = true;
        }

     
        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        
        public IRepository<MatrixWagon> MatrixWagonRepository
        {
            get { return new GenericRepository<MatrixWagon>(this.context); }
        }
        public  IRepository<Matrixx> MatrixxRepository
        {
            get { return new GenericRepository<Matrixx>(this.context); }
        }
        public  IRepository<SmgsNakl> SmgsNaklRepository
        {
            get { return new GenericRepository<SmgsNakl>(this.context); }
        }
        public IRepository<WagInSmgs> WagInSmgsRepository
        {
            get { return new GenericRepository<WagInSmgs>(this.context); }
        }
        public IRepository<Wagon> WagonRepository
        {
            get { return new GenericRepository<Wagon>(this.context); }
        }

    }
}
