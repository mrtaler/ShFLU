using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TicketSaleCore.Models;
using ShFLY.DataBase.Models;
using ShFLY.DataBase.DAL.Implemtaations;

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
