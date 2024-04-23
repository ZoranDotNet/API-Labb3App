namespace API_Labb3.DTOs
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;
        private int pageSize { get; set; } = 10;
        private readonly int maxRecordsPerPage = 50;
        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                if (value > maxRecordsPerPage)
                {
                    pageSize = maxRecordsPerPage;
                }
                else
                {
                    pageSize = value;
                }
            }
        }
    }
}
