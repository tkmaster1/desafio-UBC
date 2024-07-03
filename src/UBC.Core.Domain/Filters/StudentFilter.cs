namespace UBC.Core.Domain.Filters
{
    public class StudentFilter
    {
        /// <summary>
        /// Codigo
        /// </summary>
        public int? Code { get; set; }

        public string Name { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string OrderBy { get; set; } = "firstname";

        public string SortBy { get; set; } = "asc";
    }
}
