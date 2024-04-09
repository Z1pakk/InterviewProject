namespace InterviewProject.Data.Filters
{
    /// <summary>
    /// Result class for grids. It stores total count of records and data
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class PageResult<TEntity> where TEntity : class
    {
        public int Total { get; set; }

        public IEnumerable<TEntity> Items { get; set; }
    }
}

