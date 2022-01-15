using Microsoft.AspNetCore.Mvc;

namespace API.Helpers
{
    public class PaginationParams
    {
        private const int maxPageSize = 50;
        [FromQuery(Name = "pageNumber")]
        public int PageNuber { get; set; } = 1;
        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
}
