using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InterviewProject.Data
{
    /// <summary>
    /// Base interface for providing db contexts
    /// </summary>
    public interface IDbContext: IDisposable
    {
        DatabaseFacade Database { get; }

        IModel Model { get; }

        DbSet<T> Set<T>() where T : class;

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

