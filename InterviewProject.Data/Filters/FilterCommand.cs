namespace InterviewProject.Data.Filters
{
    /// <summary>
    /// We use it to make filtering and sorting data fro grids
    /// </summary>
    public class FilterCommand: ISorter
    {
        public int Take { get; set; } = 10;

        public int Skip { get; set; } = 0;

        public string? SortField { get; set; }
        public OrderDirection SortOrder { get; set; }

        public string? SearchQuery { get; set; }
    }
}

