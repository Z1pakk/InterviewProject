using InterviewProject.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace InterviewProject.Data
{
    public interface IDataContext: IDbContext
    {
        public DbSet<Note> Notes { get; set; }
    }
}

