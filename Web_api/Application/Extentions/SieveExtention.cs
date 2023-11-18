using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace Application.Extentions
{
    public static class SieveExtensions
    {
        public static async Task<PagedList<T>> GetPagedAsync<T>(this ISieveProcessor sieveProcessor,
            IQueryable<T> query, SieveOptions sieveOptions, SieveModel sieveModel,
            CancellationToken cancellationToken
            ) where T : class
        {
            var (pagedQuery, currentPage, pageSize, totalRowCount) = await GetPagedResultAsync(sieveProcessor, query, sieveOptions, sieveModel);

            var records = await pagedQuery.ToListAsync(cancellationToken);
            var result = new PagedList<T>(records, totalRowCount, currentPage, pageSize);

            return result;
        }

        private static async Task<(IQueryable<T> pagedQuery, int currentPage, int pageSize, int totalRowCount)> GetPagedResultAsync<T>(ISieveProcessor sieveProcessor,
            IQueryable<T> query, SieveOptions sieveOptions,
            SieveModel sieveModel) where T : class
        {
            var page = sieveModel?.Page == null || sieveModel.Page < 1 ? 1 : sieveModel.Page.Value;

            var pageSize = sieveModel?.PageSize ?? sieveOptions.DefaultPageSize;
            if (pageSize > sieveOptions.MaxPageSize)
            {
                pageSize = sieveOptions.MaxPageSize;
            }

            if (sieveModel != null)
            {
                query = sieveProcessor.Apply(sieveModel, query, applyPagination: false);
            }

            var rowCount = await query.CountAsync();

            var skip = (page - 1) * pageSize;
            var pagedQuery = query.Skip(skip).Take(pageSize);

            return (pagedQuery, page, pageSize, rowCount);
        }

        public static SievePropertyMapper.PropertyFluentApi<T> CanSortAndFilterByName<T>(this SievePropertyMapper.PropertyFluentApi<T> property, string name) where T : class
        {
            property
                .CanFilter()
                .CanSort()
                .HasName(name);
            return property;
        }
    }
}
