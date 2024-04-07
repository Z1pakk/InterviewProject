namespace InterviewProject.Data.Interfaces
{
    /// <summary>
    /// Base interface for dtos with generic primary key named "Id".
    /// </summary>
    public interface IDTO<TKey>
        where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }

    /// <summary>
    /// Base interface for entities with integer primary key named "Id".
    /// </summary>
    public interface IDTO : IEntity<int>
    {
    }
}

