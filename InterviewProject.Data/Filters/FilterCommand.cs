namespace InterviewProject.Data.Filters
{
    public class FilterCommand: ISorter
    {
        public int Take { get; set; } = 10;

        public int Skip { get; set; } = 0;

        public string? SortField { get; set; }
        public OrderDirection SortOrder { get; set; }

        public string? SearchQuery { get; set; }
    }
}

