namespace UBC.Core.Service.DTO
{
    public class PaginationDTO<TDto> where TDto : class, new()
    {
        public int CurrentPage { get; set; }

        public int Count { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public List<TDto> Result { get; set; }
    }
}