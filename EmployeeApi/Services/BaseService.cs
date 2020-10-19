using EmployeeApi.Models;
using EmployeeApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Services
{
    public class BaseService<T> : IService<T> where T : BaseEntity
    {
        private readonly IRepository<T> repository;

        public BaseService(IRepository<T> repository)
        {
            this.repository = repository;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return repository.GetAll();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return repository.GetById(id);
        }
        public async Task<T> InsertAsync(T entity)
        {
            if (entity == null) return null;

            return repository.Insert(entity);
        }
        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null || !repository.IsExists(entity.Id)) return null;

            return repository.Update(entity);
        }
        public async Task<T> DeleteAsync(int id)
        {
            if (!repository.IsExists(id)) return null;

            return repository.Delete(id);
        }
    }
}
