using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedismartsAPI.Model
{
    public interface IBaseDbRepo<T>
    {
        bool AddNew(T eentity);
        bool Delete(T entity);
        bool update(T entity, T incomingEntity);
       // void update(T entity, long entityId);
       // void Delete(long entityId);
        void AddNew(T[] entity);
        T GetSingle(Func<T, bool> expression);
        List<T> GetAll();
        List<T> GetAll(Func<T, bool> expression);
    }

    public class BaseDbRepo<T> : IBaseDbRepo<T> where T : class
    {
        private DbSet<T> dbSet;
        private StudentDbContext _ctx;
        public BaseDbRepo()
        {
            _ctx = new StudentDbContext(new DbContextOptions<StudentDbContext>());
            dbSet = _ctx.Set<T>();
        }
        public bool AddNew(T entity)
        {
            dbSet.Add(entity);
            return _ctx.SaveChanges() > 0;
        }

        public void AddNew(T[] entity)
        {
            foreach(var t in entity)
            {
                AddNew(t);
            }
        }

        public bool Delete(T entity)
        {
            dbSet.Remove(entity);
            return _ctx.SaveChanges() > 0;
        }

       

        public List<T> GetAll()
        {
            return dbSet.ToList();
        }

        public List<T> GetAll(Func<T, bool> expression)
        {
            return dbSet.Where(expression).ToList();
        }

        public T GetSingle(Func<T, bool> expression)
        {
            return dbSet.FirstOrDefault(expression);
        }

        public bool update(T entityToUpdate, T incomingEntity)
        {
            entityToUpdate = incomingEntity;
           // _ctx.Entry<T>(entityToUpdate).State = EntityState.Modified;
            dbSet.Update(entityToUpdate);

            return _ctx.SaveChanges() > 0;
        }

        
    }


    public interface IDbRepo<T>
    {
        bool SaveChanges();
        IBaseDbRepo<T> _repo { get; set; }
    }

    public class DbRepo<T> : IDbRepo<T> where T : class
    {
        private IBaseDbRepo<T> __repo;
        private StudentDbContext _ctx;
        public DbRepo()
        {
            __repo = new BaseDbRepo<T>();
            _ctx = new StudentDbContext(new DbContextOptions<StudentDbContext>());
        }
        public IBaseDbRepo<T> _repo { get => _repo; set => new BaseDbRepo<T>(); }

        public bool SaveChanges()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
