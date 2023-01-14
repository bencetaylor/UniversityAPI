using Microsoft.EntityFrameworkCore;
using SchoolDatabase.Context;
using SchoolDatabase.Model.Entity;

namespace SchoolDatabase.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : AbstractEntity
    {

        private readonly SchoolAPIDbContext _context;
        protected readonly DbSet<TEntity> DbSet;

        public GenericRepository(SchoolAPIDbContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public async Task<TEntity> GetById(int id)
        {
            return await DbSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Create(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            DbSet.Remove(entity);
        }

        public async Task DeleteSoft(int id)
        {
            var entity = await GetById(id);
            entity.Deleted = true;
            Update(entity);
        }
    }
}
