using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Facebook.Models;
using Facebook.Models.BaseEntity;

namespace Facebook.Models.Repository
{
    public class BaseRepository<T> : IDisposable where T:class,IBaseEntity
    {
        FacebookDbContext _db;
        private DbSet<T> Entity { get { return _db.Set<T>(); } }

        public BaseRepository()
        {
            _db = new FacebookDbContext();
        }

        public void AddModel(T model)
        {
            Entity.Add(model);
            _db.SaveChanges();
        }
        public void DeleteModel(int id)
        {
            var model = GetByID(id);
            Entity.Remove(model);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
            }
        }

        public ICollection<T> GetAllModel()
        {
            return Entity.ToList();
        }

        public T GetByID(int id)
        {
            var model = Entity.FirstOrDefault(k => k.ID == id);
            return model;
        }
        public IQueryable<E> Query<E>() where E : class
        {
            return _db.Set<E>();
        }
        public void Update(T model)
        {
            _db.Entry(model).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}