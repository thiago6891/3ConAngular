using Microsoft.EntityFrameworkCore;
using PortalRSApi.Data;
using PortalRSApi.Data.Interfaces;
using System.Linq;

namespace PortalRSApi.Data.Services
{
    /// <summary>
    /// Classe de repositório genérico
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _db;

        public Repository(ApplicationDbContext ctx)
        {
            _db = ctx;
        }

        public void Create(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        public void Delete(int id)
        {
            var entity = _db.Set<T>().Find(id);
            _db.Set<T>().Remove(entity);
        }

        public void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        public IQueryable<T> Query()
        {
            return _db.Set<T>().AsNoTracking();
        }

        public T Obter(int id)
        {
            return _db.Set<T>().Find(id);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public int Count()
        {
            return _db.Set<T>().Count();
        }

        public void Update(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
        }
    }
}