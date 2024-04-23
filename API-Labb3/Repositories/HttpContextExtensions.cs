using Microsoft.EntityFrameworkCore;


namespace API_Labb3.Repositories
{
    public static class HttpContextExtensions
    {
        public async static Task InsertPaginationParametersInResponseHeader<T>(
            this HttpContext httpContext, IQueryable<T> queryable)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }
            //total number of records to display in Response Header
            double count = await queryable.CountAsync();
            httpContext.Response.Headers.Append("total-records", count.ToString());
        }
    }
}
