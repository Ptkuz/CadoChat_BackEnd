namespace CadoChat.DAL.Entity.BaseUnitOfWork
{
    public interface IUnifOfWork : IDisposable
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task<int> SaveChangesAsync();
    }
}
