using AcAuthNetSample.Core.Comments.Dtos;
using AcAuthNetSample.Core.Domain.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AcAuthNetSample.Core.Infrastructure.Repositories {
    public static class PaginationExtensions {

        public static PageResult<T> ToPageResult<T>(this IQueryable<T> query, BasePageQuery pageQuery, 
            Expression<Func<T, bool>>? express = null,
            Expression<Func<T, object>>? orderBy = null,
            bool isAscending = false) where T : BaseEntity
        {
            ArgumentNullException.ThrowIfNullOrEmpty(nameof(pageQuery));
            if (express != null)
            {
                query = query.Where(express);
            }

            if (orderBy != null)
            {
                if (isAscending)
                {
                    query = query.OrderByDescending(orderBy);
                }
                else
                {
                    query = query.OrderBy(orderBy);
                }
            }
            else
            {
                query = query.OrderBy(x => x.Id);
            }

                var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageQuery.PageSize);
            var data = query.Skip((pageQuery.PageIndex - 1) * pageQuery.PageSize)
                .Take(pageQuery.PageSize)
                .ToList();

            return new PageResult<T>
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                HasNextPage = pageQuery.PageIndex < totalPages,
                HasPreviousPage = pageQuery.PageIndex > 1,
                PageIndex = pageQuery.PageIndex,
                PageSize = pageQuery.PageSize,
                Data = data
            };
        }

        public static async Task<PageResult<T>> ToPageResultAsync<T>(this IQueryable<T> query,
            BasePageQuery pageQuery, 
            Expression<Func<T, bool>>? express = null, 
            Expression<Func<T, object>>? orderBy = null,
            bool isAscending = false) where T : BaseEntity
        {
            ArgumentNullException.ThrowIfNullOrEmpty(nameof(pageQuery));
            if (express != null)
            {
                query = query.Where(express);
            }

            if(orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }
            else
            {
                query = query.OrderBy(x => x.Id);
            }

                var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageQuery.PageSize);
            var data = await query.Skip((pageQuery.PageIndex - 1) * pageQuery.PageSize)
                .Take(pageQuery.PageSize)
                .ToListAsync();

            return new PageResult<T>
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                HasNextPage = pageQuery.PageIndex < totalPages,
                HasPreviousPage = pageQuery.PageIndex > 1,
                PageIndex = pageQuery.PageIndex,
                PageSize = pageQuery.PageSize,
                Data = data
            };
        }
    }
}
