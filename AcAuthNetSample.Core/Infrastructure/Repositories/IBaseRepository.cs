using AcAuthNetSample.Core.Domain.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Infrastructure.Repositories {
    public interface IBaseRepository<T> where T : BaseEntity {

        Task<T> GetByIdAsync(int id);

        Task<bool> HasByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        void DeleteById(int id);

        Task<bool> DeletePhysicalBySqlAsync(int id);

        void DeleteAllAsync();

        void Update(T entity);

        Task<bool> UpdateAsync(T entity);

        void Add(T entity);

        Task<bool> AddAsync(T entity);

        Task SaveChangeAsync();
    }
}
