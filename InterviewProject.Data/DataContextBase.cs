using InterviewProject.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InterviewProject.Data
{
    public abstract class DataContextBase: IdentityDbContext<User, Role, string,
        IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>,
        IdentityUserToken<string>>, IDataContext
    {
        public DataContextBase(DbContextOptions options): base(options)
        {
            
        }
        
        public DbSet<Note> Notes { get; set; }
    }
}

