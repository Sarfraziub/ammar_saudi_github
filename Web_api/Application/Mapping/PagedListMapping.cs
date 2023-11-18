using AutoMapper;
using Domain.Model;

namespace Application.Mapping
{
    public class PagedListMapping : Profile
    {

        public PagedListMapping()
        {
            CreateMap(typeof(PagedList<>), typeof(PagedList<>)).ConvertUsing(typeof(PagedListConverter<,>));
        }
        private class PagedListConverter<TSource, TDestination> : ITypeConverter<PagedList<TSource>, PagedList<TDestination>>
        {

            public PagedList<TDestination> Convert(PagedList<TSource> source, PagedList<TDestination> destination, ResolutionContext context)
            {
                var values = context.Mapper.Map<IEnumerable<TDestination>>(source.Data);
                return new PagedList<TDestination>(values, source.TotalCount, source.Page, source.PageSize);
            }
        }

    }
}
