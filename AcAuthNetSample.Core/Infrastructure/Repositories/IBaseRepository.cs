using AcAuthNetSample.Core.Comments.Dtos;
using AcAuthNetSample.Core.Domain.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AcAuthNetSample.Core.Infrastructure.Repositories {
    public interface IBaseRepository<T> where T : BaseEntity {

        PageResult<T> GetPage(BasePageQuery query, Expression<Func<T, bool>>? express = null, Expression<Func<T, object>>? orderBy = null, bool isAscending = false);
        Task<PageResult<T>> GetPageAsync(BasePageQuery query, Expression<Func<T, bool>>? express = null, Expression<Func<T, object>>? orderBy = null, bool isAscending = false);

        IQueryable<T> GetQueryable();

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
