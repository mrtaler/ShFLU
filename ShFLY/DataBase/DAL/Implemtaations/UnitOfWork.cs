using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShFLY.DataBase.DAL.Interfaces;
using System.Data;
using TicketSaleCore.Models;
using ShFLY.DataBase.Models;
using ShFLU.DataBase;

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
            context = new ShFluContext();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

     
        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        
        public IRepository<MatrixWagon> MatrixWagonRepository
        {
            get { return new GenericRepository<MatrixWagon>(context); }
        }
        public  IRepository<Matrixx> MatrixxRepository
        {
            get { return new GenericRepository<Matrixx>(context); }
        }
        public  IRepository< SmgsNakl> SmgsNaklRepository
        {
            get { return new GenericRepository<SmgsNakl>(context); }
        }
        public IRepository<WagInSmgs> WagInSmgsRepository
        {
            get { return new GenericRepository<WagInSmgs>(context); }
        }
        public IRepository<Wagon> WagonRepository
        {
            get { return new GenericRepository<Wagon>(context); }
        }

    }
}
