
namespace Domain.Model
{
    public class PagedList<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<T> Data { get; set; }

        public PagedList(IEnumerable<T> items, int totalCount, int currentPage, int pageSize)
        {
            Page = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            Data = items;
        }

    }
}
