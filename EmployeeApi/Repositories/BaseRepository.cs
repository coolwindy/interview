using EmployeeApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DatabaseContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public BaseRepository(DatabaseContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public T GetById(int id)
        {
            return entities.SingleOrDefault(s => s.Id == id);
        }

        public T Insert(T entity)
        {
            entities.Add(entity);
            context.SaveChanges();

            return entity;
        }

        public T Update(T entity)
        {
            entities.Update(entity);
            context.SaveChanges();

            return entity;
        }

        public T Delete(int id)
        {
            T entity = entities.SingleOrDefault(s => s.Id == id);

            entities.Remove(entity);
            context.SaveChanges();

            return entity;
        }

        public bool IsExists(int id)
        {
            return entities.Any(e => e.Id == id);
        }
    }
}
