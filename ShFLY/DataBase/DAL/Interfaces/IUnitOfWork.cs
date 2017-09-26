using System;

using ShFLY.DataBase.Models;

using TicketSaleCore.Models;

namespace ShFLY.DataBase.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();

        IRepository<MatrixWagon> MatrixWagonRepository { get; }
        IRepository<Matrixx> MatrixxRepository { get; }
        IRepository<SmgsNakl> SmgsNaklRepository { get; }
        IRepository<WagInSmgs> WagInSmgsRepository { get; }
        IRepository<Wagon> WagonRepository { get; }
              
    }
}
