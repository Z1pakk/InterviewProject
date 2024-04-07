using InterviewProject.Data;
using InterviewProject.Data.ModelConfiguration;
using Microsoft.EntityFrameworkCore;

namespace InterviewProject.SqlServer
{
    // for migrations:
    // from InterviewProject.Server project:
    // create - dotnet ef migrations add <MigrationName> -p ../InterviewProject.SqlServer -s ./ -c DataContext
    // update - dotnet ef database update -p ../InterviewProject.SqlServer -s ./ -c DataContext

    // migration rollback
    // revert - dotnet ef database update <PreviousMigrationName> -p ../InterviewProject.SqlServer -s ./ -c DataContext
    // remove - dotnet ef migrations remove -p ../InterviewProject.SqlServer -s ./ -c DataContext
    
    public class DataContext: DataContextBase
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}

