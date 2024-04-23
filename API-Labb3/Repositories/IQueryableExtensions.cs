using API_Labb3.DTOs;

namespace API_Labb3.Repositories
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDTO paginationDTO)
        {
            return queryable
                .Skip((paginationDTO.Page - 1) * paginationDTO.PageSize)
                .Take(paginationDTO.PageSize);
        }
    }
}
