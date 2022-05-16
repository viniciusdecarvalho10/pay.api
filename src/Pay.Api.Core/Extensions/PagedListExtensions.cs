using System;
using System.Linq;
using System.Linq.Expressions;
using Pay.Api.Core.Enums;
using Pay.Api.Core.Models;

namespace Pay.Api.Core.Extensions
{
    public static class PagedListExtensions
    {
        public static PagedResult<TEntity> GetPaged<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, object>> predicateOrder, OrderTypeEnum orderType, int page, int pageSize) where TEntity : class
        {
            var result = new PagedResult<TEntity>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;

            result.Results = orderType == OrderTypeEnum.Desc ? query.OrderByDescending(predicateOrder).Skip(skip).Take(pageSize).ToList() : query.OrderBy(predicateOrder).Skip(skip).Take(pageSize).ToList();

            return result;
        }
    }
}