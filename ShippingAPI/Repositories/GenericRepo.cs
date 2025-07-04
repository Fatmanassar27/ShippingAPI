using Microsoft.EntityFrameworkCore;
using ShippingAPI.Data;
using System.Linq.Expressions;

namespace ShippingAPI.Repositories
{
    public class GenericRepo<T> where T : class
    {
        public ShippingContext db;
        public GenericRepo(ShippingContext db) {
            this.db = db;
        }

        public List<T> getAll()
        {
            return db.Set<T>().ToList();

        }
        public T getById(int id)
        {
            return db.Set<T>().Find(id);
        }
        public void add(T item)
        {
            db.Set<T>().Add(item);
        }
        public void edit(T item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public void delete(int id)
        {
            T e = db.Set<T>().Find(id);
            db.Set<T>().Remove(e);
        }
        public async Task AddAsync(T item)
        {
            await db.Set<T>().AddAsync(item);
        }
        public void Edit(T item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await db.Set<T>().FindAsync(id);
            if (entity == null) return false;

            db.Set<T>().Remove(entity);
            return true;
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await db.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await db.Set<T>().FindAsync(id);
        }
        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await db.Set<T>().Where(predicate).ToListAsync();
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await db.Set<T>().AnyAsync(predicate);
        }
        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await db.Set<T>().FirstOrDefaultAsync(predicate);
        }
        public async Task<List<T>> WhereAsync(Expression<Func<T, bool>> predicate)
        {
            return await db.Set<T>().Where(predicate).ToListAsync();
        }
        public void Delete(int id)
        {
            var entity = db.Set<T>().Find(id);
            if (entity != null)
                db.Set<T>().Remove(entity);
        }
    }
}
