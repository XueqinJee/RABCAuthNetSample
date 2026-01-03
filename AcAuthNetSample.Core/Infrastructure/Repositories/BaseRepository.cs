using AcAuthNetSample.Core.Comments.Dtos;
using AcAuthNetSample.Core.Domain.Shared.Entities;
using AcAuthNetSample.Core.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AcAuthNetSample.Core.Infrastructure.Repositories {
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity {
        private readonly ApplicationDbContext _context;
        public DbSet<T> _dbSet { get; set; }

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public PageResult<T> GetPage(BasePageQuery query, Expression<Func<T, bool>>? express = null, Expression<Func<T, object>>? orderBy = null, bool isAscending = false)
        {
            return _dbSet.ToPageResult(query, express, orderBy, isAscending);
        }

        public async Task<PageResult<T>> GetPageAsync(BasePageQuery query, Expression<Func<T, bool>>? express = null, Expression<Func<T, object>>? orderBy = null, bool isAscending = false)
        {
            return await _dbSet.ToPageResultAsync(query, express, orderBy, isAscending);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FirstAsync(x => x.Id == id);
        }

        public async Task<bool> HasByIdAsync(int id)
        {
            return await _context.Set<T>().AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public void DeleteById(int id)
        {
            var entity = _dbSet.First(x => x.Id == id);
            _dbSet.Remove(entity);
        }

        public void DeleteAllAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task<bool> AddAsync(T entity)
        {
            _dbSet.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletePhysicalBySqlAsync(int id)
        {
            var tableName = _context.Model.FindEntityType(typeof(T))!.GetTableName();
            var sql = $"delete from {tableName} where id = @Id";
            return await _context.Database.ExecuteSqlRawAsync(sql, new SqlParameter("@Id", id)) > 0;
        }

        public IQueryable<T> GetQueryable()
        {
            return _dbSet as IQueryable<T>;
        }
    }
}
