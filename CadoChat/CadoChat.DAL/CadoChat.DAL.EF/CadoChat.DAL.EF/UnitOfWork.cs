using CadoChat.DAL.Entity.BaseUnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.DAL.EF
{
    public class UnitOfWork : IUnifOfWork
    {

        protected readonly DbContext _context;
        protected IDbContextTransaction? transaction;

        protected UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (transaction == null) 
                throw new InvalidOperationException("Транзакция не была начата.");

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            await transaction.DisposeAsync();
            transaction = null;
        }

        public async Task RollbackAsync()
        {
            if (transaction == null) 
                throw new InvalidOperationException("Транзакция не была начата.");

            await transaction.RollbackAsync();
            await transaction.DisposeAsync();
            transaction = null;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            transaction?.Dispose();
            _context.Dispose();
        }
    }
}
