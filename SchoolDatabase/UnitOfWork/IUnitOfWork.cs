using Microsoft.EntityFrameworkCore;
using SchoolDatabase.Model.Entity;
using SchoolDatabase.Repository;

namespace SchoolDatabase.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : AbstractEntity;
        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : AbstractEntity;
        int SaveChanges();
        Task SaveChangesAsync();
        DbContext Context();
    }
}
