namespace InterviewProject.Data.Interfaces
{
    /// <summary>
    /// Base interface for entities with generic primary key named "Id".
    /// </summary>
    public interface IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }

    /// <summary>
    /// Base interface for entities with integer primary key named "Id".
    /// </summary>
    public interface IEntity : IEntity<int>
    {
    }
}

