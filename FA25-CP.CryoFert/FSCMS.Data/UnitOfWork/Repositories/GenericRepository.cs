using FSCMS.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Data.UnitOfWork.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private static AppDbContext Context;
        private static DbSet<T> Table { get; set; }

        public GenericRepository(AppDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));  // Thêm kiểm tra null
            Table = Context.Set<T>();
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().AsQueryable().Where(predicate).ToList();
        }
        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return
                await Table.SingleOrDefaultAsync(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().AnyAsync(predicate);
        }

        public IQueryable<T> FindAll(Func<T, bool> predicate)
        {
            return Table.Where(predicate).AsQueryable();
        }

        //public DbSet<T> GetAll()
        //{
        //    return Table;
        //}

        public IQueryable<T> GetAll()
        {
            return Context.Set<T>().AsQueryable(); // Trả về IQueryable để hỗ trợ Include()
        }
        public async Task<T?> GetByIdGuid(Guid Id)
        {
            return await Table.FindAsync(Id);
        }

        public async Task<T?> GetById(int Id)
        {
            return await Table.FindAsync(Id);
        }

        public async Task HardDeleteGuid(Guid key)
        {
            var rs = await GetByIdGuid(key);
            Table.Remove(rs);
        }
        public void Remove(T entity)
        {
            Table.Remove(entity);
        }
        public async Task HardDelete(int key)
        {
            var rs = await GetById(key);
            Table.Remove(rs);
        }

        public void Insert(T entity)
        {
            Table.Add(entity);
        }

        public async Task UpdateGuid(T entity, Guid Id)
        {
            var existEntity = await GetByIdGuid(Id);
            if (existEntity != null)
            {
                Context.Entry(existEntity).CurrentValues.SetValues(entity);
                Table.Update(existEntity);
            }
        }

        public async Task Update(T entity, int Id)
        {
            var existEntity = await GetById(Id);
            if (existEntity != null)
            {
                Context.Entry(existEntity).CurrentValues.SetValues(entity);
                Table.Update(existEntity);
            }
        }


        public void UpdateRange(IQueryable<T> entities)
        {
            Table.UpdateRange(entities);
        }

        public void DeleteRange(IQueryable<T> entities)
        {
            Table.RemoveRange(entities);
        }

        public void InsertRange(IQueryable<T> entities)
        {
            Table.AddRange(entities);
        }

        public EntityEntry<T> Delete(T entity)
        {
            return Table.Remove(entity);
        }

        //async
        public async Task InsertAsync(T entity)
        {
            await Table.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public async Task InsertRangeAsync(IQueryable<T> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return await Table.Where(predicate).ToListAsync();
        }

        public IQueryable<T> GetAllApart()
        {
            return Table.Take(100);
        }
        public async Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken = default)
        {
            return await Context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task UpdateDetached(T entity)
        {
            Table.Update(entity);
        }

        public async Task DetachEntity(T entity)
        {
            Context.Entry(entity).State = EntityState.Detached;
        }

        public IQueryable<T> AsNoTracking()
        {
            return Table.AsNoTracking();
        }

        public IQueryable<T> AsQueryable()
        {
            return Context.Set<T>().AsQueryable();
        }

        public IQueryable<T> AsQueryable(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().AsQueryable().Where(predicate);
        }

        public IQueryable<TResult?> ObjectMapper<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = Table;
            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            return query.AsNoTracking().Select(selector);
        }

        public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = Table;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public async Task UpdateAsync(T entity)
        {
            Table.Attach(entity); // Đính kèm entity vào DbContext nếu chưa có
            Context.Entry(entity).State = EntityState.Modified; // Đánh dấu entity đã bị sửa đổi
            await Context.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Table.Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;
            }
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                Table.Attach(entity);
            }
            Table.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> FindAllRangeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table.Where(predicate);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.ToListAsync();
        }
        public IQueryable<T> GetQueryable()
        {
            return Context.Set<T>();
        }

    }
}
